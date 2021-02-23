using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GroceriesiOSCS.Models;
using Newtonsoft.Json;

namespace GroceriesiOSCS.Classes.DataManagement
{
    public static class ReadWriteDisk
    {
        static readonly string MainPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
        static readonly string DataPath = Path.Combine (MainPath, "data.json");
        static readonly string UserPath = Path.Combine (MainPath, "user.json");

        public static void WriteData ()
        {
            AppData.offlineList = new List<GroceryList> ();
            foreach (var list in AppData.currentList.Where (list => list.ListOwner.Uid == AppData.currentUser.Uid)) {
                AppData.offlineList.Add (list);
            }

            var dataJson = JsonConvert.SerializeObject (AppData.offlineList, Formatting.Indented);
            File.WriteAllText (DataPath, dataJson);
        }

        public static void ReadData ()
        {
            AppData.offlineList = new List<GroceryList> ();

            using var file = File.OpenText (DataPath);
            var serializer = new JsonSerializer ();
            AppData.offlineList = (List<GroceryList>) serializer.Deserialize (file, typeof (List<GroceryList>));
        }

        public static void WriteUser ()
        {
            var userJson = JsonConvert.SerializeObject (AppData.currentUser, Formatting.Indented);
            File.WriteAllText (UserPath, userJson); 
        }

        public static void ReadUser ()
        {
            if (!File.Exists (UserPath)) return;

            using var file = File.OpenText (UserPath);
            var serializer = new JsonSerializer ();
            AppData.currentUser = (User) serializer.Deserialize (file, typeof(User));
        }
    }
}
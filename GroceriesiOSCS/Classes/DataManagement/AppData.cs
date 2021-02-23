using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using GroceriesiOSCS.Models;
using User = GroceriesiOSCS.Models.User;

namespace GroceriesiOSCS.Classes.DataManagement
{
    public class AppData
    {
        static AppData instance;

        public static User currentUser;
        public static List<GroceryList> currentList; //Always shown in TableView
        public static List<GroceryList> offlineList; //Data that is stored on Disk

        #region FirebaseAndOnline
        
        public static List<GroceryList> onlineList; //Data stored on cloud
        public static DatabaseReference DataNode { get; set; } //represents the "data" node that should be in your database
        public static DatabaseReference UsersNode { get; set; } //represents the "users" node that should be in your database
        public static Auth auth;

        #endregion

        #region Invitations

        public static List<Invitations> invitationsData; //"Coordinates" to where the invitations are
        public static List<GroceryList> invitationsList; //Lists we've been invited to
        

        #endregion

        public static AppData GetInstance ()
        {
            return instance ??= new AppData ();
        }

        AppData ()
        {
            currentList = new List<GroceryList> ();
            
            //App.Configure(); called in AppDelegate
            DataNode = Database.DefaultInstance.GetRootReference ().GetChild ("data"); //RootReference is the URL associated with the database.
            UsersNode = Database.DefaultInstance.GetRootReference ().GetChild ("users");
            
            auth = Auth.DefaultInstance;
        }

        public static void ClearOnLogout ()
        {
            currentUser = null;
            currentList = null;
            onlineList = null;
            offlineList = null;
            invitationsData = null;
            invitationsList = null;
        }

    }
}
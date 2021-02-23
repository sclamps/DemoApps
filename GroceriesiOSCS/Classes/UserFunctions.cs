using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using GroceriesiOSCS.Classes.DataManagement;
using GroceriesiOSCS.Classes.Helpers;
using GroceriesiOSCS.Models;

namespace GroceriesiOSCS.Classes
{
    public static class UserFunctions
    {
        static void SetLocalUser (User user)
        {
            foreach (var list in AppData.currentList.Where (list => list.ListOwner.Uid == AppData.currentUser.Uid)) {
                list.ListOwner = user;
            }
            
            AppData.currentList = new List<GroceryList> ();
            AppData.currentUser = user;
            ReadWriteDisk.WriteData ();
            ReadWriteDisk.WriteUser ();
        }
        
        public static async Task RegisterUser (ListsViewController view, string name, string email, string password)
        {
            var done = false;
            
            AppData.auth.CreateUser (email, password, (user, error) => {
                if (error != null) {
                    CustomAlert.Alert (view, "Error creating user", error.UserInfo.Description);
                    return;
                }

                if (user != null) {
                    var changeRequest = user.User.ProfileChangeRequest ();
                    changeRequest.DisplayName = name;
                    changeRequest.CommitChanges (async profileError => {
                        if (profileError != null) {
                            CustomAlert.Alert (view, "Error updating user profile", profileError.UserInfo.Description);
                            return;
                        }
                        //store locally
                        SetLocalUser ( new User {
                            Name =  user.User.DisplayName,
                            Email = user.User.Email,
                            Uid =  user.User.Uid
                        });

                        object[] userKeys = {"name", "email", "uid"};
                        object[] userValues = {user.User.DisplayName, user.User.Email, user.User.Uid};
                        var userDict = NSDictionary.FromObjectsAndKeys (userValues, userKeys);
                        //store online
                        AppData.UsersNode.GetChild (user.User.Uid).SetValue (userDict);

                        foreach (var list in AppData.currentList.Where (list => list.ListOwner.Uid == AppData.currentUser.Uid)) {
                            CloudFunctions.SaveList (list);
                        }

                        await view.ReloadData ();
                        CustomAlert.Alert (view, "Success", "You are now registered and can share lists with your friends.");
                        
                        done = true;
                    });
                }
            });

            while (!done) {
                await Task.Delay (50);
            }
        }
        
        public static async Task Login (ListsViewController view, string inpEmail, string inpPassword)
        {
            var done = false;

            AppData.auth.SignInWithPassword(inpEmail, inpPassword, async (user, error) =>
            {
                if (error != null)
                {
                    CustomAlert.Alert(view, "Error Signing In", error.UserInfo.Description);
                    done = true;
                    return;
                }

                if (user != null) {
                    SetLocalUser (new User
                    {
                        Name = user.User.DisplayName,
                        Uid = user.User.Uid,
                        Email = user.User.Email
                    });

                    await view.ReloadData ();
                    CustomAlert.Alert(view, "Login Was Successful", "Welcome back " + user.User.DisplayName);
                }

                done = true;
            });
            
            while (!done)
            {
                await Task.Delay(50);
            }
        }
        
        public static async void Logout (ListsViewController view)
        {
            var signedOut = AppData.auth.SignOut (out var error);
            
            if (signedOut) {
                CustomAlert.Alert (view, "Logged Out", "You can still work offline!");

                await view.ReloadData ();
                return;
            }
            
            CustomAlert.Alert (view, "Error on Logout", error?.ToString ());
        }
    }
}
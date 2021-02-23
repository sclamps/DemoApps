using System.Linq;
using System.Threading.Tasks;
using GroceriesiOSCS.Classes.Helpers;
using GroceriesiOSCS.Models;
using UIKit;

namespace GroceriesiOSCS.Classes.DataManagement
{
    public static class ReadAllData
    {
        public static async Task Read (ListsViewController view)
        {
            ReadWriteDisk.ReadUser ();
            
            if (AppData.currentUser != null) {
                //Read data and use that as the current list of lists
                ReadWriteDisk.ReadData ();
                AppData.currentList = AppData.offlineList;
            }
            
            view.SetProfileButton ("Offline. Login Here", UIColor.Orange);
            
            
            //if we have a logged in user, pull online data, compare it to local data and make comparison updates, then update locally and online
            if (AppData.auth.CurrentUser != null) {
                if (AppData.currentUser == null)
                    return;
                
                view.SetProfileButton (AppData.auth.CurrentUser.DisplayName + " is Online", UIColor.Green);
                
                await CloudFunctions.Read();

                AppData.currentList = ListHelpers.Compare(AppData.onlineList, AppData.offlineList);

                ReadWriteDisk.WriteData();
                
                foreach (var list in AppData.currentList.Where (list => list.ListOwner.Uid == AppData.currentUser.Uid))
                    CloudFunctions.SaveList (list);

                await Invitations.Read (); //This gets "myInvitations" info and puts in AppData.invitationsData
                await Invitations.FetchInvitationItems (); //Uses invitationsData to get the actual list & list items and puts them in AppData.invitationsList

                foreach (var invitation in AppData.invitationsList) {
                    AppData.currentList.Add (invitation);
                }
            }
        }
    }
}
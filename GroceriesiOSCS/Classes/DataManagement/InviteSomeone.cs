using Firebase.Database;
using Foundation;
using GroceriesiOSCS.Classes.Helpers;
using GroceriesiOSCS.Models;
using UIKit;

namespace GroceriesiOSCS.Classes.DataManagement
{
    public static class InviteSomeone
    {
        public static void Invite(UIViewController view, GroceryList list, string inviteeEmail)
        {
            User inviteeUser = null;
            var inviterUser = AppData.currentUser;
            var listName = list.ListName;

            AppData.UsersNode.ObserveSingleEvent(DataEventType.Value, snapshot =>
            {
                var children = snapshot.Children;
                var childSnapShot = children.NextObject() as DataSnapshot;

                while (childSnapShot != null)
                {
                    var childDict = childSnapShot.GetValue<NSDictionary>();

                    if (childDict.ValueForKey((NSString)"email").ToString() == inviteeEmail)
                    {
                        // user exist
                        inviteeUser = new User
                        {
                            Name = childDict.ValueForKey((NSString)"name").ToString(),
                            Email = childDict.ValueForKey((NSString)"email").ToString(),
                            Uid = childDict.ValueForKey((NSString)"uid").ToString()
                        };
                        break;
                    }
                    childSnapShot = children.NextObject() as DataSnapshot;
                }


                if (inviteeUser == null)
                {
                    CustomAlert.Alert(view, 
                                    "No Such User",
                                    "Such user doesn't have an account with us");
                    return;
                }

                var invitationTitle = inviterUser.Uid + "|" + listName;

                object[] ownerKeys = { "ownerUid", "ownerEmail", "ownerName" };
                object[] ownerValues = { inviterUser.Uid, inviterUser.Email, inviterUser.Name };
                var ownerDict = NSDictionary.FromObjectsAndKeys(ownerValues, ownerKeys);

                object[] inviteeKeys = { "listName", "owner" };
                object[] inviteeValues = { listName, ownerDict };
                var inviteeDict = NSDictionary.FromObjectsAndKeys(inviteeValues, inviteeKeys);

                var inviteeNode = AppData.UsersNode.GetChild(inviteeUser.Uid);
                inviteeNode.GetChild("myInvitations")
                           .GetChild(invitationTitle)
                           .SetValue(inviteeDict);

                CustomAlert.Alert(view, 
                                "Invitation Sent",
                                "You have successfully invited " + inviteeUser.Name + " to this list.");
            });

        }
    }
}

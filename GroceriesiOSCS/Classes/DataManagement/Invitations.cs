using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Core;
using Firebase.Database;
using Foundation;
using GroceriesiOSCS.Models;
using User = GroceriesiOSCS.Models.User;

namespace GroceriesiOSCS.Classes.DataManagement
{
    public class Invitations
    {
        public string ListName { get; set; }
        public User ListOwner { get; set; }

        public static async Task Read ()
        {
            AppData.invitationsData = new List<Invitations> ();

            if (AppData.auth.CurrentUser == null)
                return;

            var done = false;

            AppData.UsersNode
                .GetChild (AppData.currentUser.Uid)
                .GetChild ("myInvitations")
                .ObserveSingleEvent (DataEventType.Value, snapshot => {
                    if (!snapshot.HasChildren) {
                        done = true;
                        return;
                    }

                    var invitations = snapshot.GetValue<NSDictionary> ();

                    foreach (var invitationObject in invitations.Values) {
                        var invitation = (NSDictionary) invitationObject;
                        var listName = (NSString) invitation.ValueForKey ((NSString) "listName");
                        var invitationOwner = (NSDictionary) invitation.ValueForKey ((NSString) "owner");

                        AppData.invitationsData.Add (new Invitations {
                            ListName = listName,
                            ListOwner = new User {
                                Name = (NSString) invitationOwner.ValueForKey ((NSString) "ownerName"),
                                Uid = (NSString) invitationOwner.ValueForKey ((NSString) "ownerUid"),
                                Email = (NSString) invitationOwner.ValueForKey ((NSString) "ownerEmail")
                            }
                        });
                    }

                    done = true;
                });
            
            while (!done) {
                await Task.Delay (50);
            }
        }

        public static async Task FetchInvitationItems ()
        {
            AppData.invitationsList = new List<GroceryList> ();

            if (AppData.auth.CurrentUser == null || AppData.invitationsData.Count == 0)
                return;

            var done = false;

            foreach (var invitation in AppData.invitationsData) {
                var listName = invitation.ListName;
                var ownerUid = invitation.ListOwner.Uid;

                AppData.DataNode
                    .GetChild (ownerUid)
                    .GetChild (listName)
                    .ObserveSingleEvent (DataEventType.Value, snapshot => {
                        var invitationData = snapshot.GetValue<NSDictionary> ();
                        var itemsInList = new List<Item> ();

                        if (invitationData?.ValueForKey ((NSString) "items") != null) {
                            if (invitationData.ValueForKey ((NSString) "items").IsKindOfClass (new ObjCRuntime.Class (typeof (NSDictionary)))) {
                                var listItems = (NSDictionary) NSObject.FromObject (invitationData.ValueForKey ((NSString) "items"));

                                foreach (var i in listItems.Values) {
                                    var item = (NSDictionary) NSObject.FromObject (i);
                                    var itemName = (NSString) item.ValueForKey ((NSString) "itemName");
                                    var itemTime = (NSString) item.ValueForKey ((NSString) "itemTime");
                                    var itemPurchasedString =
                                        (NSString) item.ValueForKey ((NSString) "itemPurchased");
                                    var itemPurchased = itemPurchasedString == "true" || itemPurchasedString == "True";

                                    itemsInList.Add (new Item {
                                        ItemName = itemName,
                                        ItemPurchased = itemPurchased,
                                        ItemTime = DateTime.Parse (itemTime)
                                    });
                                }
                            }
                        }

                        var thisList = new GroceryList {
                            ListName = listName,
                            ListOwner = invitation.ListOwner,
                            ListItems = itemsInList
                        };

                        AppData.invitationsList.Add (thisList);

                        done = true;
                    });
            }

            while (!done) {
                await Task.Delay (50);
            }
        }
        
        public static void RemoveInvitation (Invitations invitation)
        {
            if (AppData.auth.CurrentUser == null)
                return;

            var title = invitation.ListOwner.Uid + "|" + invitation.ListName;
            var invitationNode = AppData.UsersNode
                .GetChild (AppData.currentUser.Uid)
                .GetChild ("myInvitations")
                .GetChild (title);
            
            invitationNode.RemoveValue();
        }
    }
}
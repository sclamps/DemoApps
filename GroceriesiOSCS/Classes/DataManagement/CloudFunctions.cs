using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Firebase.Database;
using Foundation;
using GroceriesiOSCS.Classes.Helpers;
using GroceriesiOSCS.Models;

namespace GroceriesiOSCS.Classes.DataManagement
{
    public static class CloudFunctions
    {
        public static void SaveItem (Item item, GroceryList list)
        {
            if (AppData.auth.CurrentUser == null)
                return;

            object[] itemKeys = { "itemName", "itemTime", "itemPurchased"};
            object[] itemValues = { item.ItemName, item.ItemTime.ToString(CultureInfo.InvariantCulture), item.ItemPurchased.ToString()};

            var itemDict = NSDictionary.FromObjectsAndKeys (itemValues, itemKeys);

            AppData.DataNode
                .GetChild (list.ListOwner.Uid)
                .GetChild (list.ListName)
                .GetChild ("items")
                .GetChild (item.ItemName)
                .SetValue (itemDict);
        }
        
        public static void SaveList (GroceryList newList)
        {
            if (AppData.auth.CurrentUser == null)
                return;

            var toWriteDict = ListHelpers.ListToDict(newList);

            AppData.DataNode.GetChild(AppData.currentUser.Uid)
                .GetChild(newList.ListName)
                .SetValue(toWriteDict);
        }
        
        public static void DeleteItem (Item item, GroceryList list)
        {
            if (AppData.auth.CurrentUser == null)
                return;

            var itemNode = AppData.DataNode
                .GetChild (list.ListOwner.Uid)
                .GetChild (list.ListName)
                .GetChild ("items")
                .GetChild (item.ItemName);
            
            itemNode.RemoveValue();
        }
        
        public static void DeleteList (GroceryList list)
        {
            if (AppData.auth.CurrentUser == null)
                return;

            var listNode = AppData.DataNode
                .GetChild (list.ListOwner.Uid)
                .GetChild (list.ListName);
            
            listNode.RemoveValue();
        }

        public static async Task Read ()
        {
            AppData.onlineList = new List<GroceryList> ();

            if (AppData.auth.CurrentUser == null)
                return;

            var done = false;

            AppData.DataNode
                .GetChild (AppData.currentUser.Uid)
                .ObserveSingleEvent (DataEventType.Value, snapshot => {
                    if (!snapshot.HasChildren) {
                        done = true;
                        return;
                    }

                    var allListsData = snapshot.GetValue<NSDictionary> ();

                    if (allListsData.Count == 0 || !allListsData.IsKindOfClass (new ObjCRuntime.Class (typeof (NSDictionary)))) {
                        done = true;
                        return;
                    }

                    foreach (var listObject in allListsData.Values) {
                        var list = (NSDictionary) listObject;

                        var listName = (NSString) list.ValueForKey ((NSString) "listName");

                        var itemsInList = new List<Item> ();
                        var items = list.ValueForKey ((NSString) "items");
                        if (items != null && items.IsKindOfClass (new ObjCRuntime.Class (typeof (NSDictionary)))) {
                            var itemsOfListValues = (NSDictionary) NSObject.FromObject (list.ValueForKey ((NSString) "items"));

                            foreach (var t in itemsOfListValues.Values) {
                                var eachItemValues = (NSDictionary) NSObject.FromObject (t);
                                var fetchedItemName = (NSString) eachItemValues.ValueForKey ((NSString) "itemName");
                                var fetchedItemTime = (NSString) eachItemValues.ValueForKey ((NSString) "itemTime");
                                var fetchedItemPurchasedStr =
                                    (NSString) eachItemValues.ValueForKey ((NSString) "itemPurchased");
                                var fetchedItemPurchased = fetchedItemPurchasedStr == "true" ||
                                                           fetchedItemPurchasedStr == "True";

                                itemsInList.Add (new Item {
                                    ItemName = fetchedItemName,
                                    ItemPurchased = fetchedItemPurchased,
                                    ItemTime = DateTime.Parse (fetchedItemTime)
                                });
                            }
                        }

                        AppData.onlineList.Add (new GroceryList {
                            ListName = listName,
                            ListOwner = AppData.currentUser,
                            ListItems = itemsInList
                        });
                    }

                    done = true;
                });

            while (!done) {
                Debug.WriteLine ("awaiting.........");
                await Task.Delay (50);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using GroceriesiOSCS.Classes.DataManagement;
using GroceriesiOSCS.Models;

namespace GroceriesiOSCS.Classes.Helpers
{
    public class PrepareInitialTestData
    {
        public static void Prepare ()
        {
            AppData.currentList = new List<GroceryList> ();
            AppData.currentList.Add (new GroceryList {
                ListName = "Sample List",
                ListOwner = AppData.currentUser,
                ListItems = new List<Item> ()
            });
            
            AppData.currentList[0].ListItems.Add (new Item {
                ItemName = "Milk",
                ItemTime = DateTime.Now,
                ItemPurchased = false
            });
            
            AppData.currentList[0].ListItems.Add (new Item {
                ItemName = "Bread",
                ItemTime = DateTime.Now,
                ItemPurchased = true
            });

            AppData.currentList.Add (new GroceryList {
                ListName = "Office Supplies",
                ListOwner = AppData.currentUser,
                ListItems = new List<Item> ()
            });
            
            AppData.currentList[1].ListItems.Add (new Item {
                ItemName = "Blue Pen",
                ItemTime = DateTime.Now,
                ItemPurchased = false
            });
            
            AppData.currentList[1].ListItems.Add (new Item {
                ItemName = "Staples",
                ItemTime = DateTime.Now,
                ItemPurchased = true
            });
        }
    }
}
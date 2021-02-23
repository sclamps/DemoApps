using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using GroceriesiOSCS.Models;

namespace GroceriesiOSCS.Classes.Helpers
{
    public static class ListHelpers
    {
        public static NSDictionary ListToDict(GroceryList newList)
        {
            var allItemsDict = new NSMutableDictionary();

            foreach (var item in newList.ListItems)
            {
                var eachItemDict = new NSMutableDictionary();

                eachItemDict.SetValueForKey((NSString)item.ItemName, 
                    (NSString)"itemName");
                
                eachItemDict.SetValueForKey((NSString)item.ItemPurchased.ToString(),
                    (NSString)"itemPurchased");
                
                eachItemDict.SetValueForKey((NSString)item.ItemTime.ToString(),
                    (NSString)"itemTime");


                allItemsDict.SetValueForKey(eachItemDict,
                    (NSString)item.ItemName);
            }

            object[] listOwnerKeys = { "name", "email", "uid" };
            object[] listOwnerValues = { newList.ListOwner.Name, newList.ListOwner.Email, newList.ListOwner.Uid };

            var listOwnerDict = NSDictionary.FromObjectsAndKeys(listOwnerValues, listOwnerKeys);


            var anyListDataDict = new NSMutableDictionary();

            anyListDataDict.SetValueForKey(allItemsDict, (NSString)"items");
            anyListDataDict.SetValueForKey((NSString)newList.ListName, (NSString)"listName");
            anyListDataDict.SetValueForKey(listOwnerDict, (NSString)"listOwner");


            if (allItemsDict.Count == 0)
                anyListDataDict.Remove((NSString)"items");

            return anyListDataDict;
        }
        
        public static List<GroceryList> Compare(List<GroceryList> listA, List<GroceryList> listB)
        {
            var combinedListsLst = new List<GroceryList>();

            // first, if there is a list in one that is not in other, just copy it
            foreach (var a in listA)
            {
                foreach (var _ in listB.Where (anyList => a.ListName == anyList.ListName)) {
                    goto ContinueLoop;
                }
                combinedListsLst.Add(a);

            ContinueLoop:;
            }

            // and the other way around
            foreach (var b in listB)
            {
                foreach (var _ in listA.Where (anyList => b.ListName == anyList.ListName)) {
                    goto ContinueLoop;
                }
                combinedListsLst.Add(b);

            ContinueLoop:;
            }


            // then we remove anyone we have already added
            foreach (var any in combinedListsLst)
            {
                if (listA.Contains(any))
                    listA.Remove(any);
                if (listB.Contains(any))
                    listB.Remove(any);
            }


            // now let's compare the similar lists against each other
            foreach (var aList in listA)
            {
                var thisListResultItems = new List<Item>();

                var counterPartList = new GroceryList();
                var combinedTime = DateTime.UtcNow;


                // first we find the lists that are similar
                foreach (var bList in listB.Where (bList => bList.ListName == aList.ListName)) {
                    counterPartList = bList;
                    break;
                }

                // then we check their items
                foreach (var aItem in aList.ListItems)
                {
                    // let's compare items from one list to another
                    foreach (var counterItem in counterPartList.ListItems.Where (counterItem => aItem.ItemName == counterItem.ItemName)) {
                        // item exists in both lists, let's decide which one to add
                        thisListResultItems.Add (aItem.ItemTime > counterItem.ItemTime ? aItem : counterItem);

                        goto ContinueHere;
                    }
                    // if we reach here, non of the names have matched and items are unique
                    thisListResultItems.Add(aItem);
                ContinueHere:;
                }


                // by now all items of this LIST are resolved, now, let's deal with counter part
                foreach (var counterItem in counterPartList.ListItems)
                {
                    // let's compare items from counter to the main
                    foreach (var unused in aList.ListItems.Where (anyItem => counterItem.ItemName == anyItem.ItemName)) {
                        // item exists both sides, we have already added that, let's drop out
                        goto ContinueHere;
                    }
                    // if we reach here, non of the names have matched
                    thisListResultItems.Add(counterItem);

                ContinueHere:;
                }


                // this shopping class now contains all of the similar ones and uniques ones
                combinedListsLst.Add(new GroceryList
                {
                    ListName = aList.ListName,
                    ListOwner = aList.ListOwner,
                    ListItems = thisListResultItems
                });

            }

            return combinedListsLst;
        }
    }
}
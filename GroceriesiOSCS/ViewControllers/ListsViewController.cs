using Foundation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GroceriesiOSCS.Classes;
using GroceriesiOSCS.Classes.DataManagement;
using GroceriesiOSCS.Classes.Helpers;
using GroceriesiOSCS.Models;
using UIKit;

namespace GroceriesiOSCS
{
    public partial class ListsViewController : UIViewController
    {
        public ListsViewController(IntPtr handle) : base(handle)
        {
        }

        ListsDataSource _listsDataSource;

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            AppData.GetInstance ();
            
            _listsDataSource = new ListsDataSource (this);
            listsTableView.Source = _listsDataSource;
            
            await ReloadData ();
        }
        
        public async Task ReloadData ()
        {
            await ReadAllData.Read (this);
            listsTableView.ReloadData (); 
        } 

        partial void NewListButton_TouchUpInside(UIButton sender)
        {
            var alert = UIAlertController.Create (
                "New List", 
                "Please enter a list name", 
                UIAlertControllerStyle.Alert);
            alert.AddTextField (field => {
                field.Placeholder = "List Name";
            });

            var saveAction = UIAlertAction.Create (
                "Save", 
                UIAlertActionStyle.Default, 
                action => { SaveNewList (alert.TextFields[0].Text); });
            
            alert.AddAction (saveAction);
            alert.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, null));

            PresentViewController (alert, true, null);
        }

        partial void ProfileButton_TouchUpInside(UIButton sender)
        {
            var alert = UIAlertController.Create ("Profile", "What would you like to do", UIAlertControllerStyle.ActionSheet);

            var register = UIAlertAction.Create ("Register", UIAlertActionStyle.Default, action => CustomAlert.RegisterAlert (this));

            var login = UIAlertAction.Create ("Login", UIAlertActionStyle.Default, action => CustomAlert.LoginAlert (this));
            
            var logout = UIAlertAction.Create ("Logout", UIAlertActionStyle.Default, action => UserFunctions.Logout (this));
            
            var cancel = UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, null);
            
            alert.AddAction (register);
            alert.AddAction (login);
            alert.AddAction (logout);
            alert.AddAction (cancel);

            PresentViewController (alert, true, null);
        }

        void SaveNewList (string name)
        {
            if (name.Length < 1) return;
            
            var newList = new GroceryList {
                ListName = name,
                ListOwner = AppData.currentUser,
                ListItems = new List<Item> ()
            };
            
            AppData.currentList.Add (newList);
            ReadWriteDisk.WriteData ();
            CloudFunctions.SaveList (newList);
            
            listsTableView.ReloadData ();
        }

        public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue (segue, sender);
            
            var senderIndexPath = (NSIndexPath) sender;

            if (segue.DestinationViewController is ItemsViewController itemsViewController) {
                itemsViewController.currentList = AppData.currentList[senderIndexPath.Row];
            }
        }

        public void SetProfileButton (string status, UIColor color)
        {
            profileButton.SetTitle (status, UIControlState.Normal);
            profileButton.BackgroundColor = color;
        } 
    }
}
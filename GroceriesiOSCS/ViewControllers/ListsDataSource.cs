using System;
using Foundation;
using GroceriesiOSCS.Classes;
using GroceriesiOSCS.Classes.DataManagement;
using GroceriesiOSCS.Models;
using UIKit;
namespace GroceriesiOSCS
{
    public class ListsDataSource: UITableViewSource
    {
        readonly UIViewController _dataSourceController;
        
        public ListsDataSource (UIViewController dataSourceController)
        {
            _dataSourceController = dataSourceController;
        }

        public override nint RowsInSection (UITableView tableview, nint section)
        {
            return AppData.currentList.Count;
        }
        
        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            var cell = new UITableViewCell (UITableViewCellStyle.Subtitle, "listsCell");
            var list = AppData.currentList[indexPath.Row];
            
            if (list.ListOwner.Uid == AppData.currentUser.Uid) {
                cell.DetailTextLabel.TextColor = UIColor.Black;;
            } else {
                cell.DetailTextLabel.TextColor = UIColor.Red;
            }
            
            cell.TextLabel.Text = list.ListName;
                 
            var subtext = list.ListItems.Count + " items for " + list.ListOwner.Name;
            cell.DetailTextLabel.Text = subtext;

            return cell;
        }
        
        public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
        {
            return true;
            
        }

        public override string TitleForDeleteConfirmation (UITableView tableView, NSIndexPath indexPath)
        {
            var toRemove = AppData.currentList[indexPath.Row];
            return toRemove.ListOwner.Uid == AppData.currentUser.Uid ? "Delete" : "Remove";
        }

        public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            var toRemove = AppData.currentList[indexPath.Row];
            AppData.currentList.Remove (toRemove);

            if (toRemove.ListOwner.Uid == AppData.currentUser.Uid) {
                ReadWriteDisk.WriteData ();
                CloudFunctions.DeleteList(toRemove);
            } else {
                var invitation = new Invitations {
                    ListName = toRemove.ListName,
                    ListOwner = toRemove.ListOwner,
                };
                
                Invitations.RemoveInvitation (invitation);
            }
            
            tableView.DeleteRows (new[] {indexPath}, UITableViewRowAnimation.Fade);
        }

        public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
        {
            //iOS 13 won't call ViewDidAppear on Dismiss so the lists won't reload automatically.
            //you'll want to push or show the view controller instead, which will require a NavigationController setup.
            _dataSourceController.PerformSegue ("toItemsSegue_id", indexPath); 
        }
    }
}

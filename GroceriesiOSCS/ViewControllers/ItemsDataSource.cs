using System;
using Foundation;
using GroceriesiOSCS.Classes.DataManagement;
using GroceriesiOSCS.Models;
using UIKit;

namespace GroceriesiOSCS.Classes
{
    public class ItemsDataSource: UITableViewSource
    {
        readonly GroceryList list;

        public ItemsDataSource (GroceryList list)
        {
            this.list = list;
        }
        
        public override nint RowsInSection (UITableView tableview, nint section)
        {
            return list.ListItems.Count;
        }
        
        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell ("itemsCell", indexPath);
            var item = list.ListItems[indexPath.Row];

            if (item.ItemPurchased) {
                cell.Accessory = UITableViewCellAccessory.Checkmark;
                cell.BackgroundColor = UIColor.DarkGray;
                cell.TextLabel.TextColor = UIColor.LightGray;
                cell.TextLabel.AttributedText = new NSAttributedString(item.ItemName, strikethroughStyle: NSUnderlineStyle.Single);;
            } else {
                cell.Accessory = UITableViewCellAccessory.None;
                cell.BackgroundColor = UIColor.White;
                cell.TextLabel.TextColor = UIColor.Black;
                cell.TextLabel.AttributedText = new NSAttributedString(item.ItemName);
            }
            
            return cell;
        }

        public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
        {
            var item = list.ListItems[indexPath.Row];
            item.ItemPurchased = !item.ItemPurchased;
            item.ItemTime = DateTime.Now;
            
            tableView.ReloadData ();
            
            ReadWriteDisk.WriteData ();
            CloudFunctions.SaveItem (item, list);
        }

        public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override string TitleForDeleteConfirmation (UITableView tableView, NSIndexPath indexPath)
        {
            return "Delete";
        }

        public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            var toDelete = list.ListItems[indexPath.Row];
            list.ListItems.Remove (toDelete);
            
            ReadWriteDisk.WriteData ();
            CloudFunctions.DeleteItem (toDelete, list);
            
            tableView.DeleteRows (new[] {indexPath}, UITableViewRowAnimation.Fade);
        }
    }
}
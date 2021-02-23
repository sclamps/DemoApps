// WARNING
//
// This file has been generated automatically by Rider IDE
//   to store outlets and actions made in Xcode.
// If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace GroceriesiOSCS
{
	[Register ("ItemsViewController")]
	partial class ItemsViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton backButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UITableView itemsTableView { get; set; }

		[Outlet]
		UIKit.UITextField itemTextField { get; set; }

		[Outlet]
		UIKit.UILabel listNameLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton shareList { get; set; }

		[Action ("BackButton_TouchUpInside:")]
		partial void BackButton_TouchUpInside (UIKit.UIButton sender);

		[Action ("ShareList_TouchUpInside:")]
		partial void ShareList_TouchUpInside (UIKit.UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (backButton != null) {
				backButton.Dispose ();
				backButton = null;
			}

			if (itemsTableView != null) {
				itemsTableView.Dispose ();
				itemsTableView = null;
			}

			if (itemTextField != null) {
				itemTextField.Dispose ();
				itemTextField = null;
			}

			if (listNameLabel != null) {
				listNameLabel.Dispose ();
				listNameLabel = null;
			}

			if (shareList != null) {
				shareList.Dispose ();
				shareList = null;
			}

		}
	}
}

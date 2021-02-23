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
	[Register ("ListsViewController")]
	partial class ListsViewController
	{
		[Outlet]
		UIKit.UITableView listsTableView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton newListButton { get; set; }

		[Outlet]
		UIKit.UIButton profileButton { get; set; }

		[Action ("NewListButton_TouchUpInside:")]
		partial void NewListButton_TouchUpInside (UIKit.UIButton sender);

		[Action ("ProfileButton_TouchUpInside:")]
		partial void ProfileButton_TouchUpInside (UIKit.UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (listsTableView != null) {
				listsTableView.Dispose ();
				listsTableView = null;
			}

			if (newListButton != null) {
				newListButton.Dispose ();
				newListButton = null;
			}

			if (profileButton != null) {
				profileButton.Dispose ();
				profileButton = null;
			}

		}
	}
}

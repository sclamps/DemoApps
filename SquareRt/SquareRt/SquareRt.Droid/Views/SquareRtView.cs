using Android.App;
using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace SquareRt.Droid.Views
{
    [Activity(Label = "@string/ApplicationName")]
    public class SquareRtView: MvxAppCompatActivity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);
            SetContentView (Resource.Layout.squarert_view);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar> (Resource.Id.toolbar);
            SetSupportActionBar (toolbar);
        }
    }
}
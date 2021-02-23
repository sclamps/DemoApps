using UIKit;

namespace GroceriesiOSCS.Classes.Helpers
{
    public static class CustomAlert
    {
        public static void Alert (UIViewController view, string title, string message)
        {
            var alertController = UIAlertController.Create (
                title, 
                message, 
                UIAlertControllerStyle.Alert);
            alertController.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, null));
            
            view.PresentViewController (alertController, true, null);
        }
        
        public static void RegisterAlert (ListsViewController view)
        {
            var alert = UIAlertController.Create ("Register", "Please enter name, email, and password",
                UIAlertControllerStyle.Alert);
            
            alert.AddTextField (field => {
                field.Placeholder = "name";
            });
            
            alert.AddTextField (field => {
                field.Placeholder = "email";
                field.KeyboardType = UIKeyboardType.EmailAddress;
            });
            
            alert.AddTextField (field => {
                field.Placeholder = "password";
                field.SecureTextEntry = true;
            });

            var register = UIAlertAction.Create ("Register", UIAlertActionStyle.Default, async action => await UserFunctions.RegisterUser (
                view, 
                alert.TextFields[0].Text, 
                alert.TextFields[1].Text, 
                alert.TextFields[2].Text)
            );
            
            alert.AddAction (register);
            alert.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, null));
            view.PresentViewController (alert, true, null);
        }
        
        public static void LoginAlert (ListsViewController view)
        {
            var loginAlert = UIAlertController.Create("Login Online", "Please enter your email and password", UIAlertControllerStyle.Alert);

            loginAlert.AddTextField((field) =>
            {
                field.Font = UIFont.SystemFontOfSize(22);
                field.Placeholder = "Email";
                field.KeyboardType = UIKeyboardType.EmailAddress;
                field.TextAlignment = UITextAlignment.Center;
            });

            loginAlert.AddTextField((field) =>
            {
                field.Font = UIFont.SystemFontOfSize(22);
                field.Placeholder = "Password";
                field.SecureTextEntry = true;
                field.TextAlignment = UITextAlignment.Center;
            });


            var loginAction = UIAlertAction.Create("Login",
                UIAlertActionStyle.Default,
                async _ => {
                    await UserFunctions.Login (view, loginAlert.TextFields[0].Text,
                        loginAlert.TextFields[1].Text);
                    await view.ReloadData ();
                });

            loginAlert.AddAction(loginAction);
            loginAlert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Destructive, null));
            view.PresentViewController(loginAlert, true, null);
        }
    }
}
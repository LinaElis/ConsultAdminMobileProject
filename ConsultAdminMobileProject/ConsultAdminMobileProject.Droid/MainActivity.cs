using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics.Drawables;
using Microsoft.WindowsAzure.MobileServices;
using ConsultAdminMobileProject;
using ConsultAdminMobileProject.Model;
using ConsultAdminMobileProject.Interface;
using ConsultAdminMobileProject.Service;
using Xamarin.Forms;
using ConsultAdminMobileProject.UI;

namespace ConsultAdminMobileProject.Droid
{
    [Activity(Theme = "@style/Theme.Splash", //Indicates the theme to use for this activity
              MainLauncher = true, //Set it as boot activity
              NoHistory = true)] //Doesn't place it in back stack
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //System.Threading.Thread.Sleep(5000); //Let's wait awhile...
            this.StartActivity(typeof(MainActivity));
        }
    }

    [Activity(Label = "ConsultAdminProject",
        //Icon = "@drawable/icon", 
        //MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity //, IAuthenticate
    {

        protected override void OnCreate(Bundle bundle)
        {
            Insights.HasPendingCrashReport += (sender, isStartupCrash) =>
            {
                if (isStartupCrash)
                {
                    Insights.PurgePendingCrashReports().Wait();
                }
            };

            Insights.Initialize("29250715aa954d3ceb31ff2d01c1cc194d0e2dd6", ApplicationContext);

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            //AppUI.Init((IAuthenticate)this);

            //Tar bort den blåa xamarinikonen (gör den transparent)
            ActionBar.SetIcon(new ColorDrawable(Resources.GetColor(Android.Resource.Color.Transparent)));

            LoadApplication(new App());
        }

        private MobileServiceUser user;

        public async Task<bool> Authenticate()
        {
            var success = false;
            try
            {
                // Sign in with Facebook login using a server-managed flow.
                user = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(this,
                    MobileServiceAuthenticationProvider.Facebook);

                CreateAndShowDialog(string.Format("you are now logged in - {0}",
                    user.UserId), "Logged in!");

                success = true;
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex.Message, "Authentication failed");
            }
            return success;
        }

        private void CreateAndShowDialog(String message, String title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }

    }
}


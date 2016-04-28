using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ConsultAdminMobileProject.Droid;
using ConsultAdminMobileProject.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(UtilitiesAndroid))]
namespace ConsultAdminMobileProject.Droid
{
    internal class UtilitiesAndroid : Java.Lang.Object, IApplicationUtilities
    {
        public UtilitiesAndroid() { }

        public void CloseApp()
        {
            Process.KillProcess(Process.MyPid());
        }

    }
}
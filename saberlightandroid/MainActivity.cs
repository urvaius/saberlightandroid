using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace saberlightandroid
{
    [Activity(Label = "saberlightandroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            ImageButton imabeButton = FindViewById<ImageButton>(Resource.Id.button_on_off);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }
    }
}


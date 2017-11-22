using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Hardware.Camera2;
using Android.Media;
using Android.Content.PM;
using Android.Provider;
using System.Collections.Generic;


namespace saberlightandroid
{
    [Activity(Label = "saberlightandroid", MainLauncher = true,ScreenOrientation =ScreenOrientation.Portrait, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        private CameraManager mCameraManager;
        private String mCameraId;
        private ImageButton mTorchOnOffButton;
        private Boolean isTorchOn;
        private MediaPlayer mp;
        private MediaPlayer mp2;
        private MediaPlayer mp3;
        public MediaPlayer mpToLoop;


        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            RequestWindowFeature(WindowFeatures.NoTitle);
            
            SetContentView(Resource.Layout.Main);


            // Get our button from the layout resource,
            // and attach an event to it
            mTorchOnOffButton = FindViewById<ImageButton>(Resource.Id.button_on_off);
            isTorchOn = false;

            if(!IsFlashAvailable())
            {
               

            }

            mCameraManager = (CameraManager)GetSystemService(Context.CameraService);
            try
            {
                mCameraId = mCameraManager.GetCameraIdList()[0];
            }
            catch (CameraAccessException es)
            {
                es.PrintStackTrace();
            }

            mTorchOnOffButton.Click += (object sender, EventArgs e) =>
             {
                 try
                 {
                     if (isTorchOn)
                     {
                         TurnOffFlashLight();
                         isTorchOn = false;
                         PlayOffSound();
                     }
                     else
                     {
                         TurnOnFlashLight();
                         PlayOnSound();
                         isTorchOn = true;
                     }
                 }
                 catch (Java.Lang.Exception eb)
                 {
                     eb.PrintStackTrace();
                 }

             };
         

            
            

            
        }
        private bool IsFlashAvailable()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
            
        }



        
        private void PlayOnSound()
        {
            mp = MediaPlayer.Create(this, Resource.Raw.fx4);
            mp3 = MediaPlayer.Create(this, Resource.Raw.saberftn);

            mp3.Looping = true;
            if(mp2!=null)
            {
                if (mp2.IsPlaying)
                {
                    mp2.Stop();
                }

            }

            // if(mp2.IsPlaying)
            // {
            //    mp2.Stop();
            //     mp2.Release();
            // }

            mp.Start();
            //mp.SetNextMediaPlayer(mp3);
            
          
        }
     
        private void PlayOffSound()
        {
            //if(mp3.IsPlaying)
           // {
            //    mp3.Stop();
                
           // }
           // mp3.Release();
            mp2 = MediaPlayer.Create(this, Resource.Raw.fx5);
            if(mp.IsPlaying)
              {
                mp.Stop();
                mp.Release();
            }
            
            mp2.Start();
        }
        public void TurnOnFlashLight()
        {
            try
            { if(Build.VERSION.SdkInt >= BuildVersionCodes.M)
                {
                    mCameraManager.SetTorchMode(mCameraId, true);
                    mTorchOnOffButton.SetImageResource(Resource.Drawable.green_lightsaber);
                }
            }catch(Java.Lang.Exception e)
            {
                e.PrintStackTrace();
                
            }

        }

        public void TurnOffFlashLight()
        {
            try
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
                {
                    mCameraManager.SetTorchMode(mCameraId, false);
                    PlayOffSound();

                    mTorchOnOffButton.SetImageResource(Resource.Drawable.green_lightsaberoff);
                }
            }catch (Java.Lang.Exception e)
            {
                e.PrintStackTrace();
            }
        }

        
        
  public void OnStop()
        {
            base.OnStop();
            if (isTorchOn)
            {
                TurnOffFlashLight();
                mp.Release();
                mp2.Release();
                mp3.Release();
            }
        }

        
    public void OnPause()
        {
            base.OnPause();
            if (isTorchOn)
            {
                TurnOffFlashLight();
            }
        }

       
    public void OnResume()
        {
            base.OnResume();
            if (isTorchOn)
            {
                TurnOnFlashLight();
            }
        }
    }
}


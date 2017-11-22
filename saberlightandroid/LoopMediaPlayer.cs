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
using Android.Media;
using Android.Util;
using Java.Lang;

namespace saberlightandroid
{
    public class LoopMediaPlayer
    {
        private Context mContext = null;
        private int mResId = 0;
        private int mCounter = 1;
        private MediaPlayer mCurrentPlayer = null;
        private MediaPlayer mNextPlayer = null;

        public static LoopMediaPlayer create(Context context, int resId)
        {
            return new LoopMediaPlayer(context, resId);
        }


        private LoopMediaPlayer(Context context, int resId)
        {
            mContext = context;
            mResId = resId;

            mCurrentPlayer = MediaPlayer.Create(mContext, mResId);

            mCurrentPlayer.Start();
            CreateNextMediaPlayer();
            
        }
        private void CreateNextMediaPlayer()
        {
            mNextPlayer = MediaPlayer.Create(mContext, mResId);
            mCurrentPlayer.SetNextMediaPlayer(mNextPlayer);
            
        }
        
    }
}
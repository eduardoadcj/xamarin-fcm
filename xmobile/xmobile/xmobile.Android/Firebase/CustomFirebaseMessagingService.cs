using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamarinFirebaseIntegration.Droid.Firebase
{
    [Service(Exported = false)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class CustomFirebaseMessagingService : FirebaseMessagingService 
    {

        private const string TAG = "CustomFirebaseMessagingService";

        public override void OnMessageReceived(RemoteMessage remoteMessage)
        {
            base.OnMessageReceived(remoteMessage);
            Log.Debug(TAG, "OnMessageReceived!");
        }

    }
}
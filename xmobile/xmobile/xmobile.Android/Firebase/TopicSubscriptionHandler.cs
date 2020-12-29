using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xmobile.Droid.Firebase
{
    public class TopicSubscriptionHandler
    {

        private const string TAG = "TopicSubscriptionHandler";
        private const string DEFAULT_TOPIC = "android";
        private FirebaseMessaging _firebaseMessaging;

        public TopicSubscriptionHandler()
        {
            _firebaseMessaging = FirebaseMessaging.Instance;
        }

        public void Init()
        {
            _firebaseMessaging.SubscribeToTopic(DEFAULT_TOPIC)
                .AddOnCompleteListener(new OnCompleteListener((task) =>
                {
                    string result = task.IsSuccessful 
                        ? "Default token successfully subscribed!" : "Fail to subscribe to default token!";
                    Log.Info(TAG, result);
                }));
        }

    }
}
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using Firebase.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xmobile.Droid.Services;

namespace XamarinFirebaseIntegration.Droid.Firebase
{
    [Service(Exported = false)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class CustomFirebaseMessagingService : FirebaseMessagingService 
    {

        private const string NOTIFICATION_CHANNEL_ID = "1001";
        private const string NOTIFICATION_CHANNEL_NAME = "XMobileNotificationChannel";
        private const string TAG = "CustomFirebaseMessagingService";

        public override void OnMessageReceived(RemoteMessage remoteMessage)
        {
            base.OnMessageReceived(remoteMessage);

            Log.Debug(TAG, "OnMessageReceived!");

            if (remoteMessage.Data.ContainsKey("Type") 
                && remoteMessage.Data["Type"] == "Sync_Request")
            { // Executes if came from the server.
                Intent serviceIntent = new Intent(this, typeof(ForegroundSimulationService));
                StartForegroundService(serviceIntent);
            }
            else
            { // Executes if came from firebase.
                var notification = remoteMessage.GetNotification();
                DispatchFirebaseNotification(notification.Title, notification.Body);
            }
        }

        private void DispatchFirebaseNotification(string title, string content)
        {
            NotificationChannel notificationChannel = new NotificationChannel(NOTIFICATION_CHANNEL_ID,
                NOTIFICATION_CHANNEL_NAME, NotificationImportance.Max);
            notificationChannel.EnableLights(true);

            NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.CreateNotificationChannel(notificationChannel);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(this, NOTIFICATION_CHANNEL_ID)
                .SetSmallIcon(Resource.Drawable.IcPopupReminder)
                .SetPriority(NotificationCompat.PriorityDefault)
                .SetContentTitle(title)
                .SetContentText(content);

            NotificationManagerCompat.From(this)
                .Notify(1000, builder.Build());
        }

    }
}
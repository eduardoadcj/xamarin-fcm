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

        private const string NOTIFICATION_CHANNEL_ID = "23432";
        private const string TAG = "CustomFirebaseMessagingService";

        public override void OnMessageReceived(RemoteMessage remoteMessage)
        {
            base.OnMessageReceived(remoteMessage);
            Log.Debug(TAG, "OnMessageReceived!");

            if (remoteMessage.Data.ContainsKey("Type") 
                && remoteMessage.Data["Type"] == "Sync_Request")
            {
                Intent serviceIntent = new Intent(this, typeof(ForegroundSimulationService));
                StartForegroundService(serviceIntent);
            }
            else
            {
                var notification = remoteMessage.GetNotification();
                DispatchNoticeNotification(notification.Title, notification.Body);
            }
        }

        private void DispatchNoticeNotification(string title, string content)
        {
            NotificationChannel notificationChannel = new NotificationChannel(NOTIFICATION_CHANNEL_ID,
                "canal", NotificationImportance.Max);
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
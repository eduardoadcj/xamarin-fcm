using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xmobile.Droid.Services
{
    [Service(Enabled = true)]
    public class ForegroundSimulationService : Service
    {

        private const string NOTIFICATION_CHANNEL_ID = "1001";
        private const string NOTIFICATION_CHANNEL_NAME = "XMobileNotificationChannel";
        private const int NOTIFICATION_ID = 1010;

        private const string TAG = "ForegroundSimulationService";

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            CreateForegroundNotification();

            Task.Run(async () =>
            {
                // Waits for 30 seconds.
                await Task.Delay(30000);
                StopSelf();
            });

            return StartCommandResult.Sticky;
        }

        private void CreateForegroundNotification()
        {
            NotificationChannel notificationChannel = new NotificationChannel(NOTIFICATION_CHANNEL_ID,
                NOTIFICATION_CHANNEL_NAME, NotificationImportance.Max);
            notificationChannel.EnableLights(true);

            NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.CreateNotificationChannel(notificationChannel);

            var builder = new NotificationCompat.Builder(this, NOTIFICATION_CHANNEL_ID);
            builder.SetSmallIcon(Resource.Drawable.notification_template_icon_bg)
               .SetPriority(NotificationCompat.PriorityHigh)
               .SetAutoCancel(false)
               .SetContentTitle("Foreground Notification")
               .SetContentText("Processing...")
               .SetOngoing(true)
               .SetProgress(0, 0, true)
               .SetOnlyAlertOnce(true);

            StartForeground(NOTIFICATION_ID, builder.Build());
        }

    }
}
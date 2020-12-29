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

        private NotificationManagerCompat _notificationManager;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            CreateNotificationChannel();

            Task.Run(async () =>
            {
                await Task.Delay(30000);
                StopSelf();
            });

            return StartCommandResult.Sticky;
        }

        private void CreateNotificationChannel()
        {
            NotificationChannel notificationChannel = new NotificationChannel("1001",
                "channel_name", NotificationImportance.Max);
            notificationChannel.EnableLights(true);

            NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.CreateNotificationChannel(notificationChannel);

            _notificationManager = NotificationManagerCompat.From(this);
            _notificationManager.CancelAll();

            var builder = new NotificationCompat.Builder(this, "1001");
            builder.SetSmallIcon(Resource.Drawable.notification_template_icon_bg)
               .SetPriority(NotificationCompat.PriorityHigh)
               .SetAutoCancel(false)
               .SetContentTitle("Primeiro Plano")
               .SetContentText("Processando...")
               .SetOngoing(true)
               .SetProgress(0, 0, true)
               .SetOnlyAlertOnce(true);

            StartForeground(1002, builder.Build());
        }

    }
}
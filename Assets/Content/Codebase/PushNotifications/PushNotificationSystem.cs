using System;
using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using Unity.Notifications.Android;

namespace Woodman.PushNotifications
{
    [EcsSystem(typeof(StartupModule))]
    public class PushNotificationSystem : IPreInitSystem, IDestroySystem
    {
        private const string MainChannel = "notifications.android.main_channel";

        public void PreInit()
        {
            #if UNITY_ANDROID
            AndroidNotificationCenter.CancelAllNotifications();
            var channel = CheckMainChannel();
            var push1000 = CreatePush1000();
            var push2000 = CreatePush2000();
            AndroidNotificationCenter.SendNotification(push1000, channel.Id);
            AndroidNotificationCenter.SendNotification(push2000, channel.Id);
            #endif
        }

        #if UNITY_ANDROID
        private static AndroidNotification CreatePush1000()
        {
            var time1000 = DateTime.Today + TimeSpan.FromHours(10);
            var now = DateTime.Now;
            if (now.Hour >= 10)
                time1000 += TimeSpan.FromDays(1);
            var push1000 = new AndroidNotification
            {
                Title = "It's time!",
                Text = "For finish house.",
                SmallIcon = "main_small",
                FireTime = time1000,
                ShouldAutoCancel = true
            };
            return push1000;
        }
        
        private static AndroidNotification CreatePush2000()
        {
            var time2000 = DateTime.Today + TimeSpan.FromHours(20);
            var now = DateTime.Now;
            if (now.Hour >= 20)
                time2000 += TimeSpan.FromDays(1);
            var push2000 = new AndroidNotification
            {
                Title = "A little bit more!",
                Text = "It's time to finish houses.",
                SmallIcon = "main_small",
                FireTime = time2000,
                ShouldAutoCancel = true
            };
            return push2000;
        }

        private static AndroidNotificationChannel CheckMainChannel()
        {
            var mainChannel = AndroidNotificationCenter.GetNotificationChannel(MainChannel);
            if (string.IsNullOrWhiteSpace(mainChannel.Id))
            {
                mainChannel = new AndroidNotificationChannel
                {
                    Id = MainChannel,
                    Description = "Description",
                    Importance = Importance.Default,
                    Name = "Notifications (Main)"
                };
                AndroidNotificationCenter.RegisterNotificationChannel(mainChannel);
            }

            return mainChannel;
        }
        #endif

        public void Destroy()
        {
        }
    }
}
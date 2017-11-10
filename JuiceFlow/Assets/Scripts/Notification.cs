using System;
using UnityEngine;

#if UNITY_ANDROID
namespace Assets.SimpleAndroidNotifications
{
    public class Notification : MonoBehaviour
    {
        public string title;
        public string message;
        public string ticker;

        void Start()
        {

            //Debug.Log("消息推送");
            var notificationParams = new NotificationParams
            {
                Id = UnityEngine.Random.Range(0, int.MaxValue),
                Delay = TimeSpan.FromSeconds(172800),
                //Delay = TimeSpan.FromSeconds(17),
                Title = "Bored out of your mind?",
				//标题
                Message = "Come back to PopGarden and enjoy the fun time!",
                //文本内容
				Ticker = "PopGarden",
				//缩略内容
                Sound = true,
                Vibrate = true,
                Light = true,
                SmallIcon = NotificationIcon.Heart,
                SmallIconColor = new Color(0, 0.5f, 0),
                LargeIcon = "app_icon"
            };

            //此处开始是保证只发送1次推送的处理
            var SendNotification = GameObject.Find("SendNotificationObj");
            if (SendNotification == null)
            {
                SendNotification = new GameObject();
                SendNotification.name = "SendNotificationObj";
                NotificationManager.SendCustom(notificationParams);
                DontDestroyOnLoad(SendNotification);
                //Debug.Log("消息推送-消息发送");
            }

        }
    }
}			
#endif
#if UNITY_ANDROID
using System;
using UnityEngine;


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
                Delay = TimeSpan.FromSeconds(86400),
                //Delay = TimeSpan.FromSeconds(17),
                Title = "Pon Pon Pon!!!",
				//标题
                Message = "Come back to Ice Blast and enjoy the fun time!",
                //文本内容
				Ticker = "Ice Blast",
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

#elif UNITY_IPHONE
using UnityEngine;
using System.Collections;

using NotificationServices = UnityEngine.iOS.NotificationServices;
using NotificationType = UnityEngine.iOS.NotificationType;
 
public class NewBehaviourScript : MonoBehaviour {
	//注册
	//public static void ScheLocalNotification(LocalNotification notification)


	//本地推送
	public static void NotificationMessage(string message,int hour ,bool isRepeatDay)
	{
		int year = System.DateTime.Now.Year;
		int month = System.DateTime.Now.Month;
		int day= System.DateTime.Now.Day;
		System.DateTime newDate = new System.DateTime(year,month,day,hour,0,0);
		NotificationMessage(message,newDate,isRepeatDay);
	}
	//本地推送 你可以传入一个固定的推送时间
	public static void NotificationMessage(string message,System.DateTime newDate,bool isRepeatDay)
	{
		//推送时间需要大于当前时间
		if(newDate > System.DateTime.Now)
		{
			UnityEngine.iOS.LocalNotification localNotification = new UnityEngine.iOS.LocalNotification();
			localNotification.fireDate = newDate;	
			localNotification.alertBody = message;
			localNotification.applicationIconBadgeNumber = 1;
			localNotification.hasAction = true;
			localNotification.alertAction = "Pop Garden Time!";
			if(isRepeatDay)
			{
				//是否每天定期循环
				localNotification.repeatCalendar = UnityEngine.iOS.CalendarIdentifier.ChineseCalendar;
				localNotification.repeatInterval = UnityEngine.iOS.CalendarUnit.Day;
			}
			localNotification.soundName = UnityEngine.iOS.LocalNotification.defaultSoundName;
			UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(localNotification);
		}
	}
 
	void Awake()
	{
		//获取权限
		UnityEngine.iOS.NotificationServices.RegisterForNotifications (
			NotificationType.Alert|
			NotificationType.Badge|
			NotificationType.Sound
		);

		//第一次进入游戏的时候清空，有可能用户自己把游戏冲后台杀死，这里强制清空
		CleanNotification();
	}

	void OnApplicationQuit()
	{
		//满生命后发送4500
		NotificationMessage("Q满生命后发送4500",System.DateTime.Now.AddSeconds(5),false);
		//次日发送86400
		NotificationMessage("Q次日发送86400",System.DateTime.Now.AddSeconds(10),false);
		//3日后发送259200
		NotificationMessage("Q3日后发送259200",System.DateTime.Now.AddSeconds(15),false);
		//7日后发送604800
		NotificationMessage("Q7日后发送604800",System.DateTime.Now.AddSeconds(20),false);
	}
 
	void OnApplicationPause(bool paused)
	{
		//程序进入后台时
		if(paused)
		{
			//满生命后发送4500
			NotificationMessage("P满生命后发送4500",System.DateTime.Now.AddSeconds(5),false);
			//次日发送86400
			NotificationMessage("P次日发送86400",System.DateTime.Now.AddSeconds(10),false);
			//3日后发送259200
			NotificationMessage("P3日后发送259200",System.DateTime.Now.AddSeconds(15),false);
			//7日后发送604800
			NotificationMessage("P7日后发送604800",System.DateTime.Now.AddSeconds(20),false);
			//每天中午12点推送
			//NotificationMessage("雨松MOMO : 每天中午12点推送",12,true);
		}
		else
		{
			//程序从后台进入前台时
			CleanNotification();
		}
	}
 
	//清空所有本地消息
	void CleanNotification()
	{
		UnityEngine.iOS.LocalNotification l = new UnityEngine.iOS.LocalNotification (); 
		l.applicationIconBadgeNumber = -1; 
		UnityEngine.iOS.NotificationServices.PresentLocalNotificationNow (l); 
		UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications (); 
		UnityEngine.iOS.NotificationServices.ClearLocalNotifications (); 
	}
}
#endif
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
 
public class NewBehaviourScript : MonoBehaviour {
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
			LocalNotification localNotification = new LocalNotification();
			localNotification.fireDate = newDate;	
			localNotification.alertBody = message;
			localNotification.applicationIconBadgeNumber = 1;
			localNotification.hasAction = true;
			if(isRepeatDay)
			{
				//是否每天定期循环
				localNotification.repeatCalendar = CalendarIdentifier.ChineseCalendar;
				localNotification.repeatInterval = CalendarUnit.Day;
			}
			localNotification.soundName = LocalNotification.defaultSoundName;
			NotificationServices.ScheduleLocalNotification(localNotification);
		}
	}
 
	void Awake()
	{
		//第一次进入游戏的时候清空，有可能用户自己把游戏冲后台杀死，这里强制清空
		CleanNotification();
	}
 
	void OnApplicationPause(bool paused)
	{
		//程序进入后台时
		if(paused)
		{
			//满生命后发送4500
			NotificationMessage("You got 5 lives now!Came back!Ice blast super fun!",System.DateTime.Now.AddSeconds(4500),true);
			//次日发送86400
			NotificationMessage("Come back to Ice Blast and enjoy the fun time!",System.DateTime.Now.AddSeconds(86400),true);
			//3日后发送259200
			NotificationMessage("Come back to Ice Blast and enjoy the fun time!",System.DateTime.Now.AddSeconds(259200),true);
			//7日后发送604800
			NotificationMessage("Come back to Ice Blast and enjoy the fun time!",System.DateTime.Now.AddSeconds(604800),true);
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
		LocalNotification l = new LocalNotification (); 
		l.applicationIconBadgeNumber = -1; 
		NotificationServices.PresentLocalNotificationNow (l); 
		NotificationServices.CancelAllLocalNotifications (); 
		NotificationServices.ClearLocalNotifications (); 
	}
}
#endif
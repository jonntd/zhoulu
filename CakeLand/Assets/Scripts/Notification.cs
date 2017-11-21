#if UNITY_ANDROID
using System;
using UnityEngine;
using Assets.SimpleAndroidNotifications;

public class Notification : MonoBehaviour
{
    void Awake()
    {
        //第一次进入游戏的时候清空，有可能用户自己把游戏冲后台杀死，这里强制清空
        CleanNotification();
    }

    /*
        void OnEnable()
        {
            Debug.Log("OnEnable");
            CleanNotification();
        }

        void OnGUI()
        {
            if (GUILayout.Button("5 SECONDS", GUILayout.Height(Screen.height * 0.2f)))
            {
                NotificationManager.Send(TimeSpan.FromSeconds(NotificationConfig.day3_time), "title", NotificationConfig.day3_text, new Color(1, 0.3f, 0.15f));
            }
        }
    */
    void OnApplicationPause(bool paused)
    {
        Debug.Log("OnApplicationPause" + paused);
        //程序进入后台时
        if (paused)
        {
            NotificationManager.Send(TimeSpan.FromSeconds(NotificationConfig.day3_time), NotificationConfig.NoticeTitle, NotificationConfig.day3_text, new Color(1, 0.3f, 0.15f));
            NotificationManager.Send(TimeSpan.FromSeconds(NotificationConfig.day7_time), NotificationConfig.NoticeTitle, NotificationConfig.day7_text, new Color(1, 0.3f, 0.15f));
            NotificationManager.Send(TimeSpan.FromSeconds(NotificationConfig.life_full_time), NotificationConfig.NoticeTitle, NotificationConfig.lift_full, new Color(1, 0.3f, 0.15f));
            NotificationManager.Send(TimeSpan.FromSeconds(NotificationConfig.next_day_time), NotificationConfig.NoticeTitle, NotificationConfig.next_day_text, new Color(1, 0.3f, 0.15f));
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
        NotificationManager.CancelAll();
    }
}
#elif UNITY_IPHONE

using UnityEngine;
using System.Collections;

using NotificationServices = UnityEngine.iOS.NotificationServices;
using NotificationType = UnityEngine.iOS.NotificationType;

public class Notification : MonoBehaviour {

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
localNotification.alertAction = NotificationConfig.NoticeTitle;
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
UnityEngine.iOS.NotificationServices.RegisterForNotifications (
NotificationType.Alert |
NotificationType.Badge |
NotificationType.Sound);
//第一次进入游戏的时候清空，有可能用户自己把游戏冲后台杀死，这里强制清空
CleanNotification();
}

void OnApplicationPause(bool paused)
{
//程序进入后台时
if(paused)
{
NotificationMessage(NotificationConfig.day3_text,System.DateTime.Now.AddSeconds(NotificationConfig.day3_time),false);
NotificationMessage(NotificationConfig.day7_text,System.DateTime.Now.AddSeconds(NotificationConfig.day7_time),false);
NotificationMessage(NotificationConfig.lift_full,System.DateTime.Now.AddSeconds(NotificationConfig.life_full_time),false);
NotificationMessage(NotificationConfig.next_day_text,System.DateTime.Now.AddSeconds(NotificationConfig.next_day_time),false);
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
#if UNITY_ANDROID
using System;
using UnityEngine;
using Assets.SimpleAndroidNotifications;

public class AndoridNotification : MonoBehaviour
{
    void Awake()
    {
        //第一次进入游戏的时候清空，有可能用户自己把游戏冲后台杀死，这里强制清空
        //第一次进入游戏的时候清空，有可能用户自己把游戏冲后台杀死，这里强制清空
        CleanNotification();
    }

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

    void OnApplicationPause(bool paused)
    {
        Debug.Log("OnApplicationPause" + paused);
        //程序进入后台时
        if (paused)
        {
           /* NotificationManager.Send(TimeSpan.FromSeconds(NotificationConfig.day3_time), "title", NotificationConfig.day3_text, new Color(1, 0.3f, 0.15f));
            NotificationManager.Send(TimeSpan.FromSeconds(NotificationConfig.day7_time), "title", NotificationConfig.day7_text, new Color(1, 0.3f, 0.15f));
            NotificationManager.Send(TimeSpan.FromSeconds(NotificationConfig.life_full_time), "title", NotificationConfig.lift_full, new Color(1, 0.3f, 0.15f));
            NotificationManager.Send(TimeSpan.FromSeconds(NotificationConfig.next_day_time), "title", NotificationConfig.next_day_text, new Color(1, 0.3f, 0.15f));*/
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

#endif

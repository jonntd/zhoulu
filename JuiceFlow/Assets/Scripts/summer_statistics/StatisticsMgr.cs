using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.summer
{
    public class StatisticsMgr : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            Debug.Log("===================开启=======================");
            //StatisticsManager.AppKeyAndChannelId("59c5fe4b07fe65541e000047", "Android");

#if UNITY_ANDROID
            StatisticsManager.AppKeyAndChannelId("59c5fe4b07fe65541e000047", "Googleplay");
            //调试时开启日志 发布时设置为false
            StatisticsManager.SetLogEnabled(false);
            //StatisticsManager.SetLogEnabled (true);	
#elif UNITY_IPHONE
        StatisticsManager.StartWithAppKeyAndChannelId("59c5fef14ad1567064000072", "AppStore");
		//调试时开启日志 发布时设置为false
		StatisticsManager.SetLogEnabled (false);
		//GA.SetLogEnabled (true);	
#endif
        }

        // Update is called once per frame
        void Update()
        {

        }
        //    public static string des = "hello";
        //    void OnGUI()
        //    {
        //        if (GUI.Button(new Rect(0, 0, 100, 100), des)) ;
        //    }
    }
}


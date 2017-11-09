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
            StatisticsManager.LoadEnterLevel();
          

            Debug.Log("===================开启=======================");
            //StatisticsManager.AppKeyAndChannelId("59c5fe4b07fe65541e000047", "Android");

#if UNITY_ANDROID
            StatisticsManager.AppKeyAndChannelId("59fddeabf43e4844330000e9", "Googleplay");
            //调试时开启日志 发布时设置为false
            StatisticsManager.SetLogEnabled(false);
            //StatisticsManager.SetLogEnabled (true);	
#elif UNITY_IPHONE
            StatisticsManager.AppKeyAndChannelId("59fddeecb27b0a26dd00024c", "AppStore");
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


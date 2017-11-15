using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationConfig
{

    public static int next_day_time = 60 * 60 * 24;
    public static string next_day_text = "次日推送-退出游戏24小时后发送消息";

    public static int day3_time = 5;//60 * 60 * 24 * 3;
    public static string day3_text = "3日推送-...";

    public static int day7_time = 60 * 60 * 24 * 7;
    public static string day7_text = "7日推送-...";

    public static int life_full_time = 15 * 60 * 5;
    public static string lift_full = "生命恢复推送-生命恢复满以后发送消息";
}


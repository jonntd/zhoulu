
using System;
using PureMVC.Patterns;
using System.Collections.Generic;
using UnityEngine;
namespace Summer
{
    public class DailyRewardProxy : Proxy
    {
        public const string PROXY = "DailyRewardProxy";



        public DailyRewardProxy() : base(PROXY)
        {

        }
    }

    public class TimeStampManager
    {
        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <param name="bflag">为真时获取10位时间戳,为假时获取13位时间戳.</param>
        /// <returns></returns>
        public static long GetTimeStamp(bool bflag = true)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long ret;
            if (bflag)
                ret = Convert.ToInt64(ts.TotalSeconds);
            else
                ret = Convert.ToInt64(ts.TotalMilliseconds);
            return ret;
        }


        static DateTime dt_start = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        public static string NormalizeTimpstamp0(long timpStamp)
        {
            long unix_time = timpStamp * 10000000L;
            TimeSpan to_now = new TimeSpan(unix_time);
            DateTime dt = dt_start.Add(to_now);
            return dt.ToString("yyyy-mm-dd");
        }




        /// <summary>
        /// 时钟式倒计时
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public string GetSecondString(int second)
        {
            return string.Format("{0:D2}", second / 3600) + string.Format("{0:D2}", second % 3600 / 60) + ":" + string.Format("{0:D2}", second % 60);
        }

        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// <param name="d">double 型数字</param>
        /// <returns>DateTime</returns>
        /// </summary>
        public static DateTime ConvertIntDateTime(double d)
        {
            DateTime time = DateTime.MinValue;
            DateTime start_time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
            time = start_time.AddSeconds(d);
            return time;
        }


        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>double</returns>
        public static double ConvertDateTimeInt(System.DateTime time)
        {
            double int_result = 0;
            DateTime start_time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            int_result = (time - start_time).TotalSeconds;
            return int_result;
        }




        /// <summary>
        /// 日期转换成unix时间戳
        /// </summary>
        public static long DateTimeToUnixTimestamp(DateTime date_time)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, date_time.Kind);
            return Convert.ToInt64((date_time - start).TotalSeconds);
        }


        /// <summary>
        /// unix时间戳转换成日期
        /// </summary>
        public static DateTime UnixTimestampToDateTime(DateTime date_time, long times_tamp)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, date_time.Kind);
            return start.AddSeconds(times_tamp);
        }
    }
}

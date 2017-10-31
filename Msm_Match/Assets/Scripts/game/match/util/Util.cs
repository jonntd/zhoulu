using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Game
{
    public class Util
    {

        //目录
        public const string ResourcesPrefab = "Prefabs/";
        //文件名称
        public const string Item = "Item";

        //动画参数名称
        public const string Pressed = "Pressed";
        public const string Exit = "Exit";


        /// <summary>
        ///  获取鼠标滑动方向 判断按下，抬起之间的xy的比例来判断方向，八分四方向
        /// </summary>
        /// <param name="end"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static Vector2 GetDirection(Vector3 end, Vector3 start)
        {
            // 1.方向向量
            Vector3 dir = end - start;
            // 2.如果是横向滑动
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                //返回横向坐标
                return new Vector2(dir.x / Mathf.Abs(dir.x), 0);
            }
            else
            {
                //返回纵向坐标
                return new Vector2(0, dir.y / Mathf.Abs(dir.y));
            }
        }
    }
}



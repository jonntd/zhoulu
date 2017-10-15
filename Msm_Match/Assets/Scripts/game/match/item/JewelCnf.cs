using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Game
{
    [System.Serializable]
    public class JewelCnf
    {
        public E_JewelType type = E_JewelType.none;         // 宝石类型
        public Vector2 position;                            // 地块位置
        public E_JewelEffect effect;                        // 宝石作用
    }

    /// <summary>
    /// 地块类型
    /// </summary>
    public enum E_JewelType
    {
        none,
        jewel_01,
        jewel_02,
        jewel_03,
        jewel_04,
        jewel_05,
        jewel_06,
        jewel_07,
        jewel_08,
        max,
    }

    public enum E_JewelEffect
    {
        none,
        normal,
    }
}


using UnityEngine;

namespace Summer.Game
{
    /// <summary>
    /// 地块
    /// </summary>
    [System.Serializable]
    public class CellCnf
    {
        public E_CellType cell_type = E_CellType.none;          // 地块类型
        public Vector2 cell_position;                           // 地块位置
        public E_CellEffect cell_effect = E_CellEffect.none;    // 播放特效

    }

    /// <summary>
    /// 地块类型
    /// </summary>
    public enum E_CellType
    {
        none,
        blue,
        gray,
        red,
        tranf,
        max,
    }

    /// <summary>
    /// 地块特效
    /// </summary>
    public enum E_CellEffect
    {
        none,
        max,
    }
}


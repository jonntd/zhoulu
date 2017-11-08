using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Game
{
    /// <summary>
    /// 地表格子信息
    /// </summary>
    public class GridInfo : TiledInfo
    {


        public int Iindex { get; private set; }     // ij的计算下标
        public int _i_row;
        public int _i_col;
        public int _type;


        public GridInfo(int i_row, int i_col, int type) : base(i_row, i_col)
        {
            _type = type;
        }
    }
}


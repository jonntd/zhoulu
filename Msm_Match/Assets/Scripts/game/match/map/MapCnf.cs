using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Game
{
    /// <summary>
    /// 地图数据
    /// </summary>
    public class MapCnf
    {
        public int row_num;             //行
        public int col_num;             //列
        public int map_id;
        public int[][] map_cnf;     //行、列

        public MapCnf(int id)
        {
            map_id = id;
        }
    }
}


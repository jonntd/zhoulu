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
        public static string[] SPLIT01 = new string[] { "\r\n" };
        public static string[] SPLIT02 = new string[] { "," };
        public int row;             //行
        public int col;             //列
        public int map_id;
        public int[][] map_cnf;     //行、列

        public MapCnf(int id)
        {
            map_id = id;
            _load_cnf();
        }

        public void _load_cnf()
        {
            TextAsset text_asset = ResManager.instance.LoadAsset<TextAsset>(map_id + "", E_GameResType.map);
            string[] lines = text_asset.text.Split(SPLIT01, StringSplitOptions.RemoveEmptyEntries);

            row = lines.Length;
            map_cnf = new int[row][];
            for (int irow = 0; irow < row; irow++)
            {
                string[] tmp = lines[irow].Split(SPLIT02, StringSplitOptions.RemoveEmptyEntries);
                col = tmp.Length;
                map_cnf[irow] = new int[col];
                for (int icol = 0; icol < col; icol++)
                {
                    map_cnf[irow][icol] = int.Parse(tmp[icol]);
                }
            }

            Debug.Log(map_cnf);
        }
    }


}


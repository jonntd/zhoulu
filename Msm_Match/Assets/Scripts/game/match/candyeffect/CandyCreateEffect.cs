using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Game
{
    /// <summary>
    /// 创建一个东西
    /// </summary>
    public class CandyCreateEffect : I_CandyEffect
    {
        public void OnExcute(CandyInfo info)
        {
            LogManager.Assert(info.Alive, "宿主没有死亡");
            if (info.Alive) return;

            // 1.合法性验证

            // 2.得到CandyInfo

            // 3.创建一个东西
            LogManager.Log("Row:[{0}],Col:[{1}] 创建一个新的元素", info.ItemRow, info.ItemCol, "107");

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Game
{
    /// <summary>
    /// 死亡之后对周围上下左右进行消除格
    /// </summary>
    public class DeadEffect : I_CandyEffect
    {
        public void OnExcute(CandyInfo info)
        {
            OnExcuteSingle(info.ItemRow - 1, info.ItemCol);
            OnExcuteSingle(info.ItemRow + 1, info.ItemCol);
            OnExcuteSingle(info.ItemRow, info.ItemCol - 1);
            OnExcuteSingle(info.ItemRow, info.ItemCol + 1);
        }

        private void OnExcuteSingle(int row, int col)
        {
            // 1.合法性验证

            // 2.得到CandyInfo
            CandyInfo info = new CandyInfo(100,100);
            // 3.进行消除行为
            if (info == null) return;
            info.EliminateHealth();
        }
    }

}


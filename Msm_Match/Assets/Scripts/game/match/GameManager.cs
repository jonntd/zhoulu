
using UnityEngine;
namespace Summer.Game
{
    public class GameManager
    {
        public static GameManager Instance = new GameManager();
        public int _col;                         //列
        public int _row;                         //行

        /// <summary>
        /// 根据糖果类型对匹配列表进行分组
        /// </summary>
        public void GroupMatchCandys()
        {

        }

        /// <summary>
        /// 检测是否会产生特殊糖果(分组后的待消除糖果)
        /// </summary>
        public void CheckSpecialCandy()
        {

        }

        /// <summary>
        /// 根据特殊糖果记录列表添加特殊糖果
        /// </summary>
        public void AddSpecialCandy()
        {

        }

        /// <summary>
        /// 获取同一列的糖果
        /// </summary>
        public void GetSameCol(CandyInfo info)
        {

        }

        /// <summary>
        /// 获取同一行的糖果
        /// </summary>
        public void GetSameRow(CandyInfo info)
        {

        }

        /// <summary>
        /// 获取九宫
        /// </summary>
        /// <param name="info"></param>
        public void GetNineGrid(CandyInfo info)
        {

        }

        /// <summary>
        /// 获取某一位置的糖果
        /// </summary>
        /// <param name="row">第几行</param>
        /// <param name="col">第几列</param>
        public void GetCandyByColRow(int row, int col)
        {

        }

    }
}

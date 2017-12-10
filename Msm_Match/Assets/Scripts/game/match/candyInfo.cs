
namespace Summer.Game
{
    /// <summary>
    /// 糖果信息
    /// </summary>
    public class CandyInfo : TiledInfo
    {
        protected E_CandyType _type;                            // 糖果类型
        public bool can_move;                                   // 是否可以移动

        public bool CanRemove { get; private set; }             // 可以消除
        public bool alive;                                      // 存活

        public CandyInfo(int i_row, int i_col) : base(i_row, i_col)
        {
            can_move = true;
            CanRemove = true;
            alive = true;
        }

        /// <summary>
        /// 糖果匹配成功
        /// </summary>
        public void MatchCandy()
        {

        }

       
    }
}

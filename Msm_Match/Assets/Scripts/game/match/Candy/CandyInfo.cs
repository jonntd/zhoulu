
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
        public bool Alive { get; protected set; }               // 存活

        protected I_CandyEffect dead_effect;

        public CandyInfo(int i_row, int i_col) : base(i_row, i_col)
        {
            can_move = true;
            CanRemove = true;
            Alive = true;
        }

        // 拥有匹配功能
        public bool HsaMatch() { return true; }

        // 糖果匹配成功
        public void MatchCandy()
        {

        }

        public virtual void EliminateHealth()
        {

        }

        public virtual void DeadEffect()
        {
            if (dead_effect != null)
                dead_effect.OnExcute(this);
        }
    }
}



namespace Summer.Game
{
    /// <summary>
    /// 横向
    /// </summary>
    public class CandyStrevkH : I_CandyEffect
    {
        public void OnExcute(CandyInfo info)
        {
            GameManager.Instance.GetSameCol(info);
        }
    }
}

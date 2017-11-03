
namespace Summer.Game
{
    public class CandyNineGrid : I_CandyEffect
    {
        public void OnExcute(CandyInfo info)
        {
            GameManager.Instance.GetNineGrid(info);
        }
    }
}

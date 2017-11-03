

namespace Summer.Game
{
    public enum E_CandyType
    {
        one,
        two,
        three,
        four,
        five,
        six,
    }

    /// <summary>
    /// 消除特效
    /// </summary>
    public enum E_ClearType
    {
        normal = 0,         // 普通
        streak_h = 1,       // 横纹
        streak_v = 2,       // 纵纹
        cross = 3,          // 十字
        nine_grid = 4,      // 九宫
        color_ful = 5       // 彩色糖果
    }
}

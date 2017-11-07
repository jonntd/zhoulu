
namespace Summer.Game
{

    public class TiledInfo
    {
        public const int MAX_ROW_COUNT = 8;                         // 一行有多少个格子   
        public const int WITDH = 78;
        public const int HEIGHT = 78;
        public const int OFFSET_X = -78 * 4 + 39;
        public const int OFFSET_Y = -250;

        public int itemRow { get { return _item_row; } }
        public int itemCol { get { return _item_col; } }
        public int itemX { get { return _item_x; } }
        public int itemY { get { return _item_y; } }
        public int Index { get { return _index; } }
        public int _item_row;                                       // 第几行
        public int _item_col;                                       // 第几列
        public int _item_x;
        public int _item_y;
        public int _index;
        public TiledInfo(int i_row, int i_col)
        {
            _item_row = i_row;
            _item_col = i_col;
            _index = _item_row * MAX_ROW_COUNT + _item_col;

            _item_x = _item_col * WITDH + OFFSET_X;
            _item_y = _item_row * HEIGHT + OFFSET_Y;
        }
    }

}

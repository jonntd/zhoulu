
namespace Summer.Game
{

    public class TiledInfo
    {
        public const int MAX_ROW_COUNT = 8;                         // 一行有多少个格子   
        public const int WITDH = 78;
        public const int HEIGHT = 78;
        public const int OFFSET_X = -WITDH * 4 + WITDH / 2;
        public const int OFFSET_Y = 300;

        public int ItemRow { get { return _item_row; } }
        public int ItemCol { get { return _item_col; } }
        public int ItemPosX { get { return _item_pos_x; } }
        public int ItemPosY { get { return _item_pos_y; } }
        public int Index { get { return _index; } }
        public int _item_row;                                       // 第几行
        public int _item_col;                                       // 第几列
        public int _item_pos_x;
        public int _item_pos_y;
        public int _index;
        public TiledInfo(int i_row, int i_col)
        {
            _item_row = i_row;
            _item_col = i_col;
            _index = _item_row * MAX_ROW_COUNT + _item_col;

            _item_pos_x = _item_col * WITDH + OFFSET_X;
            _item_pos_y = -_item_row * HEIGHT + OFFSET_Y;
        }

        #region static function

        public static int FindIndexByRowCol(int row, int col)
        {
            int tmp_index = row * MAX_ROW_COUNT + col;
            return tmp_index;
        }

        #endregion

        public void ChangeRowCol(int row, int col)
        {
            _item_row = row;
            _item_col = col;
            _index = _item_row * MAX_ROW_COUNT + _item_col;
        }
    }

}

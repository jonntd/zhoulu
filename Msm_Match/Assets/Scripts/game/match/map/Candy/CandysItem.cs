using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Summer.Game
{

    public class CandysItem : MonoBehaviour, IMoveHandler, IPointerDownHandler, IDragHandler
    {
        public const float move_time = 0.2f;
        public Dictionary<int, CandyItem> _grid_map = new Dictionary<int, CandyItem>();

        public bool in_control = false;
        private Transform trans;

        #region Mono
        void Awake()
        {
            trans = GetComponent<Transform>();
        }
        // Use this for initialization
        void Start()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    CandyItem item = ItemFactory.Instance.PopCandyItem();

                    item.name = row + "_" + col;
                    item.transform.parent = trans;
                    item.transform.localScale = Vector3.one;

                    CandyInfo info = new CandyInfo(row, col);
                    item.SetInfo(info);
                    _grid_map.Add(item.info.Index, item);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        #region

        public void OnPointerDown(PointerEventData event_data)
        {
            LogManager.Log("OnPointerDown");
        }

        public void OnMove(AxisEventData event_data)
        {
            LogManager.Log("OnMove");
        }

        public void OnDrag(PointerEventData eventData)
        {
            LogManager.Log("OnDrag");
        }
        #endregion

        public void Move(CandyItem source, int row, int col)
        {
            int index = TiledInfo.FindIndexByRowCol(row, col);
            if (!_grid_map.ContainsKey(index))
            {
                LogManager.Error("找不到对应格子的糖果。Row[{0}] Col[{1}]", row, col);
                return;
            }

            CandyItem target = _grid_map[index];
            Move(source, target);
        }

        /// <summary>
        /// 双方交换目标
        /// </summary>
        public void Move(CandyItem source, CandyItem target)
        {
            LogManager.Log("Source:[{0}], Target:[{1}]", source.ToDes(), target.ToDes());
            // 原始的row col 以及坐标
            int source_row = source.info.ItemRow;
            int source_col = source.info.ItemCol;
            Vector3 source_pos = source.transform.position;

            // 目标的row col 以及坐标
            int target_row = target.info.ItemRow;
            int target_col = target.info.ItemCol;
            Vector3 target_pos = target.transform.position;

            // 清空集合中的位置
            _grid_map.Remove((source.info.Index));
            _grid_map.Remove((target.info.Index));

            // 改变数据
            source.info.ChangeRowCol(target_row, target_col);
            target.info.ChangeRowCol(source_row, source_col);
            _grid_map.Add(source.info.Index, source);
            _grid_map.Add(target.info.Index, target);

            // 显示修改
            source.transform.DOMove(target_pos, move_time);
            target.transform.DOMove(source_pos, move_time);
        }
    }
}


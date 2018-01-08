using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Summer.Game
{
    /// <summary>
    /// 移动功能
    /// </summary>
    public class MoveEffect : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
    {
        public static int move_effect_index = 0;
        private CandyItem item;
        private Vector3 downPos;            //按下的鼠标坐标
        private Vector3 upPos;              //抬起的鼠标坐标 

        public IEnumerator enumerator;
        void Awake()
        {
            item = GetComponent<CandyItem>();
        }

        #region 点击事件

        public static bool move_donw = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            GameController.instance.SetOperation(true);
            GameController.instance.AddCandy(item);
            if (GameController.instance.CanMove())
            {
                GameController.instance.Move();
            }
            LogManager.Log("OnPointerDown " + this);
            move_donw = true;
            MoveManager.instance.AddItem(this);
            if (MoveManager.instance.CanExchange())
            {
                MoveManager.instance.ExcuteExchange();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            move_donw = false;
            /*// 1.如果其他人正在操作 返回
            if (GameController.instance.isOperation)
                return;
            // 2.正在操作
            GameController.instance.isOperation = true;
            upPos = Input.mousePosition;
            // 3.获取方向
            Vector2 dir = Util.GetDirection(upPos, downPos);

            // 这里其实是有异常的，比如点下，抬起的距离太近
            // 太近或者太远
            //点击异常处理
            if (!MathHelper.IsEqualFloat(dir.magnitude, 1f))
            {
                GameController.instance.isOperation = false;
                return;
            }

            enumerator = ItemExchange(dir);
            StopCoroutine(enumerator);
            //开启协程
            StartCoroutine(enumerator);
            */
            upPos = Input.mousePosition;
            Vector2 dir = Util.GetDirection(upPos, downPos);
            enumerator = ItemExchange(dir);
            StartCoroutine(enumerator);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            GameController.instance.AddCandy(item);
            if (GameController.instance.CanMove())
            {
                GameController.instance.Move();
            }

            LogManager.Log("OnPointerEnter " + this + "move_donw:" + move_donw);
            if (!move_donw) return;
            MoveManager.instance.AddItem(this);
            if (MoveManager.instance.CanExchange())
            {
                MoveManager.instance.ExcuteExchange();
            }
        }

        #endregion

        /// <summary>
        /// Item交换
        /// </summary>
        /// <returns>The exchange.</returns>
        /// <param name="dir">Dir.</param>
        IEnumerator ItemExchange(Vector2 dir)
        {
            LogManager.Assert(move_effect_index == 0, "移动操作不合法[{0}]", move_effect_index);

            move_effect_index++;
            int targetRow = item.info.ItemRow - System.Convert.ToInt32(dir.y);
            int targetColumn = item.info.ItemCol - System.Convert.ToInt32(dir.x);
            BattleController.Instance.candy_map.Move(item, targetRow, targetColumn);
            yield return new WaitForSeconds(CandysItem.move_time);//;
            /*  //获取目标行列
              int targetRow = item.ItemRow + System.Convert.ToInt32(dir.y);
              int targetColumn = item.itemColumn + System.Convert.ToInt32(dir.x);
              //检测合法
              bool isLagal = GameController.instance.CheckRCLegal(targetRow, targetColumn);
              if (!isLagal)
              {
                  GameController.instance.isOperation = false;
                  //不合法跳出
                  yield break;
              }
              //获取目标
              Item target = GameController.instance.allItems[targetRow, targetColumn];


              //从全局列表中获取当前item，查看是否已经被消除，被消除后不能再交换
              Item myItem = GameController.instance.allItems[item.ItemRow, item.itemColumn];
              if (!target || !myItem)
              {
                  GameController.instance.isOperation = false;
                  //Item已经被消除
                  yield break;
              }
              //相互移动
              target.GetComponent<ItemOperation>().ItemMove(item.ItemRow, item.itemColumn, transform.position);
              ItemMove(targetRow, targetColumn, target.transform.position);
              //还原标志位
              bool reduction = false;
              //消除处理
              item.CheckAroundBoom();
              if (GameController.instance.boomList.Count == 0)
              {
                  reduction = true;
              }
              target.CheckAroundBoom();
              if (GameController.instance.boomList.Count != 0)
              {
                  reduction = false;
              }

              //还原
              if (reduction)
              {
                  //延迟
                  yield return new WaitForSeconds(0.2f);
                  //临时行列
                  int tempRow, tempColumn;
                  tempRow = myItem.ItemRow;
                  tempColumn = myItem.itemColumn;
                  //移动
                  myItem.GetComponent<ItemOperation>().ItemMove(target.ItemRow,
                      target.itemColumn, target.transform.position);
                  target.GetComponent<ItemOperation>().ItemMove(tempRow,
                      tempColumn, myItem.transform.position);
                  //延迟
                  yield return new WaitForSeconds(0.2f);
                  //操作完毕
                  GameController.instance.isOperation = false;
              }*/
            move_effect_index--;
            yield break;
        }

        public void ItemMove(int targetRow, int targetColumn, Vector3 pos)
        {
            /* //改行列
             item.temRow = targetRow;
             item.itemColumn = targetColumn;
             //改全局列表
             GameController.instance.allItems[targetRow, targetColumn] = item;
             //移动
             transform.DOMove(pos, 0.2f);*/
        }

        public void CurrentItemDrop(Vector3 pos)
        {
            //下落
            transform.DOMove(pos, 0.2f);
        }
    }
}


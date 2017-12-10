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
    public class MoveEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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

        public void OnPointerDown(PointerEventData eventData)
        {
            downPos = Input.mousePosition;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
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
            int targetRow = item.info.itemRow - System.Convert.ToInt32(dir.y);
            int targetColumn = item.info.itemCol - System.Convert.ToInt32(dir.x);
            BattleController.Instance.candy_map.Move(item, targetRow, targetColumn);
            yield return new WaitForSeconds(CandysItem.move_time);//;
            /*  //获取目标行列
              int targetRow = item.itemRow + System.Convert.ToInt32(dir.y);
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
              Item myItem = GameController.instance.allItems[item.itemRow, item.itemColumn];
              if (!target || !myItem)
              {
                  GameController.instance.isOperation = false;
                  //Item已经被消除
                  yield break;
              }
              //相互移动
              target.GetComponent<ItemOperation>().ItemMove(item.itemRow, item.itemColumn, transform.position);
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
                  tempRow = myItem.itemRow;
                  tempColumn = myItem.itemColumn;
                  //移动
                  myItem.GetComponent<ItemOperation>().ItemMove(target.itemRow,
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


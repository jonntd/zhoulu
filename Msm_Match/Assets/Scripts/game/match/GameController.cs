using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Summer.Game
{
    public class GameController : MonoBehaviour
    {

        public static GameController instance;              //单例
        public int tableRow, tableColumn;
        public Item[,] allItems; //所有的Item

        public Vector3[,] allPos;                           //所有Item的坐标

        public List<Item> sameItemsList;                 //相同Item列表

        public List<Item> boomList;             //要消除的Item列表

        public Color randomColor;               //随机颜色

        public bool isOperation = false;         //正在操作

        public bool allBoom = false;                //是否正在执行AllBoom

        #region Override MonoBehaviour

        private void Awake()
        {
            instance = this;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        void AllBoom()
        {
            // 1.有消除
            bool hasBoom = false;
            foreach (var item in allItems)
            {
                // 2.指定位置的Item存在，且没有被检测过
                if (item && !item.hasCheck)
                {
                    // 3.检测周围的消除
                    item.CheckAroundBoom();
                    if (boomList.Count > 0)
                    {
                        hasBoom = true;
                        isOperation = true;
                    }
                }
            }
            if (!hasBoom)
            {
                // 操作结束
                isOperation = false;
            }
        }

        //填充相同Item列表
        public void FillSameItemsList(Item current)
        {
            // 1.如果已存在，跳过
            if (sameItemsList.Contains(current))
                return;
            // 2.添加到列表
            sameItemsList.Add(current);
            // 3.上下左右的Item
            Item[] tempItemList = new Item[]{
            GetUpItem(current),GetDownItem(current),
            GetLeftItem(current),GetRightItem(current)};
            for (int i = 0; i < tempItemList.Length; i++)
            {
                // 4.如果Item不合法，跳过
                if (tempItemList[i] == null) continue;

                // 5.同一类型
                if (current.currentSpr == tempItemList[i].currentSpr)
                {
                    FillSameItemsList(tempItemList[i]);
                }
            }
        }

        // 填充待消除列表
        public void FillBoomList(Item current)
        {
            //计数器
            int rowCount = 0;
            int columnCount = 0;
            //临时列表
            List<Item> rowTempList = new List<Item>();
            List<Item> columnTempList = new List<Item>();
            ///横向纵向检测
            foreach (var item in sameItemsList)
            {

                //如果在同一行
                if (item.itemRow == current.itemRow)
                {
                    //判断该点与Curren中间有无间隙
                    bool rowCanBoom = CheckItemsInterval(true, current, item);
                    if (rowCanBoom)
                    {
                        //计数
                        rowCount++;
                        //添加到行临时列表
                        rowTempList.Add(item);
                    }
                }
                //如果在同一列
                if (item.itemColumn == current.itemColumn)
                {
                    //判断该点与Curren中间有无间隙
                    bool columnCanBoom = CheckItemsInterval(false, current, item);
                    if (columnCanBoom)
                    {
                        //计数
                        columnCount++;
                        //添加到列临时列表
                        columnTempList.Add(item);
                    }
                }
            }
            //横向消除
            bool horizontalBoom = false;
            //如果横向三个以上
            if (rowCount > 2)
            {
                //将临时列表中的Item全部放入BoomList
                boomList.AddRange(rowTempList);
                //横向消除
                horizontalBoom = true;
            }
            //如果纵向三个以上
            if (columnCount > 2)
            {
                if (horizontalBoom)
                {
                    //剔除自己
                    boomList.Remove(current);
                }
                //将临时列表中的Item全部放入BoomList
                boomList.AddRange(columnTempList);
            }
            //如果没有消除对象，返回
            if (boomList.Count == 0)
                return;
            //创建临时的BoomList
            List<Item> tempBoomList = new List<Item>();
            //转移到临时列表
            tempBoomList.AddRange(boomList);
            IEnumerator manipulate_cor = ManipulateBoomList(tempBoomList);

            //开启处理BoomList的协程
            StartCoroutine(manipulate_cor);
        }

        /// <summary>
        /// 处理BoomList
        /// </summary>
        /// <returns>The boom list.</returns>
        IEnumerator ManipulateBoomList(List<Item> tempBoomList)
        {
            foreach (var item in tempBoomList)
            {
                item.hasCheck = true;
                item.GetComponent<Image>().color = randomColor * 2;
                //离开动画
                item.GetComponent<AnimatedButton>().Exit();
                //Boom声音
                //AudioManager.instance.PlayMagicalAudio();
                //将被消除的Item在全局列表中移除
                allItems[item.itemRow, item.itemColumn] = null;
            }
            //检测Item是否已经开发播放离开动画
            while (!tempBoomList[0].GetComponent<AnimatedButton>().CheckPlayExit())
            {
                yield return 0;
            }

            //延迟0.2秒
            yield return new WaitForSeconds(0.2f);
            //开启下落
            yield return StartCoroutine(ItemsDrop());
            //延迟0.38秒
            yield return new WaitForSeconds(0.38f);

            foreach (var item in tempBoomList)
            {
                //回收Item
                //ObjectPool.instance.SetGameObject(item.gameObject);
            }
        }

        /// <summary>
        /// Items下落
        /// </summary>
        /// <returns>The drop.</returns>
        IEnumerator ItemsDrop()
        {
            isOperation = true;
            //逐列检测
            for (int i = 0; i < tableColumn; i++)
            {
                //计数器
                int count = 0;
                //下落队列
                Queue<Item> dropQueue = new Queue<Item>();
                //逐行检测
                for (int j = 0; j < tableRow; j++)
                {
                    if (allItems[j, i] != null)
                    {
                        //计数
                        count++;
                        //放入队列
                        dropQueue.Enqueue(allItems[j, i]);
                    }
                }
                //下落
                for (int k = 0; k < count; k++)
                {
                   /* //获取要下落的Item
                    Item current = dropQueue.Dequeue();
                    //修改全局数组(原位置情况)
                    allItems[current.itemRow, current.itemColumn] = null;
                    //修改Item的行数
                    current.itemRow = k;
                    //修改全局数组(填充新位置)
                    allItems[current.itemRow, current.itemColumn] = current;
                    //下落
                    current.GetComponent<ItemOperation>().
                        CurrentItemDrop(allPos[current.itemRow, current.itemColumn]);*/
                }
            }

            yield return new WaitForSeconds(0.2f);

            StartCoroutine(CreateNewItem());
            yield return new WaitForSeconds(0.2f);
            AllBoom();
        }
        /// <summary>
        /// 生成新的Item
        /// </summary>
        /// <returns>The new item.</returns>
        public IEnumerator CreateNewItem()
        {
            isOperation = true;
            for (int i = 0; i < tableColumn; i++)
            {
                int count = 0;
                Queue<GameObject> newItemQueue = new Queue<GameObject>();
                for (int j = 0; j < tableRow; j++)
                {
                    if (allItems[j, i] == null)
                    {
                        //生成一个Item
                        GameObject current = (GameObject)Instantiate(Resources.
                            Load<GameObject>(Util.ResourcesPrefab + Util.Item));
                        //						ObjectPool.instance.GetGameObject (Util.Item, transform);
                        current.transform.parent = transform;
                        current.transform.position = allPos[tableRow - 1, i];
                        newItemQueue.Enqueue(current);
                        count++;
                    }
                }
                for (int k = 0; k < count; k++)
                {
                   /* //获取Item组件
                    Item currentItem = newItemQueue.Dequeue().GetComponent<Item>();
                    //随机数
                    int random = Random.Range(0, randomSprites.Length);
                    //修改脚本中的图片
                    currentItem.currentSpr = randomSprites[random];
                    //修改真实图片
                    currentItem.currentImg.sprite = randomSprites[random];
                    //获取要移动的行数
                    int r = tableRow - count + k;
                    //移动
                    currentItem.GetComponent<ItemOperation>().ItemMove(r, i, allPos[r, i]);*/
                }
            }
            yield break;
        }


        #region 取得周围的item

        //获取上方Item
        private Item GetUpItem(Item current)
        {
            int row = current.itemRow + 1;
            int column = current.itemColumn;
            if (!CheckRCLegal(row, column))
                return null;
            return allItems[row, column];
        }

        //获取下方Item
        private Item GetDownItem(Item current)
        {
            int row = current.itemRow - 1;
            int column = current.itemColumn;
            if (!CheckRCLegal(row, column))
                return null;
            return allItems[row, column];
        }

        //获取左方Item
        private Item GetLeftItem(Item current)
        {
            int row = current.itemRow;
            int column = current.itemColumn - 1;
            if (!CheckRCLegal(row, column))
                return null;
            return allItems[row, column];
        }

        //获取右方Item
        private Item GetRightItem(Item current)
        {
            int row = current.itemRow;
            int column = current.itemColumn + 1;
            if (!CheckRCLegal(row, column))
                return null;
            return allItems[row, column];
        }
        #endregion

        //检测行列是否合法
        public bool CheckRCLegal(int itemRow, int itemColumn)
        {
            if (itemRow >= 0 && itemRow < tableRow && itemColumn >= 0 && itemColumn < tableColumn)
                return true;
            return false;
        }

        /// <summary>
        /// 检测两个Item之间是否有间隙（图案不一致）
        /// </summary>
        /// <param name="isHorizontal">是否是横向.</param>
        /// <param name="begin">检测起点.</param>
        /// <param name="end">检测终点.</param>
        private bool CheckItemsInterval(bool isHorizontal, Item begin, Item end)
        {
            //获取图案
            Sprite spr = begin.currentSpr;
            //如果是横向
            if (isHorizontal)
            {
                //起点终点列号
                int beginIndex = begin.itemColumn;
                int endIndex = end.itemColumn;
                //如果起点在右，交换起点终点列号
                if (beginIndex > endIndex)
                {
                    beginIndex = end.itemColumn;
                    endIndex = begin.itemColumn;
                }
                //遍历中间的Item
                for (int i = beginIndex + 1; i < endIndex; i++)
                {
                    //异常处理（中间未生成，标识为不合法）
                    if (allItems[begin.itemRow, i] == null)
                        return false;
                    //如果中间有间隙（有图案不一致的）
                    if (allItems[begin.itemRow, i].currentSpr != spr)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                //起点终点行号
                int beginIndex = begin.itemRow;
                int endIndex = end.itemRow;
                //如果起点在上，交换起点终点列号
                if (beginIndex > endIndex)
                {
                    beginIndex = end.itemRow;
                    endIndex = begin.itemRow;
                }
                //遍历中间的Item
                for (int i = beginIndex + 1; i < endIndex; i++)
                {
                    //如果中间有间隙（有图案不一致的）
                    if (allItems[i, begin.itemColumn].currentSpr != spr)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}


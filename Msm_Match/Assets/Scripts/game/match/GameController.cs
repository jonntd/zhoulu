using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Summer.Game
{
    public class GameController : MonoBehaviour
    {
        #region Porp

        //单例
        public static GameController instance;
        //随机图案
        public Sprite[] randomSprites;
        //行列
        public int tableRow = 5;
        public int tableColumn = 5;
        //偏移量
        public Vector2 offset = new Vector2(0, 0);
        //所有的Item
        public Item[,] all_items;
        //所有Item的坐标
        public Vector3[,] allPos;
        //相同Item列表
        public List<Item> same_items_list = new List<Item>();
        //要消除的Item列表
        public List<Item> boom_list = new List<Item>();
        //随机颜色
        public Color randomColor;
        //正在操作
        public bool is_operation = false;
        //是否正在执行AllBoom
        public bool allBoom = false;

        //ITEM的边长
        private float itemSize = 0;

        #endregion

        #region MONO

        void Awake()
        {
            instance = this;
            // 初始化位置
            all_items = new Item[tableRow, tableColumn];
            allPos = new Vector3[tableRow, tableColumn];
        }

        void Start()
        {
            //初始化游戏
            InitGame();
            AllBoom();
        }

        #endregion

        #region public 

        /// <summary>
        /// 填充相同Item列表
        /// </summary>
        public void FillSameItemsList(Item current)
        {
            // 1.如果已存在，跳过
            if (same_items_list.Contains(current))
                return;
            // 2.添加到列表
            same_items_list.Add(current);

            //上下左右的Item
            Item[] temp_item_list = new Item[]{
            GetUpItem(current),GetDownItem(current),
            GetLeftItem(current),GetRightItem(current)};
            for (int i = 0; i < temp_item_list.Length; i++)
            {
                //如果Item不合法，跳过
                if (temp_item_list[i] == null)
                    continue;
                if (current.currentSpr == temp_item_list[i].currentSpr)
                {
                    FillSameItemsList(temp_item_list[i]);
                }
            }
        }

        #region 掉落

        #endregion

        #endregion


        /// <summary>
        /// 填充待消除列表
        /// </summary>
        /// <param name="current">Current.</param>
        public void FillBoomList(Item current)
        {
            // 计数器
            int row_count = 0;
            int column_count = 0;
            // 临时列表
            List<Item> row_temp_list = new List<Item>();
            List<Item> column_temp_list = new List<Item>();
            // 横向纵向检测
            foreach (var item in same_items_list)
            {
                // 如果在同一行
                if (item.itemRow == current.itemRow)
                {
                    //判断该点与Curren中间有无间隙
                    bool row_can_boom = CheckItemsInterval(true, current, item);
                    if (row_can_boom)
                    {
                        //计数
                        row_count++;
                        //添加到行临时列表
                        row_temp_list.Add(item);
                    }
                }

                // 如果在同一列
                if (item.itemColumn == current.itemColumn)
                {
                    //判断该点与Curren中间有无间隙
                    bool column_can_boom = CheckItemsInterval(false, current, item);
                    if (column_can_boom)
                    {
                        //计数
                        column_count++;
                        //添加到列临时列表
                        column_temp_list.Add(item);
                    }
                }
            }

            // 横向消除
            bool horizontal_boom = false;
            // 如果横向三个以上
            if (row_count > 2)
            {
                //将临时列表中的Item全部放入BoomList
                boom_list.AddRange(row_temp_list);
                //横向消除
                horizontal_boom = true;
            }
            // 如果纵向三个以上
            if (column_count > 2)
            {
                if (horizontal_boom)
                {
                    //剔除自己
                    boom_list.Remove(current);
                }
                //将临时列表中的Item全部放入BoomList
                boom_list.AddRange(column_temp_list);
            }
            // 如果没有消除对象，返回
            if (boom_list.Count == 0)
                return;
            // 创建临时的BoomList
            List<Item> temp_boom_list = new List<Item>();
            // 转移到临时列表
            temp_boom_list.AddRange(boom_list);
            // 开启处理BoomList的协程
            StartCoroutine(ManipulateBoomList(temp_boom_list));
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }

        public void SetOperation(bool value)
        {
            is_operation = value;
        }

        public CandyItem first;
        public CandyItem second;
        public void AddCandy(CandyItem item)
        {
            if (first == null)
            {
                first = item;
            }
            else
            {
                if (first == item) return;
            }

            if (second == null)
            {
                second = item;
            }
            else
            {
                if (first == item) return;
            }
            LogManager.Error("--------------AddCandy Erro------------");
        }

        public bool CanMove()
        {
            if (first == null || second == null)
                return false;
            return true;
        }

        public void Move()
        {

        }

        public void ClearSelect()
        {
            first = null;
            second = null;
        }

        public IEnumerator IenMove()
        {
            yield break;

        }

        #region private 

        /// <summary>
        /// 初始化游戏
        /// </summary>
        private void InitGame()
        {
            //获取Item边长
            itemSize = GetItemSize();
            //生成ITEM
            for (int i = 0; i < tableRow; i++)
            {
                for (int j = 0; j < tableColumn; j++)
                {
                    // 生成
                    GameObject currentItem =
                        ObjectPool.instance.GetGameObject(Util.Item, transform);
                    // 设置坐标
                    currentItem.transform.localPosition =
                        new Vector3(j * itemSize, i * itemSize, 0) + new Vector3(offset.x, offset.y, 0);

                    currentItem.transform.localScale = Vector3.one;
                    currentItem.name = string.Format("Item_{0}_{1}", j, i);

                    // 随机图案编号
                    int random = Random.Range(0, randomSprites.Length);
                    // 获取Item组件
                    Item current = currentItem.GetComponent<Item>();
                    // 设置行列
                    current.itemRow = i;
                    current.itemColumn = j;
                    // 设置图案
                    current.currentSpr = randomSprites[random];
                    // 设置图片
                    current.currentImg.sprite = randomSprites[random];
                    // 保存到数组
                    all_items[i, j] = current;
                    // 记录世界坐标
                    allPos[i, j] = currentItem.transform.position;
                }
            }
        }

        /// <summary>
        /// 获取Item边长
        /// </summary>
        private float GetItemSize()
        {
            /* return Resources.Load<GameObject>(Util.ResourcesPrefab + Util.Item).
                 GetComponent<RectTransform>().rect.height;*/
            return 98;
        }

        public void AllBoom()
        {
            //有消除
            int check_count = 0;
            bool has_boom = false;
            foreach (var item in all_items)
            {
                //指定位置的Item存在，且没有被检测过
                if (item && !item.hasCheck)
                {
                    check_count++;
                    //检测周围的消除
                    item.CheckAroundBoom();
                    if (boom_list.Count > 0)
                    {
                        has_boom = true;
                        is_operation = true;
                    }
                }
            }
            Debug.Log("check_count:" + check_count);
            if (!has_boom)
            {
                //操作结束
                is_operation = false;
            }
        }

        /// <summary>
        /// 生成新的Item
        /// </summary>
        public IEnumerator CreateNewItem()
        {
            is_operation = true;
            for (int i = 0; i < tableColumn; i++)
            {
                int count = 0;
                Queue<GameObject> newItemQueue = new Queue<GameObject>();
                for (int j = 0; j < tableRow; j++)
                {
                    if (all_items[j, i] == null)
                    {
                        //生成一个Item
                        GameObject current = (GameObject)Instantiate(Resources.
                            Load<GameObject>(Util.ResourcesPrefab + Util.Item));

                        //						ObjectPool.instance.GetGameObject (Util.Item, transform);
                        current.transform.parent = transform;
                        current.transform.position = allPos[tableRow - 1, i];
                        current.transform.localScale = Vector3.one;
                        current.name = string.Format("Item_{0}_{1}", j, i);
                        newItemQueue.Enqueue(current);
                        count++;
                    }
                }
                for (int k = 0; k < count; k++)
                {
                    //获取Item组件
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
                    currentItem.GetComponent<ItemOperation>().ItemMove(r, i, allPos[r, i]);
                }
            }
            yield break;
        }

        /// <summary>
        /// 检测两个Item之间是否有间隙（图案不一致）
        /// </summary>
        private bool CheckItemsInterval(bool is_horizontal, Item begin, Item end)
        {
            // 是否是横向 / 检测起点 / 检测终点
            //  检测起点和重点两个item是否一致，以及他们之间是否相连
            // 获取图案
            Sprite spr = begin.currentSpr;
            // 如果是横向
            if (is_horizontal)
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
                    if (all_items[begin.itemRow, i] == null)
                        return false;
                    //如果中间有间隙（有图案不一致的）
                    if (all_items[begin.itemRow, i].currentSpr != spr)
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
                    if (all_items[i, begin.itemColumn].currentSpr != spr)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// 处理BoomList
        /// </summary>
        private IEnumerator ManipulateBoomList(List<Item> temp_boom_list)
        {
            foreach (var item in temp_boom_list)
            {
                item.hasCheck = true;
                item.GetComponent<Image>().color = randomColor * 2;
                // 离开动画
                item.GetComponent<AnimatedButton>().Exit();
                // Boom声音
                //AudioManager.instance.PlayMagicalAudio();
                if (all_items[item.itemRow, item.itemColumn] == null)
                {
                    Debug.Log("已经为空了");
                }
                //将被消除的Item在全局列表中移除
                all_items[item.itemRow, item.itemColumn] = null;
            }
            //检测Item是否已经开发播放离开动画
            while (!temp_boom_list[0].GetComponent<AnimatedButton>().CheckPlayExit())
            {
                yield return 0;
            }

            //延迟0.2秒
            yield return new WaitForSeconds(0.2f);
            // 开启下落
            yield return StartCoroutine(ItemsDrop());
            // 延迟0.38秒
            yield return new WaitForSeconds(0.38f);

            foreach (var item in temp_boom_list)
            {
                //回收Item
                ObjectPool.instance.SetGameObject(item.gameObject);
            }
        }


        #region 检测

        /// <summary>
        /// 检测行列是否合法
        /// </summary>
        public bool CheckRcLegal(int item_row, int item_column)
        {
            if (item_row >= 0 && item_row < tableRow && item_column >= 0 && item_column < tableColumn)
                return true;
            return false;
        }


        /// <summary>
        /// 获取上方Item
        /// </summary>
        private Item GetUpItem(Item current)
        {
            int row = current.itemRow + 1;
            int column = current.itemColumn;
            if (!CheckRcLegal(row, column))
                return null;
            return all_items[row, column];
        }

        /// <summary>
        /// 获取下方Item
        /// </summary>
        private Item GetDownItem(Item current)
        {
            int row = current.itemRow - 1;
            int column = current.itemColumn;
            if (!CheckRcLegal(row, column))
                return null;
            return all_items[row, column];
        }

        /// <summary>
        /// 获取左方Item
        /// </summary>
        private Item GetLeftItem(Item current)
        {
            int row = current.itemRow;
            int column = current.itemColumn - 1;
            if (!CheckRcLegal(row, column))
                return null;
            return all_items[row, column];
        }

        /// <summary>
        /// 获取右方Item
        /// </summary>
        private Item GetRightItem(Item current)
        {
            int row = current.itemRow;
            int column = current.itemColumn + 1;
            if (!CheckRcLegal(row, column))
                return null;
            return all_items[row, column];
        }

        #endregion

        #region 掉落

        /// <summary>
        /// Items下落
        /// </summary>
        private IEnumerator ItemsDrop()
        {
            is_operation = true;
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
                    if (all_items[j, i] != null)
                    {
                        //计数
                        count++;
                        //放入队列
                        dropQueue.Enqueue(all_items[j, i]);
                    }
                }
                //下落
                for (int k = 0; k < count; k++)
                {
                    //获取要下落的Item
                    Item current = dropQueue.Dequeue();
                    //修改全局数组(原位置情况)
                    all_items[current.itemRow, current.itemColumn] = null;
                    //修改Item的行数
                    current.itemRow = k;
                    //修改全局数组(填充新位置)
                    all_items[current.itemRow, current.itemColumn] = current;
                    //下落
                    current.GetComponent<ItemOperation>().
                        CurrentItemDrop(allPos[current.itemRow, current.itemColumn]);
                }
            }

            yield return new WaitForSeconds(0.2f);

            StartCoroutine(CreateNewItem());
            yield return new WaitForSeconds(0.2f);
            AllBoom();
        }

        #endregion

        #endregion
    }
}
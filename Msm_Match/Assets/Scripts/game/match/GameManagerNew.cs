using System.Collections;
using UnityEngine;

namespace Summer.Game
{
    public class GameManagerNew : MonoBehaviour
    {
        #region Unity内置函数 MonoBehaviour
        void Start()
        {
            /* audioSource = this.GetComponent<AudioSource>();
             candyArray = new ArrayList();

             for (int rIndex = 0; rIndex < rowNum; rIndex++)
             {
                 ArrayList temp = new ArrayList();

                 for (int cIndex = 0; cIndex < columnNum; cIndex++)
                 {
                     temp.Add(AddCandy(rIndex, cIndex));
                 }

                 candyArray.Add(temp);
             }

             //第一次检测糖果是否可以消除
             if (CheckMatches())
             {
                 RemoveMatches();
             }

             if (Application.loadedLevelName == "PlayGame")
             {
                 CountDown.startTime = Time.time;
             }*/
        }
        void Update()
        {
            /*  if (CountDown.GameOver)//游戏结束
              {
                  DDL._i.currentScore = sum;
                  sum = 0;
                  SceneManager.LoadScene(2);
              }*/
        }
        #endregion

        #region Exchange2协程
        IEnumerator ExchangeTwo(CandyItem candy1, CandyItem candy2)
        {
            Exchange(candy1, candy2);//先交换

            yield return new WaitForSeconds(0.2f);//然后暂停0.7秒

            if (CheckMatches())//最后在检测是否可以消除
            {
                RemoveMatches();//检测到有3个以上相同的糖果了那就进行消除
            }
            else//没有检测到可以消除的糖果
            {
                Exchange(candy1, candy2);//那就将这两个交换位置后的糖果再交换回去到原来的位置
            }
        }

        #endregion

        #region -Exchange交换两个糖果的位置
        private void Exchange(CandyItem candy1, CandyItem candy2)
        {
            // 1.糖果的交换的音乐

            //重新设置糖果集合里的数据
           /* SetCandy(candy2.rIndex, candy2.cIndex, candy1);
            SetCandy(candy1.rIndex, candy1.cIndex, candy2);

            //记录candy1也就是第一个点击时记录下来的糖果的行列索引
            rIndex = candy1.rIndex;
            cIndex = candy1.cIndex;

            //交换candy1的行列索引
            candy1.rIndex = candy2.rIndex;
            candy1.cIndex = candy2.cIndex;

            //交换candy2的行列索引
            candy2.rIndex = rIndex;
            candy2.cIndex = cIndex;

            //同时执行candy1和candy2的位置更新方法
            candy1.TweenToPosition();
            candy2.TweenToPosition();*/
        }
        #endregion

    }
}

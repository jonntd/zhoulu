using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Summer.Game
{

    public class Item : MonoBehaviour
    {

        public int itemRow;                 //行
        public int itemColumn;              //列

        public Sprite currentSpr;           //当前图案
        public Image currentImg;            //图案
        private GameController controller;

        public bool hasCheck = false;        //被检测

        void Awake()
        {
            currentImg = transform.GetChild(0).GetComponent<Image>();
        }

        void OnEnable()
        {
            controller = GameController.instance;
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        public void CheckAroundBoom()
        {
            // 清除相同Item列表
            controller.same_items_list.Clear();
            // 待消除的Item列表
            controller.boom_list.Clear();
            controller.randomColor = Color.white;
            controller.FillSameItemsList(this);
            controller.FillBoomList(this);
        }
    }
}


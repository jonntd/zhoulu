using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Summer.Game
{
    public class CandyItem : MonoBehaviour
    {
        public CandyInfo info;
        public Image icon;
        public RectTransform rect_trans;

        #region
        void Awake()
        {

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

        public void SetInfo(CandyInfo data)
        {
            info = data;
            _init();
        }

        public void _init()
        {
            rect_trans.localPosition = new Vector3(info.ItemPosX, info.ItemPosY, 0);
        }

        #region Des

        public string des;

        public string ToDes()
        {
            if (string.IsNullOrEmpty(des))
            {
                des = "Rol:" + info.ItemRow + "   Col:" + info.ItemCol;
            }
            return des;
        }

        #endregion
    }


}

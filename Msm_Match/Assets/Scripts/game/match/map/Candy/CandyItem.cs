using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Game
{
    public class CandyItem : MonoBehaviour
    {
        public CandyInfo _info;

        private RectTransform rect_trans;

        #region
        void Awake()
        {
            rect_trans = GetComponent<RectTransform>();
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

        public void SetInfo(CandyInfo info)
        {
            _info = info;
            _init();
        }

        public void _init()
        {
            rect_trans.localPosition = new Vector3(_info.itemX, _info.itemY, 0);
        }
    }


}

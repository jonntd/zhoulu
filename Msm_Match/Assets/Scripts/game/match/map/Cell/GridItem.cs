using UnityEngine;
using UnityEngine.UI;
namespace Summer.Game
{
    public class GridItem : MonoBehaviour
    {
       
        //public SpriteRenderer sprite_renderer;
        public Image img;
        public GridInfo _info;
        private RectTransform rect_trans;
        #region Mono
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

        public void SetInfo(GridInfo info)
        {
            _info = info;
            _init();
        }

        public void _init()
        {
            // 初始化位置
            rect_trans.localPosition = new Vector3(_info.ItemPosX, _info.ItemPosY, 0);
            // 初始化图片
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Game
{

    public class GridsItem : MonoBehaviour
    {
        public Dictionary<int, GridItem> _grid_map = new Dictionary<int, GridItem>();

        private Transform trans;
        #region Mono

        void Awake()
        {
            trans = GetComponent<Transform>();
        }

        void Start()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    GridItem item = ItemFactory.Instance.PopGridItem();
                    
                    item.name = i + "_" + j;
                    item.transform.parent = trans;
                    item.transform.localScale=Vector3.one;

                    item.SetInfo(new GridInfo(i, j, 1));
                    ;

                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        #endregion
    }
}



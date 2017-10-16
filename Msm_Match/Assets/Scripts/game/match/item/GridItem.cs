using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Game
{
    public class GridItem : MonoBehaviour
    {
        public CellItem _cell_item;
        public int row;
        public int col;

        private Transform trans;
        void Awake()
        {
            trans = GetComponent<Transform>();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetRowCol(int r, int c)
        {
            row = r;
            col = c;
            trans.localPosition = new Vector3(c, r, 0);
        }
    }
}


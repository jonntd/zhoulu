using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Game
{
    public class LevelItem : MonoBehaviour
    {
        public const int MAX_GRID = 64;
        public GridItem pfb_grid;
        public Transform parent_grid;
        public List<GridItem> grids = new List<GridItem>(MAX_GRID);

        #region MonoBehaviour

        void Awake()
        {
            LoadLevel(1);
        }

        void Start()
        {

        }


        void Update()
        {

        }
        #endregion

        public void LoadLevel(int id)
        {
            MapCnf map_cnf = MapManager.Instance.FindMap(id);

            int rlength = map_cnf.map_cnf.Length;
            for (int r = 0; r < rlength; r++)
            {
                int clength = map_cnf.map_cnf[r].Length;
                for (int c = 0; c < clength; c++)
                {
                    GridItem tmp_grid = Instantiate<GridItem>(pfb_grid);

                   /* tmp_grid.gameObject.transform.parent = parent_grid;
                    tmp_grid.gameObject.transform.localPosition = Vector3.zero;
                    tmp_grid.gameObject.transform.localScale = Vector3.one;
                    tmp_grid.SetRowCol(r, c);

                    grids.Add(tmp_grid);*/
                }

                //ItemFactory.Instance.CreateGrid()
            }
        }

    }
}


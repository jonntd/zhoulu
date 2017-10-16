using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Game
{
    public class LevelItem : MonoBehaviour
    {
        public const int MAX_GRID = 64;
        public List<GridItem> grids = new List<GridItem>(MAX_GRID);

        #region MonoBehaviour

        void Awake()
        {

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

                //ItemFactory.Instance.CreateGrid()
            }
        }

    }
}


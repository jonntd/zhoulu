using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Game
{
    public class BattleController : MonoBehaviour
    {
        public static BattleController Instance;
        public CandysItem candy_map;                            // 糖果层
        public GridsItem grid_map;                              // 地表
        #region Mono

        void Awake()
        {
            Instance = this;
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

        #region public
        #endregion
    }

}


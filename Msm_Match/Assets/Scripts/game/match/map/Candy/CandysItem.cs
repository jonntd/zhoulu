using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Game
{

    public class CandysItem : MonoBehaviour
    {
        public Dictionary<int, GridItem> _grid_map = new Dictionary<int, GridItem>();

        private Transform trans;

        #region Mono
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
        #endregion
    }
}


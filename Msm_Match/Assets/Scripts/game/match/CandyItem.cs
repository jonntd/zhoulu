using UnityEngine;

namespace Summer.Game
{
    public class CandyItem : MonoBehaviour
    {
        public CandyInfo _info;

        #region Unity内置函数 MonoBehaviour

        private void Awake()
        {

        }

        private void Start()
        {

        }

        private void Update()
        {

        }

        #endregion


        public void SetInfo(CandyInfo info)
        {
            _info = info;
        }
    }
}

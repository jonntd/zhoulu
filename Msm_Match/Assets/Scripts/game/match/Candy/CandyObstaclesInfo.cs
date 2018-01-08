using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Game
{
    public class CandyObstaclesInfo : CandyInfo
    {
        protected int _headlth;

        public CandyObstaclesInfo(int i_row, int i_col) : base(i_row, i_col)
        {
            dead_effect = new CandyCreateEffect();
        }

        #region Override

        public override void EliminateHealth()
        {
            _headlth--;
            if (_headlth == 0)
            {
                Alive = false;
            }

        }

        #endregion
    }
}


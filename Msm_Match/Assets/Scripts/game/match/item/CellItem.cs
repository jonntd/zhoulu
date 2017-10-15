using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Game
{
    public class CellItem : MonoBehaviour
    {
        public CellCnf cnf;
        public SpriteRenderer sprite_renderer;
        public void ResetSprite()
        {

        }

        public void ResetSrpite(E_CellType type)
        {
            sprite_renderer.sprite = SpriteManager.Instance.find_cell(type);
            _reset_chil_effect_sprite(cnf.cell_effect);
        }

        public void _reset_chil_effect_sprite(E_CellEffect effect_type)
        {

        }

    }

}


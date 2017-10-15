using UnityEngine;
using System.Collections.Generic;
namespace Summer.Game
{
    public class SpriteManager : MonoBehaviour
    {

        public static SpriteManager Instance;
        public CellSprite[] _cell_sprites;
        public JewelsSprite[] _Jewel_spites;

        public Dictionary<E_CellType, Sprite> _cell_map;
        public Dictionary<E_JewelType, Sprite> _jewel_map;
        void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 查找地块图片
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Sprite find_cell(E_CellType type)
        {
            return _cell_map[type];
        }

        public Sprite find_jewel(E_JewelType type)
        {
            return _jewel_map[type];
        }
    }

    [System.Serializable]
    public class JewelsSprite
    {
        public E_JewelType type;
        public Sprite sprite;
    }

    [System.Serializable]
    public class CellSprite
    {
        public E_CellType type;
        public Sprite sprite;
    }
}


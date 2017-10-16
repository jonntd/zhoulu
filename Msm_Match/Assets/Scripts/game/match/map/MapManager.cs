
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Game
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance;

        public Dictionary<int, MapCnf> _maps = new Dictionary<int, MapCnf>();
        void Awake()
        {
            Instance = this;
            FindMap(1);
        }


        public MapCnf FindMap(int id)
        {
            if (!_maps.ContainsKey(id))
            {
                _maps.Add(id, new MapCnf(id));
            }

            return _maps[id];
        }
    }

}


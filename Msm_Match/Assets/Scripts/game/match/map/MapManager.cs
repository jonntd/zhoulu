
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Game
{
    public class MapManager 
    {
        public static MapManager Instance=new MapManager();

        public Dictionary<int, MapCnf> _maps = new Dictionary<int, MapCnf>();

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


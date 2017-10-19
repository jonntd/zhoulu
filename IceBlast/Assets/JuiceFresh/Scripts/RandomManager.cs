using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager
{
    public static RandomManager instance = new RandomManager();

    public List<int> _values = new List<int>();
    public int RandomIndex()
    {
        int all_num = 0;
        for (int i = 0; i < _values.Count; i++)
        {
            all_num += _values[i];
        }
        int index = 0;
        int result = (int)(Random.value * all_num);
        for (int i = 0; i < _values.Count; i++)
        {
            if (result > 0)
            {
                result -= _values[i];
                if (result <= 0)
                {
                    index = i;
                    break;
                }
            }
            else
            {
                index = i;
            }
        }

        return index;
    }

    public void ResetLevel(int level)
    {
        MLevelInfo info = DataManager.instance.m_level_map[level];
        _reset_values(info.weight);
    }

    public void _reset_values(List<int> values)
    {
        _values.Clear();
        for (int i = 0; i < values.Count; i++)
        {
            _values.Add(values[i]);
        }
    }

    public bool IsValid(int index)
    {
        return _values[index] > 0;
    }

}

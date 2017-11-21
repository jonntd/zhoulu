using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfig
{
    public int Id;
    public LIMIT type_limit;
    public int type_limit_param;

    public int color_limit;                         //3-6之间

    public int[] weight = new int[6];               //每一个比重强制性六段

    public int[] starts = new int[3];               //分数 强制性3段

    public Target target;                           //一共六种类型

    public void SetInfo(string content)
    {
        string[] info = content.Split('\t');
        _excute_id(info[0]);

        _excute_type_limit(info[1]);
        _excute_type_limit_param(info[2]);

        _excute_color_limit(info[3]);

        _excute_star(info[4]);
        _excute_weight(info[5]);
        _excute_target(info[6]);

        if (target == Target.SCORE)
        {
            _excute_score(info[7]);
        }
        else if (target == Target.COLLECT)
        {
            _excute_collect(info[8]);
        }
        else if (target == Target.ITEMS)
        {
            _excute_items(info[9]);
        }
        else if (target == Target.BLOCKS)
        {
            _excute_blocks(info[10]);
        }
        else if (target == Target.CAGES)
        {
            _excute_cages(info[11]);
        }
        else if (target == Target.BOMBS)
        {
            _excute_bombs(info[12]);
        }
    }

    public void _excute_id(string info)
    {
        bool result = int.TryParse(info, out Id);
        ErrorInfo(result, info);
    }
    public void _excute_type_limit(string info)
    {
        int type = 0;
        bool result = int.TryParse(info, out type);

        if (type == 0)
        {
            type_limit = LIMIT.MOVES;
        }
        else
        {
            type_limit = LIMIT.TIME;
        }
        ErrorInfo(result, info);
    }

    public void _excute_type_limit_param(string info)
    {
        bool result = false;
        if (type_limit == LIMIT.MOVES)
        {
            result = int.TryParse(info, out type_limit_param);
        }
        else
        {
            result = int.TryParse(info, out type_limit_param);
        }
        ErrorInfo(result, info);
    }
    public void _excute_color_limit(string info)
    {
        bool result = int.TryParse(info, out color_limit);
        if (color_limit <= 3)
            color_limit = 3;

        if (color_limit >= 6)
            color_limit = 6;
        ErrorInfo(result, info);
    }

    public void _excute_weight(string info)
    {
        string[] weight_info = info.Split('|');
        for (int i = 0; i < weight.Length; i++)
        {
            int.TryParse(weight_info[i], out weight[i]);
        }
    }
    public void _excute_star(string info)
    {
        string[] star_info = info.Split('|');
        for (int i = 0; i < starts.Length; i++)
        {
            int.TryParse(star_info[i], out starts[i]);
        }
    }

    public void _excute_target(string info)
    {
        int type = 0;
        bool result = int.TryParse(info, out type);
        target = (Target)type;
        ErrorInfo(result, info);
    }

    #region target

    public CollectStars collect_starts = CollectStars.STAR_1;
    private void _excute_score(string info)
    {
        string content = info;
        if (content == "1")
            collect_starts = CollectStars.STAR_1;
        else if (content == "2")
            collect_starts = CollectStars.STARS_2;
        else if (content == "3")
            collect_starts = CollectStars.STARS_3;
        else
            collect_starts = CollectStars.STAR_1;
    }

    public CollectedIngredientsConfig[] _collect = new CollectedIngredientsConfig[2];             //收集 
    private void _excute_collect(string info)
    {
        string content = info;

        string[] nums = content.Split('|');
        for (int i = 0; i < _collect.Length; i++)
        {
            if (_collect[i] == null)
                _collect[i] = new CollectedIngredientsConfig();
            int num = 0;
            int.TryParse(nums[i], out num);
            if (num == 0)
            {
                _collect[i].check = false;
                _collect[i].count = 1;
            }
            else
            {
                _collect[i].check = true;
                _collect[i].count = num;
            }
        }
    }

    public CollectedItemConfig[] _items = new CollectedItemConfig[6];             //收集 
    private void _excute_items(string info)
    {
        string content = info;

        string[] nums = content.Split('|');
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == null)
                _items[i] = new CollectedItemConfig();
            int num = 0;
            int.TryParse(nums[i], out num);
            if (num == 0)
            {
                _items[i].check = false;
                _items[i].count = 1;
            }
            else
            {
                _items[i].check = true;
                _items[i].count = num;
            }
        }

        //需要根据colorLimit的长度做限制
        for (int i = _items.Length - 1; i >= color_limit; i--)
        {
            if (_items[i] != null)
                _items[i].enable = false;
        }

    }

    private void _excute_blocks(string info)
    {
        //会清楚一些记录位置记录
    }

    public int cage_hp;
    private void _excute_cages(string info)
    {
        //会清楚一些记录位置记录
        string content = info;
        int.TryParse(content, out cage_hp);
        if (cage_hp <= 0)
            cage_hp = 1;
    }


    public int bombs_collect;           //bombsAtTheSameTime
    public int bombs_counter;           //bombTimer
    private void _excute_bombs(string info)
    {
        string content = "0|1";

        string[] contents = content.Split('|');
        int.TryParse(contents[0], out bombs_collect);
        int.TryParse(contents[1], out bombs_counter);
        if (bombs_counter <= 0)
            bombs_counter = 1;
    }

    #endregion

    public void ErrorInfo(bool result, string info)
    {
        if (result) return;
        Debug.LogError(info);
    }
}


public class CollectedItemConfig
{
    public bool check = false;
    public int count = 1;
    public bool enable = true;

}




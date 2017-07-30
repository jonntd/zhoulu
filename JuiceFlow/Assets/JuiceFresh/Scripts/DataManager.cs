using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DataManager
{


}

public class ExcelLevel
{
    public int levelNumber;

    public Target target;

    public string size_info = "";
    public int maxCols;
    public int maxRows;

    public ExcelLimit _excel_limit;
    public ExcelColorLimit _excel_color_limit;
    public ExcelStars _excel_stars;


    public ExcelCollectItem _target_collect_items;
    public ExcelCollectCount _target_collect_count;
    public ExcelGages _target_gages;
    public ExcelGetStars _target_get_stars;
    public ExcelBombs _target_bombs;

    public List<string> maps = new List<string>();
    public bool LoadDataFromLocal(int currentLevel)
    {
        levelNumber = currentLevel;
        //Read data from text file
        TextAsset mapText = Resources.Load("Levels/" + currentLevel) as TextAsset;
        if (mapText == null)
        {
            return false;
            //SaveLevel();
            mapText = Resources.Load("Levels/" + currentLevel) as TextAsset;
        }
        ProcessGameDataFromString(mapText.text);
        return true;
    }

    void ProcessGameDataFromString(string mapText)
    {

        string[] lines = mapText.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        int[] indexItems = new int[10];


        int mapLine = 0;
        foreach (string line in lines)
        {
            if (line.StartsWith("MODE "))
            {
                string modeString = line.Replace("MODE", string.Empty).Trim();
                target = (Target)int.Parse(modeString);
            }
            else if (line.StartsWith("SIZE "))
            {
                size_info = line;
                string blocksString = line.Replace("SIZE", string.Empty).Trim();
                string[] sizes = blocksString.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                maxCols = int.Parse(sizes[0]);
                maxRows = int.Parse(sizes[1]);
            }
            else if (line.StartsWith("LIMIT "))
            {
                _excel_limit = new ExcelLimit();
                string blocksString = line.Replace("LIMIT", string.Empty).Trim();
                _excel_limit.SetInfo(blocksString);
                /*string blocksString = line.Replace("LIMIT", string.Empty).Trim();
                string[] sizes = blocksString.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                limitType = (LIMIT)int.Parse(sizes[0]);
                limit = int.Parse(sizes[1]);*/

            }
            else if (line.StartsWith("COLOR LIMIT "))
            {
                string blocksString = line.Replace("COLOR LIMIT", string.Empty).Trim();
                _excel_color_limit=new ExcelColorLimit();
                _excel_color_limit.SetInfo(blocksString);
                /*colorLimit = int.Parse(blocksString);*/
            }
            else if (line.StartsWith("STARS "))
            {
                string blocksString = line.Replace("STARS", string.Empty).Trim();
                _excel_stars = new ExcelStars();
                _excel_stars.SetInfo(blocksString);
                /*string[] blocksNumbers = blocksString.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                star1 = int.Parse(blocksNumbers[0]);
                star2 = int.Parse(blocksNumbers[1]);
                star3 = int.Parse(blocksNumbers[2]);*/
            }
            else if (line.StartsWith("COLLECT ITEMS "))
            {
                string blocksString = line.Replace("COLLECT ITEMS", string.Empty).Trim();
                if (_target_collect_items == null)
                    _target_collect_items = new ExcelCollectItem();
                _target_collect_items.SetInfo(blocksString);
                /*string[] blocksNumbers = blocksString.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < collectItems.Length; i++)
                {
                    if (collectItems[i] == null)
                        collectItems[i] = new CollectedItem();
                }

                for (int i = 0; i < blocksNumbers.Length; i++)
                {
                    if (collectItems[i] == null)
                        collectItems[i] = new CollectedItem();

                    if (collectItems.Length > i)
                    {
                        collectItems[int.Parse(blocksNumbers[i])].check = true;
                        indexItems[i] = int.Parse(blocksNumbers[i]);
                    }

                    if (target == Target.COLLECT)
                    {
                        if (ingr.Length > i)
                            ingr[int.Parse(blocksNumbers[i])].check = true;
                    }

                }*/
            }
            else if (line.StartsWith("COLLECT COUNT "))
            {
                string blocksString = line.Replace("COLLECT COUNT", string.Empty).Trim();
                if (_target_collect_count == null)
                    _target_collect_count = new ExcelCollectCount();
                _target_collect_count.SetInfo(blocksString);
                /* string[] blocksNumbers = blocksString.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                 for (int i = 0; i < blocksNumbers.Length; i++)
                 {
                     if (collectItems[i] == null)
                         collectItems[i] = new CollectedItem();

                     collectItems[indexItems[i]].count = int.Parse(blocksNumbers[i]);

                     if (target == Target.COLLECT)
                     {
                         if (ingr.Length > i)
                             ingr[i].count = int.Parse(blocksNumbers[i]);
                     }

                 }*/
            }
            else if (line.StartsWith("CAGE "))
            {
                string blocksString = line.Replace("CAGE ", string.Empty).Trim();
                _target_gages = new ExcelGages();
                _target_gages.SetInfo(blocksString);

                /*cageHP = int.Parse(blocksString);*/
            }
            else if (line.StartsWith("BOMBS "))
            {
                string blocksString = line.Replace("BOMBS ", string.Empty).Trim();
                _target_bombs = new ExcelBombs();
                _target_bombs.SetInfo(blocksString);
                /*string[] blocksNumbers = blocksString.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                bombsAtTheSameTime = int.Parse(blocksNumbers[0]);
                bombTimer = int.Parse(blocksNumbers[1]);*/
            }
            else if (line.StartsWith("GETSTARS "))
            {
                string blocksString = line.Replace("GETSTARS ", string.Empty).Trim();
                _target_get_stars = new ExcelGetStars();
                _target_get_stars.SetInfo(blocksString);

                /*starsTargetCount = (CollectStars)int.Parse(blocksString);*/
            }

            else
            {

                maps.Add(line);
                //Maps
                //Split lines again to get map numbers
                /*string[] st = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < st.Length; i++)
                {
                    levelSquares[mapLine * maxCols + i].block = (SquareTypes)int.Parse(st[i][0].ToString());
                    levelSquares[mapLine * maxCols + i].obstacle = (SquareTypes)int.Parse(st[i][1].ToString());
                }
                mapLine++;*/
            }
        }
    }
    string saveString = "";
    public void Save()
    {
        string name = "msm_" + levelNumber;


        add_text("MODE " + (int)target);

        add_text("SIZE " + maxCols + "/" + maxRows);

        add_text(_excel_limit.GetText());
        add_text(_excel_color_limit.GetText());
        add_text(_excel_stars.GetText());

        if (target == Target.COLLECT)
        {
            add_text(_target_collect_items.GetText());
            add_text(_target_collect_count.GetText());
        }
        else if (target == Target.ITEMS)
        {
            add_text(_target_collect_items.GetText());
            add_text(_target_collect_count.GetText());
        }
        else if (target == Target.CAGES)
        {
            add_text(_target_gages.GetText());
        }
        else if (target == Target.SCORE)
        {
            add_text(_target_get_stars.GetText());
        }
        else if (target == Target.BOMBS)
        {
            add_text(_target_bombs.GetText());
        }

        for (int i = 0; i < maps.Count; i++)
        {
            add_text(maps[i], i == maps.Count - 1);
        }

        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
        {
            //Write to file
            string activeDir = Application.dataPath + @"/JuiceFresh/Resources/Levels/";
            string newPath = System.IO.Path.Combine(activeDir, levelNumber + ".txt");
            StreamWriter sw = new StreamWriter(newPath);
            sw.Write(saveString);
            sw.Close();
        }
        AssetDatabase.Refresh();

    }

    public void add_text(string add, bool is_r = false)
    {
        saveString += add;
        if (!is_r)
            saveString += "\r\n";
    }

}

public interface ExcelInfo
{
    string GetName();

    string GetText();
}

public class ExcelMode : ExcelInfo
{
    public int mode;
    public string GetName()
    {
        return "MODE ";
    }

    public string GetText()
    {
        return GetName() + mode;
    }

    public void SetMode(string str)
    {
        try
        {
            mode = int.Parse(str);
        }
        catch (Exception)
        {
            Debug.LogError("ExcelMode Error:" + str);
            throw;
        }

    }
}

public class ExcelLimit : ExcelInfo
{
    public string _info;
    public string GetName()
    {
        return "LIMIT ";
    }

    public void SetInfo(string info)
    {
        _info = info;
    }

    public string GetText()
    {
        return GetName() + _info;
    }
}

public class ExcelColorLimit : ExcelInfo
{
    public int _color_limit;
    public string GetName()
    {
        return "COLOR LIMIT ";
    }

    public string GetText()
    {
        return GetName() + _color_limit;
    }

    public void SetInfo(string color_limit)
    {
        _color_limit = int.Parse(color_limit);
    }
}

public class ExcelStars : ExcelInfo
{
    public string _star;

    public string GetName()
    {
        return "STARS ";
    }

    public string GetText()
    {
        return GetName() + _star;
    }

    public void SetInfo(string str)
    {
        _star = str;
    }
}

#region Target

//star
public class ExcelGetStars : ExcelInfo
{
    public int _get_stars;
    public string GetName()
    {
        return "GETSTARS ";
    }

    public string GetText()
    {
        return GetName() + _get_stars;
    }

    public void SetInfo(string get_stars)
    {
        _get_stars = int.Parse(get_stars);
    }
}

public class ExcelCollectItem : ExcelInfo
{
    public string _info;
    public string GetName()
    {
        return "COLLECT ITEMS ";
    }

    public string GetText()
    {
        return GetName() + _info;
    }

    public void SetInfo(string info)
    {
        _info = info;
    }
}

public class ExcelCollectCount : ExcelInfo
{
    public string _info;
    public string GetName()
    {
        return "COLLECT COUNT ";
    }

    public string GetText()
    {
        return GetName() + _info;
    }

    public void SetInfo(string info)
    {
        _info = info;
    }
}

public class ExcelItems : ExcelInfo
{
    public List<int> _items = new List<int>();
    public List<int> _counts = new List<int>();
    public string _items_1;
    public string _counts_1;
    public string GetName()
    {
        return "COLLECT ITEMS ";
    }

    public string GetText()
    {
        string info = string.Empty;
        string items = "COLLECT ITEMS" + _items_1;
        string counts = "COLLECT COUNT" + _counts_1;
        return string.Empty;
    }
}

public class ExcelGages : ExcelInfo
{
    public int _hp;
    public string GetName()
    {
        return "CAGE ";
    }

    public string GetText()
    {
        return GetName() + _hp;
    }

    public void SetInfo(string hp)
    {
        _hp = int.Parse(hp);
    }
}

public class ExcelBombs : ExcelInfo
{
    public string _info;
    public string GetName()
    {
        return "BOMBS ";
    }

    public string GetText()
    {
        return GetName() + _info;
    }

    public void SetInfo(string info)
    {
        _info = info;
    }

}

#endregion























using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class CollectedItem
{
    public string name;
    public bool check;
    public int count;
    public bool enable;

    public void SetConfig(CollectedItemConfig config)
    {
        check = config.check;
        count = config.count;
        enable = config.enable;
    }
}




public class LevelConfigManager
{
    public Dictionary<int, LevelConfig> configs = new Dictionary<int, LevelConfig>();
    public TextAsset level_config;

    public static LevelConfigManager _instance;
    public static LevelConfigManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LevelConfigManager();
            }

            return _instance;

        }
    }
    public LevelConfigManager()
    {
        level_config = Resources.Load("LevelConfig") as TextAsset;
    }

    public void parse_text()
    {
        configs.Clear();
        string[] infos = level_config.text.Split('\r');

        for (int i = 2; i < infos.Length; i++)
        {
            LevelConfig config = new LevelConfig();
            config.SetInfo(infos[i]);
            configs.Add(config.Id, config);
            break;

        }
    }

    public LevelConfig GetConfig(int level)
    {
        if (configs.ContainsKey(level))
            return configs[level];
        return null;
    }

}


public class LevelInfo
{
    public int levelNumber;

    public Target target;

    public int maxCols;
    public int maxRows;

    public LIMIT limitType;
    public int limit;

    public int colorLimit;

    public int star1;
    public int star2;
    public int star3;

    private CollectedIngredients[] ingr = new CollectedIngredients[4];

    public CollectedItem[] collectItems = new CollectedItem[6];

    public int cageHP;

    public CollectStars starsTargetCount;

    public int NumIngredients;

    public int bombsAtTheSameTime;
    public int bombTimer;


    public static SquareBlocks[] levelSquares;
    public void SaveMap()
    {
        string saveString = "";
        //Create save string
        saveString += "MODE " + (int)target;
        saveString += "\r\n";
        saveString += "SIZE " + maxCols + "/" + maxRows;
        saveString += "\r\n";
        saveString += "LIMIT " + (int)limitType + "/" + limit;
        saveString += "\r\n";
        saveString += "COLOR LIMIT " + colorLimit;
        saveString += "\r\n";
        saveString += "STARS " + star1 + "/" + star2 + "/" + star3;
        saveString += "\r\n";

        if (target == Target.COLLECT)
        {

            saveString += "COLLECT ITEMS ";
            for (int i = 0; i < ingr.Length; i++)
            {
                if (ingr[i].check)
                    saveString += i + "/";
            }
            saveString += "\r\n";

            saveString += "COLLECT COUNT ";
            for (int i = 0; i < ingr.Length; i++)
            {
                if (ingr[i].check)
                    saveString += (int)ingr[i].count + "/";
            }
        }
        else if (target == Target.ITEMS)
        {

            saveString += "COLLECT ITEMS ";
            for (int i = 0; i < collectItems.Length; i++)
            {
                if (collectItems[i].check /*&& collectItems[i].enable*/)
                    saveString += i + "/";
            }
            saveString += "\r\n";

            saveString += "COLLECT COUNT ";
            for (int i = 0; i < collectItems.Length; i++)
            {
                if (collectItems[i].check /*&& collectItems[i].enable*/)
                    saveString += (int)collectItems[i].count + "/";
            }
        }
        else if (target == Target.CAGES)
            saveString += "CAGE " + (int)cageHP;
        else if (target == Target.BOMBS)
        {
            saveString += "GETSTARS " + (int)starsTargetCount;
            saveString += "\r\n";
            saveString += "BOMBS " + bombsAtTheSameTime + "/" + bombTimer;

        }
        else if (target == Target.SCORE)
            saveString += "GETSTARS " + (int)starsTargetCount;

        else
        {

            saveString += "COLLECT ITEMS ";
            for (int i = 0; i < NumIngredients; i++)
            {
                if (ingr[i].check)
                    saveString += i + "/";
            }
        }



        saveString += "\r\n";


        //set map data
        for (int row = 0; row < maxRows; row++)
        {
            for (int col = 0; col < maxCols; col++)
            {
                saveString += (int)levelSquares[row * maxCols + col].block + "" + (int)levelSquares[row * maxCols + col].obstacle;
                //if this column not yet end of row, add space between them
                if (col < (maxCols - 1))
                    saveString += " ";
            }
            //if this row is not yet end of row, add new line symbol between rows
            if (row < (maxRows - 1))
                saveString += "\r\n";
        }
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
        {
            //Write to file
            string activeDir = Application.dataPath + @"/CakeLand/Resources/Levels/";
            string newPath = System.IO.Path.Combine(activeDir, levelNumber + ".txt");
            StreamWriter sw = new StreamWriter(newPath);
            sw.Write(saveString);
            sw.Close();
        }
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

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
        //ingr = initscript.collectedIngredients.ToArray();

        for (int i = 0; i < ingr.Length; i++)
        {

            ingr[i] = new CollectedIngredients();
        }

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
                string blocksString = line.Replace("SIZE", string.Empty).Trim();
                string[] sizes = blocksString.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                maxCols = int.Parse(sizes[0]);
                maxRows = int.Parse(sizes[1]);
                Initialize();
            }
            else if (line.StartsWith("LIMIT "))
            {
                string blocksString = line.Replace("LIMIT", string.Empty).Trim();
                string[] sizes = blocksString.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                limitType = (LIMIT)int.Parse(sizes[0]);
                limit = int.Parse(sizes[1]);

            }
            else if (line.StartsWith("COLOR LIMIT "))
            {
                string blocksString = line.Replace("COLOR LIMIT", string.Empty).Trim();
                colorLimit = int.Parse(blocksString);
                //ClearItems();

            }
            else if (line.StartsWith("STARS "))
            {
                string blocksString = line.Replace("STARS", string.Empty).Trim();
                string[] blocksNumbers = blocksString.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                star1 = int.Parse(blocksNumbers[0]);
                star2 = int.Parse(blocksNumbers[1]);
                star3 = int.Parse(blocksNumbers[2]);
            }
            else if (line.StartsWith("COLLECT ITEMS "))
            {
                string blocksString = line.Replace("COLLECT ITEMS", string.Empty).Trim();
                string[] blocksNumbers = blocksString.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

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

                }
            }
            else if (line.StartsWith("COLLECT COUNT "))
            {
                string blocksString = line.Replace("COLLECT COUNT", string.Empty).Trim();
                string[] blocksNumbers = blocksString.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
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

                }
            }
            else if (line.StartsWith("CAGE "))
            {
                string blocksString = line.Replace("CAGE ", string.Empty).Trim();
                cageHP = int.Parse(blocksString);
            }
            else if (line.StartsWith("BOMBS "))
            {
                string blocksString = line.Replace("BOMBS ", string.Empty).Trim();
                string[] blocksNumbers = blocksString.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                bombsAtTheSameTime = int.Parse(blocksNumbers[0]);
                bombTimer = int.Parse(blocksNumbers[1]);
            }
            else if (line.StartsWith("GETSTARS "))
            {
                string blocksString = line.Replace("GETSTARS ", string.Empty).Trim();
                starsTargetCount = (CollectStars)int.Parse(blocksString);
            }

            else
            { //Maps
              //Split lines again to get map numbers
                string[] st = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < st.Length; i++)
                {
                    levelSquares[mapLine * maxCols + i].block = (SquareTypes)int.Parse(st[i][0].ToString());
                    levelSquares[mapLine * maxCols + i].obstacle = (SquareTypes)int.Parse(st[i][1].ToString());
                }
                mapLine++;
            }
        }
    }


    void Initialize()
    {
        levelSquares = new SquareBlocks[maxCols * maxRows];
        for (int i = 0; i < levelSquares.Length; i++)
        {

            SquareBlocks sqBlocks = new SquareBlocks();
            sqBlocks.block = SquareTypes.EMPTY;
            sqBlocks.obstacle = SquareTypes.NONE;

            levelSquares[i] = sqBlocks;
        }
    }
}

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
#endif
using IAssetBundle;
public class AssetBundleEditor
{


    [MenuItem("Tools/Allen/Asset Bundle")]
    public static void BuildAssetBundles()
    {
        ClearAssetBundlesName();

        Pack(ABPath.getTestPath());

        string outputpath = ABPath.OutRelativePath() ;

        if (!Directory.Exists(outputpath))
        {
            Directory.CreateDirectory(outputpath);
        }
        
        BuildAssetBundleOptions babOption = BuildAssetBundleOptions.ChunkBasedCompression;//编译项目的设置 

        BuildPipeline.BuildAssetBundles(outputpath, 0, EditorUserBuildSettings.activeBuildTarget);

        AssetDatabase.Refresh();
    }

    public static void ClearAssetBundlesName()
    {
        string[] allAssetBundleNames = AssetDatabase.GetAllAssetBundleNames();
        int length = allAssetBundleNames.Length;
        string[] oldAssetBundleNames = new string[length];
        for (int i = 0; i < length; i++)
        {
            oldAssetBundleNames[i] = allAssetBundleNames[i];
        }

        for (int i = 0; i < length; i++)
        {
            AssetDatabase.RemoveAssetBundleName(oldAssetBundleNames[i], true);
        }

        length = AssetDatabase.GetAllAssetBundleNames().Length;

        if (length != 0)
            Debug.LogError("Clean old AssetBundle Name is fail");
    }

    public static List<string> needBundle = new List<string>();
    public static void Pack(string path)
    {
        if (!Directory.Exists(path))
        {
            Debug.LogError(" no exists file, Path:" + path);
            return;
        }
        DirectoryInfo folder = new DirectoryInfo(path);

        FileSystemInfo[] files = folder.GetFileSystemInfos();
        int length = files.Length;
        for (int i = 0; i < length; i++)
        {
            if (files[i] is DirectoryInfo)
            {
                Pack(files[i].FullName);
            }
            else
            {
                if (files[i].Name.EndsWith(".prefab"))
                {
                    //file(files[i].FullName);
                    //needBundle.Add(files[i].FullName);
                    calculationDep(files[i].FullName);
                }
            }
        }
    }

    public static void calculationDep(string path)
    {
        file(path);
        string source = ABhelper.Replace(path);
        source = "Assets" + source.Substring(Application.dataPath.Length);
        string[] deps = AssetDatabase.GetDependencies(source);
        for (int i = 0; i < deps.Length; i++)
        {
            file(deps[i], false);
        }
    }

    public static void file(string path, bool isChange = true)
    {
        string source = path;
        string asset_path = source;
        if (isChange)
        {
            source = ABhelper.Replace(path);
            asset_path = "Assets" + source.Substring(Application.dataPath.Length);
        }

        AssetImporter assetImporter = AssetImporter.GetAtPath(asset_path);
        string assetName = asset_path;
        if (assetName.EndsWith(".cs")) return;
        assetName = assetName.Replace(Path.GetExtension(assetName), ".unity3d");
        assetImporter.assetBundleName = assetName;
    }

}

public enum Enum_suffiex
{
    None,
    prefab,
    png,
}

/*
 * 1.收集需要打包的对应的类型的文件
 * 2.分析这些文件把他们的依赖信息保存起来，并且记录他们的依赖和被依赖次数
 */ 
public class AnalysisAssetBundel
{
    public const string Suffiex_Prefab = "prefab";


    protected string _path;
    protected Enum_suffiex _suffiex;
    protected List<FileSystemInfo> _files = new List<FileSystemInfo>();
    protected string _suffiex_str = "";
    public AnalysisAssetBundel(string path,Enum_suffiex suffiex)
    {
        _path = path;
        _suffiex = suffiex;
        _files.Clear();
        _suffiex_str=Enum.GetName(typeof(Enum_suffiex), suffiex);
        FindFile(_path);
    }

    public void FindFile(string path)
    {
        if (!Directory.Exists(path))
        {
            Debug.LogError(" no exists file, Path:" + path);
            return;
        }

        DirectoryInfo folder = new DirectoryInfo(path);

        FileSystemInfo[] files = folder.GetFileSystemInfos();
        int length = files.Length;
        for (int i = 0; i < length; i++)
        {
            if (files[i] is DirectoryInfo)
                FindFile(files[i].FullName);
            else
            {
                if (files[i].Name.EndsWith(".prefab"))
                    calculationDep(files[i].FullName);
            }
        }

    }


    public static void calculationDep(string path)
    {
        setAssetBundleName(path);
        string source = ABhelper.Replace(path);
        source = "Assets" + source.Substring(Application.dataPath.Length);
        string[] deps = AssetDatabase.GetDependencies(source);
        for (int i = 0; i < deps.Length; i++)
        {
            setAssetBundleName(deps[i], false);
        }
    }

    public static void setAssetBundleName(string path, bool isChange = true)
    {
        string source = path;
        string asset_path = source;
        if (isChange)
        {
            source = ABhelper.Replace(path);
            asset_path = "Assets" + source.Substring(Application.dataPath.Length);
        }

        AssetImporter assetImporter = AssetImporter.GetAtPath(asset_path);
        string assetName = asset_path;
        if (assetName.EndsWith(".cs")) return;
        assetName = assetName.Replace(Path.GetExtension(assetName), ".unity3d");
        assetImporter.assetBundleName = assetName;
    }

    public void AnalysisFileDependence()
    {
        if (!Directory.Exists(_path))
        {
            Debug.LogError(" no exists file, Path:" + _path);
            return;
        }
        DirectoryInfo folder = new DirectoryInfo(_path);

        FileSystemInfo[] files = folder.GetFileSystemInfos();
        int length = files.Length;
        for (int i = 0; i < length; i++)
        {
            if (files[i] is DirectoryInfo)
            {
                Pack(files[i].FullName);
            }
            else
            {
                if (files[i].Name.EndsWith(".prefab"))
                {
                    //file(files[i].FullName);
                    //needBundle.Add(files[i].FullName);
                    calculationDep(files[i].FullName);
                }
            }
        }
    }

    public void MergeFileDependence()
    { 
    
    }
}


public class ABDependInfo
{
    public string _path;
    public Dictionary<string, int> _depends = new Dictionary<string, int>();//别人依赖你
    public Dictionary<string, int> _bedepends =new Dictionary<string,int>();//你依赖别人
}

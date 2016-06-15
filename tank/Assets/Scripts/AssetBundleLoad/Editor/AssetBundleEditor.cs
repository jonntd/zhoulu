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
                if (!files[i].Name.EndsWith(".meta"))
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
        assetName = assetName.Replace(Path.GetExtension(assetName), ".unity3d");
        assetImporter.assetBundleName = assetName;
    }
}

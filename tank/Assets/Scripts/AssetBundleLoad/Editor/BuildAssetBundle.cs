#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using IAssetBundle;
#endif

public class BuildAssetBundle
{
    public static DependenciesCache depsCahe = new DependenciesCache();
    const string dependeneConfig = "Assets/config/dependeneconfig.asset";
    public static void BuildAssetBundles(AssetBundleWindowConfig config)
    {
          ClearAssetBundlesName();
          depsCahe.Clear();
          int length = config.filters.Count;
          for (int i = 0; i < length; i++)
              AnalysisFile(config.filters[i]);
 
         string outputpath = ABPath.OutRelativePath();
 
         if (!Directory.Exists(outputpath))
             Directory.CreateDirectory(outputpath);

         SaveCacheText();
         

         BuildPipeline.BuildAssetBundles(outputpath, config.bundle_options, config.bundle_platform);
        
        AssetDatabase.Refresh();
    }

    public static void SaveCacheText()
    {
        if (ABhelper.LoadAssetAtPath<DependenciesCache>(dependeneConfig) == null)
            ABhelper.SaveAssetAtPath(dependeneConfig, depsCahe);
        else
            EditorUtility.SetDirty(depsCahe);

        file(dependeneConfig, false);
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

    public static void AnalysisFile(AssetBundleWindowFilter filter)
    {
        string path = filter.path;
        string tmp_filter = filter.filter;
        if (!filter.valid) return;
        AnalysisFileInternal(filter.path,filter.filter);
    }

    public static void AnalysisFileInternal(string path,string filter)
    {
        if (!Directory.Exists(path))
        {
            Debug.LogError(" no exists file, Path:" + path);
            return;
        }

        DirectoryInfo folder = new DirectoryInfo(path);
        FileSystemInfo[] files = folder.GetFileSystemInfos();
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i] is DirectoryInfo)
                AnalysisFileInternal(files[i].FullName, filter);
            else if (files[i].Name.EndsWith(filter))
                calculationDep(files[i].FullName, files[i].Name, files[i].Extension);
        }
    }

    public static void calculationDep(string path,string assetname,string extension)
    {
        
        string ab_dep_main=file(path);
        AssetBundelDependence ad_dep = new AssetBundelDependence(assetname, extension);
        ad_dep.addDependence(ab_dep_main);
        string source = ABhelper.Replace(path);
        string source_1 = ABhelper.absoluteToAssetRelative(path);
        source = "Assets" + source.Substring(Application.dataPath.Length);
        string[] deps = AssetDatabase.GetDependencies(source);
        for (int i = 0; i < deps.Length; i++)
        {
            string ab_dep=file(deps[i], false);
            if (!string.IsNullOrEmpty(ab_dep))
                ad_dep.addDependence(ab_dep);
        }
        depsCahe.addAssetBundle(ad_dep);
    }

    public static string file(string path, bool isChange = true)
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
        if (assetName.EndsWith(".cs")) return string.Empty;
        assetName = assetName.Replace(Path.GetExtension(assetName), ".ab");
        assetImporter.assetBundleName = assetName;
        return assetName;
    }

}

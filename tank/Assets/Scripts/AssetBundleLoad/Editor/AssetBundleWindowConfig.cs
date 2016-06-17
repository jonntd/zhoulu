#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
#endif
public class AssetBundleWindowConfig : ScriptableObject
{
    public BuildAssetBundleOptions bundle_options = BuildAssetBundleOptions.None;
    public BuildTarget bundle_platform = EditorUserBuildSettings.activeBuildTarget;
    public List<AssetBundleWindowFilter> filters = new List<AssetBundleWindowFilter>();

    public void removeInvalidFilter()
    {
        List<AssetBundleWindowFilter> tmps = new List<AssetBundleWindowFilter>();
        int length = filters.Count;
        for (int i = 0; i < length; i++)
        {
            if (!filters[i].isInvalid()) tmps.Add(filters[i]);
        }
        filters.Clear();
        filters.AddRange(tmps);
    }
}

[System.Serializable]
public class AssetBundleWindowFilter
{
    public bool valid = true;
    public string path = string.Empty;
    public string filter = ".prefab";

    public bool isInvalid()
    {
        if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(filter))
            return true;
        return false;
    }

}
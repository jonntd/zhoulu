#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;
using System.IO;
#endif

namespace IAssetBundle
{
    /// <summary>
    /// 说有材质合并为一个，其他统计
    /// </summary>
    public class AssetBundleStrategy
    {
        public List<string> _path_list;
        public Dictionary<string, AssetBundleRef> _dic_ref;
        public List<FileSystemInfo> _files;
        public AssetBundleStrategy(List<FileSystemInfo> files)
        {
            _files = new List<FileSystemInfo>(files);
            _path_list = new List<string>();
            _dic_ref = new Dictionary<string, AssetBundleRef>();
        }

        public List<AssetBundleBuild> parse()
        {
            List<AssetBundleBuild> ab_builds = new List<AssetBundleBuild>();

            int length = _files.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundleBuild ab_build = new AssetBundleBuild();
                ab_build.assetBundleName = string.Format("{0}{1}",_files[i].Name,_files[i].Extension);

                //calculationDep(_files[i].FullName, _files[i].Name, _files[i].Extension);
            }

            return ab_builds;
        }

        public void calculationDep(string path, string assetname, string extension)
        {
//             AssetBundelDependence ad_dep = new AssetBundelDependence(assetname, extension);
//             string source = ABhelper.Replace(path);
//             string source_1 = ABhelper.absoluteToAssetRelative(path);
//             source = "Assets" + source.Substring(Application.dataPath.Length);
//             string[] deps = AssetDatabase.GetDependencies(source);
//             for (int i = 0; i < deps.Length; i++)
//             {
//                 string ab_dep = file(deps[i], false);
//                 if (!string.IsNullOrEmpty(ab_dep))
//                     ad_dep.addDependence(ab_dep);
//             }
//             depsCahe.addAssetBundle(ad_dep);
        }

    }

    public class AssetBundleRef
    {
        public string path;
        public int reference;
    }
}


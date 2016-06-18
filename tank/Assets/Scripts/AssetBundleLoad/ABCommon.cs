using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

namespace IAssetBundle
{
    public class ABCommon
    {



    }

    public class ABPlatform
    {
        public static string getPlatformFolder(BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.Android:
                    return "Android";
                case BuildTarget.iOS:
                    return "iOS";
                case BuildTarget.WebPlayer:
                    return "WebPlayer";
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    return "Windows";
                default:
                    return null;
            }
        }

        public static string getPlatformFolder(RuntimePlatform platform)
        {
            switch (platform)
            {
                case RuntimePlatform.Android:
                    return "Android";
                case RuntimePlatform.IPhonePlayer:
                    return "iOS";
                case RuntimePlatform.WindowsWebPlayer:
                case RuntimePlatform.OSXWebPlayer:
                    return "WebPlayer";
                case RuntimePlatform.WindowsPlayer:
                    return "Windows";
                default:
                    return null;
            }
        }
    }

    public class ABhelper
    {
        /// <summary>
        /// 加载本地资源 编辑器下使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T LoadAssetAtPath<T>(string path) where T : Object
        {
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        /// <summary>
        /// 在指定目录下保存文件 编辑下使用
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        public static void SaveAssetAtPath(string path,Object obj)
        {
            path = Replace(path);
            string folder = path.Substring(0, path.LastIndexOf("/"));
            string absolute_folder = ABhelper.assetRelativeToAbsolute(folder);

            if (!Directory.Exists(absolute_folder))
                Directory.CreateDirectory(absolute_folder);

            AssetDatabase.CreateAsset(obj, path);
        }


        public static string absoluteToAssetRelative(string path)
        {
            string relativePath = "Assets/" + path.Substring(Application.dataPath.Length + 1);
            return relativePath.Replace('\\', '/');
        }

        public static string assetRelativeToAbsolute(string path)
        {
            string absolutePath = Application.dataPath + path.Substring("Assets".Length);
            return absolutePath.Replace('\\', '/');
        }

        public static string Replace(string s)
        {
            return s.Replace("\\", "/");
        }
    }

    public class ABPath
    {
        private static string _bundlePathCache = string.Empty;
        private static string _AssetBundlesOutputPath = "AssetBundles";
        public static string bundlePath
        {
            get
            {
                if (string.IsNullOrEmpty(_bundlePathCache))
                {

#if UNITY_ANDROID
		            _bundlePathCache = string.Format("jar:file://{0}!/assets/", Application.dataPath);
#elif UNITY_IPHONE
                    _bundlePathCache = string.Format("{0}/Raw/", Application.dataPath);
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
                    _bundlePathCache = string.Format("file://{0}/StreamingAssets/", Application.dataPath);
#else
                    _bundlePathCache = string.Format("file://{0}/../AssetBundles/", Application.dataPath);
#endif
                }
                return _bundlePathCache;
            }
        }

        /// <summary>
        /// 输出
        /// </summary>
        /// <returns></returns>
        public static string OutRelativePath()
        {
            string path = Path.Combine(_AssetBundlesOutputPath, ABPlatform.getPlatformFolder(EditorUserBuildSettings.activeBuildTarget));
            return path;
        }

        public static string getTestPath()
        {
            return Application.dataPath + "/Art/Resources/character/weapon";
        }

        public static string GetPrefixPath()
        {
            string platformFolderForAssetBundles =
#if UNITY_EDITOR
 ABPlatform.getPlatformFolder(EditorUserBuildSettings.activeBuildTarget);
#else
			ABPlatform.getPlatformFolder(Application.platform);
#endif

            // Set base downloading url.
            string relativePath = GetStreamingAssetsPath();
            relativePath = relativePath + "/" + _AssetBundlesOutputPath + "/" + platformFolderForAssetBundles + "/";
            return relativePath;
        }

        public static string GetStreamingAssetsPath()
        {
            if (Application.isEditor)
                return "file://" + System.Environment.CurrentDirectory.Replace("\\", "/");
            else if (Application.isWebPlayer)
                return System.IO.Path.GetDirectoryName(Application.absoluteURL).Replace("\\", "/") + "/StreamingAssets";
            else if (Application.isMobilePlatform || Application.isConsolePlatform)
                return Application.streamingAssetsPath;
            else // For standalone player.
                return "file://" + Application.streamingAssetsPath;
        }

        public static void SetSourceAssetBundleDirectory(string relativePath)
        {
            string url = GetStreamingAssetsPath() + relativePath;
        }

    }

    public class AssetBundleInfo
    {
        public string _name;
        public string _CRC;
        public string _hashCode;
        public List<string> _Dependencies = new List<string>();

        public AssetBundleInfo(string name)
        {

        }
    }

    [System.Serializable]
    public class DependenciesCache : ScriptableObject
    {
        public List<AssetBundelDependence> _dependences = new List<AssetBundelDependence>();

        public void addAssetBundle(AssetBundelDependence ab_dep)
        {
            _dependences.Add(ab_dep);
        }

        public void Clear()
        {
            _dependences.Clear();
        }
    }

    [System.Serializable]
    public class AssetBundelDependence
    {
        public string _assetbundle;
        public List<string> _dependences;
        public string _crc;
        public string _hash;
        public string _extension;

        public AssetBundelDependence(string assetbundle, string extension)
        {
            _assetbundle = assetbundle.Substring(0, assetbundle.Length - extension.Length);
            _dependences = new List<string>();
            _crc = string.Empty;
            _hash = string.Empty;
            _extension = extension;
        }

        public string getCRC()
        {
            return _crc;
        }

        public string getHashCode()
        {
            return _hash;
        }

        public void setDependences(string[] deps)
        {
            _dependences.Clear();
            _dependences.AddRange(deps);
        }

        public void addDependence(string dep)
        {
            _dependences.Add(dep);
        }

        public List<string> getDependence()
        {
            return _dependences;
        }

        public string getExtension()
        {
            return _extension;
        }
    }

}
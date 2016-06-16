using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

namespace IAssetBundle
{
    public class ABCommon
    {

        /*
        public static string GetPrefixPath()
        {
            string platformFolderForAssetBundles =
#if UNITY_EDITOR
            GetPlatformFolderForAssetBundles(EditorUserBuildSettings.activeBuildTarget);
#else
			GetPlatformFolderForAssetBundles(Application.platform);
#endif

            // Set base downloading url.
            string relativePath = GetRelativePath();
            relativePath = relativePath + "/AssetBundles/" + platformFolderForAssetBundles + "/";
            Debug.Log(relativePath);
            return relativePath;
        }
        */




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
            string relativePath = GetRelativePath();
            relativePath = relativePath +"/"+ _AssetBundlesOutputPath +"/"+ platformFolderForAssetBundles + "/";
            return relativePath;
        }

        public static string GetRelativePath()
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
    }
}
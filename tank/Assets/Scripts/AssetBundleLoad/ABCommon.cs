using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using Object = UnityEngine.Object;
using System;
using GameEngine.Tools;
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

        public static string getAssetBundelPrePath()
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

    /*
    public class AssetBundleInfo
    {
        public string _name;
        public string _CRC;
        public string _hashCode;
        public List<string> _Dependencies = new List<string>();

        public AssetBundleInfo(string name)
        {

        }
    }*/

    public class DependenciesCache 
    {
        public string dependeneConfig = "Assets/config/dependeneconfig.txt";
        public List<AssetBundelDependence> _dependences = new List<AssetBundelDependence>();
        public Dictionary<string,AssetBundelDependence> _dic_ab_dep=new Dictionary<string,AssetBundelDependence>();
        public void addAssetBundle(AssetBundelDependence ab_dep)
        {
            _dependences.Add(ab_dep);
        }

        public void Clear()
        {
            _dependences.Clear();
        }

        public string des_ab_name = "AssetBundle Name:";
        public string des_extension = "Extension:";
        public string des_crc = "CRC:";
        public string des_hashcode = "HashCode:";
        public string des_dep_count = "Dependences Count:";
        public void saveLine()
        {
            List<string> infos = new List<string>();
            int length=_dependences.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundelDependence ab_dep = _dependences[i];
                infos.Add(des_ab_name + ab_dep.getAssetBundleName().ToLower());
                infos.Add(des_extension + ab_dep.getExtension().ToLower());
                infos.Add(des_crc + ab_dep.getCRC().ToLower());
                infos.Add(des_hashcode + ab_dep.getHashCode().ToLower());
                infos.Add(des_dep_count + ab_dep.getDependence().Count.ToString().ToLower());
                List<string> ab_dep_s = ab_dep.getDependence();
                int child_deps = ab_dep_s.Count;
                for (int j = 0; j < child_deps; j++)
                {
                    infos.Add(ab_dep_s[j].ToLower());
                }

                infos.Add("");
            }

            ToolsFile.CreateFile(ABhelper.assetRelativeToAbsolute(dependeneConfig), infos);
        }

        public void resetDependencies(TextAsset asset)
        {
            /*
            List<string> infos = new List<string>();
            ByteReader reader = new ByteReader(asset);
            while (reader.canRead)
            {
                string tmp_ab_name = reader.ReadLine();
                if (tmp_ab_name == null) break;
                if (tmp_ab_name.Trim().Length == 0) continue;

                tmp_ab_name = tmp_ab_name.Substring(des_ab_name.Length);

                string tmp_extension = reader.ReadLine();
                tmp_extension = tmp_extension.Substring(des_extension.Length);

                string tmp_crc = reader.ReadLine();
                tmp_crc = tmp_crc.Substring(des_crc.Length);

                string tmp_hashcode = reader.ReadLine();
                tmp_hashcode = tmp_hashcode.Substring(des_hashcode.Length);

                string tmp_dep_count = reader.ReadLine();
                tmp_dep_count = tmp_dep_count.Substring(des_dep_count.Length);
                int count = int.Parse(tmp_dep_count.Trim());
                AssetBundelDependence ab_dep = new AssetBundelDependence(tmp_ab_name + tmp_extension, tmp_extension);
                for (int i = 0; i < count; i++)
                {
                    string child_dep_name = reader.ReadLine();
                    ab_dep.addDependence(child_dep_name);
                }
                _dependences.Add(ab_dep);
                _dic_ab_dep.Add(ab_dep.getAssetBundleFullName(), ab_dep);
               
            }
           */
        }
        
        public int getDependenciesCount()
        {
            return _dependences.Count;
        }

        public AssetBundelDependence getAssetBundelDependence(string name)
        {
            if (_dic_ab_dep.ContainsKey(name))
                return _dic_ab_dep[name];
            return null;
        }

    }

    public class AssetBundelDependence
    {
        public string _assetbundle;
        public List<string> _dependences;
        public uint _crc;
        public Hash128 _hash;
        public string _extension;
        public string _main_assetbundle_name;

        public AssetBundelDependence(string assetbundle, string extension)
        {
            _assetbundle = assetbundle.Substring(0, assetbundle.Length - extension.Length);
            _main_assetbundle_name = assetbundle;
            _dependences = new List<string>();
           
            _extension = extension;
         
        }

        public string getAssetBundleName()
        {
            return _assetbundle;
        }

        public string getAssetBundleFullName()
        {
            return _main_assetbundle_name;
        }

        public string getCRC()
        {
            return _crc.ToString();
        }

        public string getHashCode()
        {
            return _hash.ToString();
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

        public void setCRCAndHashCode(string path)
        {
            //path=ABhelper.absoluteToAssetRelative(path);
            bool crc_flag = BuildPipeline.GetCRCForAssetBundle(path, out _crc);
            bool hash_flag = BuildPipeline.GetHashForAssetBundle(path, out _hash);
            if (!crc_flag)
                Debug.LogError("Error Path CRC:" + path);
            if (!hash_flag)
                Debug.LogError("Error Path hash:" + path);


        }
    }

}
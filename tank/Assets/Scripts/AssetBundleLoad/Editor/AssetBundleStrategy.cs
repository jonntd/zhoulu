#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;
using System.IO;
#endif

namespace IAssetBundle.build
{
    /// <summary>
    /// 针对要打包的资源进行分析
    /// 所有依赖的资源，无论主资源还是依赖资源
    /// 主资源列表
    /// </summary>
    public abstract class AssetBundleAnalyze
    {
        protected List<FileSystemInfo> _files = new List<FileSystemInfo>();
        protected Dictionary<string, AssetBundleDependRef> _ab_deps_ref = new Dictionary<string, AssetBundleDependRef>();
        protected Dictionary<string, AssetBundleMain> _ab_mains = new Dictionary<string, AssetBundleMain>();

        public AssetBundleAnalyze(List<FileSystemInfo> files)
        {
            _files = new List<FileSystemInfo>(files);
            analyzeFile();
        }

        protected void analyzeFile()
        {
            
            int length = _files.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundleMain ab_main = new AssetBundleMain(_files[i]);
                string path_main = ab_main.getMainPath(); ;
                string[] deps = AssetDatabase.GetDependencies(path_main);
                for (int j = 0; j < deps.Length; j++)
                {
                    if (deps[j].EndsWith(".cs")) continue;
                    ab_main.addDepends(deps[j]);
                    addAssetBundelRef(deps[j], path_main);
                }
                _ab_mains.Add(path_main, ab_main);
            }
            onAnalyze();
        }

        /// <summary>
        /// path 从属于 depend_path
        /// </summary>
        private void addAssetBundelRef(string depenp_path, string path)
        {
            if (_ab_deps_ref.ContainsKey(depenp_path))
                _ab_deps_ref[depenp_path].addRef(path);
            else
            {
                AssetBundleDependRef ab_ref = new AssetBundleDependRef(depenp_path);
                ab_ref.addRef(path);
                _ab_deps_ref.Add(depenp_path, ab_ref);
            }
        }

        protected abstract void onAnalyze();

        public abstract List<AssetBundleBuild> getAssetBundleBuilds();
    }

    /// <summary>
    /// 共享资源
    /// </summary>
    public class ABAnalyzeShareShader : AssetBundleAnalyze
    {
        public ABAnalyzeShareShader(List<FileSystemInfo> files)
            : base(files)
        {

        }

        protected override void onAnalyze()
        {

        }

        public override List<AssetBundleBuild> getAssetBundleBuilds()
        {
            return null;
        }
    }

    /// <summary>
    /// 做打包资源策略
    /// 只依赖于主资源，打包到一个包中,其他被多次引用的资源单独打包
    /// </summary>
    public class ABAnalyzeNormal : AssetBundleAnalyze
    {

        private List<AssetBundleBuild> _ab_builds = new List<AssetBundleBuild>();

        public ABAnalyzeNormal(List<FileSystemInfo> files)
            : base(files)
        {

        }

        public Dictionary<string, List<string>> _onCollectionAssetBundle()
        {
            Dictionary<string, List<string>> ab_builds = new Dictionary<string, List<string>>();
            //只依赖主资源的资源
            foreach (var data in _ab_deps_ref)
            {
                AssetBundleDependRef ab_dep_ref = data.Value;

                string path_main = ab_dep_ref.getOnlyDependPath();
                if (string.IsNullOrEmpty(path_main)) continue;

                My.assert(_ab_mains.ContainsKey(path_main), "无效的资源" + path_main);
                if (!_ab_mains.ContainsKey(path_main)) continue;

                string ab_path = ab_dep_ref.getABPath();
                if (ab_builds.ContainsKey(path_main))
                    ab_builds[path_main].Add(ab_path);
                else
                {
                    List<string> depends = new List<string>();
                    depends.Add(ab_path);
                    ab_builds.Add(path_main, depends);
                }
            }

            //对多依赖的资源 单独打包
            foreach (var data in _ab_deps_ref)
            {
                AssetBundleDependRef ab_dep_ref = data.Value;

                string only_ref_path = ab_dep_ref.getOnlyDependPath();
                if (!string.IsNullOrEmpty(only_ref_path)) continue;
                List<string> depends = new List<string>();
                depends.Add(ab_dep_ref.getABPath());
                ab_builds.Add(ab_dep_ref.getABPath(), depends);
            }
            return ab_builds;
        }

        public void _onAssetBundle(Dictionary<string, List<string>> coll_ab)
        {
            _ab_builds.Clear();

            foreach (var data in coll_ab)
            {
                AssetBundleBuild ab_build = new AssetBundleBuild();
                string key = data.Key;
                List<string> value = data.Value;
                ab_build.assetBundleName = key;
                ab_build.assetNames = value.ToArray(); ;
                _ab_builds.Add(ab_build);
            }
        }

        public void _checkMissionAssetBundle(Dictionary<string, List<string>> coll_ab)
        {
            List<List<string>> tmp_colls = new List<List<string>>(coll_ab.Values);
            List<string> tmp_refs = new List<string>(_ab_deps_ref.Keys);
            for (int i = 0; i < tmp_colls.Count; i++)
            {
                List<string> tmp_coll = tmp_colls[i];
                for (int j = 0; j < tmp_coll.Count; j++)
                {
                    string tmp_path = tmp_coll[j];
                    for (int z = 0; z < tmp_refs.Count; z++)
                    {
                        if (tmp_path == tmp_refs[z])
                            tmp_refs.RemoveAt(z);
                    }
                }
            }

            My.assert(tmp_refs.Count == 0, "出现没有被应用到的资源，注意，注意");
        }

        #region Override

        protected override void onAnalyze()
        {

            Dictionary<string, List<string>> builds = _onCollectionAssetBundle();
            _checkMissionAssetBundle(builds);
            _onAssetBundle(builds);
        }

        public override List<AssetBundleBuild> getAssetBundleBuilds()
        {
            return _ab_builds;
        }

        #endregion
    }

    /// <summary>
    /// 单独的依赖资源
    /// 被引用的次数，资源地址，依赖于哪些主资源
    /// </summary>
    public class AssetBundleDependRef
    {
        private string _path = string.Empty;
        private int _reference = 0;
        private Dictionary<string, int> _dic_depend = new Dictionary<string, int>();
        private bool _change = false;//参考 目前无效
        public AssetBundleDependRef(string path)
        {
            _path = path;
        }

        public void addRef(string path)
        {
            My.assert(!_dic_depend.ContainsKey(path), _path + "有重复地址出现" + path);
            _dic_depend.Add(path, 1);
        }

        public string getABPath()
        {
            return _path;
        }

        public int getRefCount()
        {
            if (_dic_depend.Count == 0) return 0;
            if (_reference > 0) return _reference;
            int reference = 0;
            foreach (var data in _dic_depend)
            {
                reference += data.Value;
            }
            _reference = reference;
            return _reference;
        }

        public string getOnlyDependPath()
        {
            if (!isOneInDepends())
                return string.Empty;
            foreach (var data in _dic_depend)
                return data.Key;

            return string.Empty;
        }

        public bool isOneInDepends()
        {
            return getRefCount() == 1;
        }
    }

    /// <summary>
    /// 打包的主资源
    /// 资源名称，地址，后缀名
    /// 依赖文件
    /// </summary>
    public class AssetBundleMain
    {
        private string _name;
        private string _path;
        private string _extension;
        private List<string> _depends = new List<string>();

        public AssetBundleMain(FileSystemInfo info)
        {
            _name = info.Name.Substring(0, info.Name.Length - info.Extension.Length);
            _path = ABhelper.absoluteToAssetRelative(info.FullName);
            _extension = info.Extension;
        }

        public void addDepends(string path)
        {
            _depends.Add(path);
        }

        public string getMainPath()
        {
            return _path;
        }

        public string getName()
        {
            return _name;
        }

        public string getExtension()
        {
            return _extension;
        }

        public List<string> getDepends()
        {
            return _depends;
        }

        public bool isContains(string path)
        {
            int length = _depends.Count;
            for (int i = 0; i < length; i++)
            {
                if (path == _depends[i])
                    return true;
            }
            return false;
        }
    }

}


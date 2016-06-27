using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;
namespace IAssetBundle.Load
{


    public class AssetBundleInfo
    {
        public string _assetbundle_name;
        public string[] _deps;
        public string _main_ab;
        public Hash128 _hashcode;
        public AssetBundleInfo(string ab_name, string[] deps,Hash128 hashcode)
        {
            _assetbundle_name = ab_name;
            _hashcode = hashcode;
            _initMainName(deps);
        }

        public void _initMainName(string[] deps)
        {
            List<string> tmp = new List<string>(deps);
            tmp.Add(_assetbundle_name);
            _deps = tmp.ToArray();

            string[] strs = _assetbundle_name.Split('/');
            _main_ab = strs[strs.Length - 1];
            int index = _main_ab.IndexOf('.');

            if (index <= 0) index = _main_ab.Length;

            _main_ab = _main_ab.Substring(0, index);
        }

        public string[] getDeps()
        {
            return _deps;
        }

        public string getABLoadPath()
        {
            return _assetbundle_name;
        }
    }

    /// <summary>
    /// AB颗粒 最小的单位
    /// </summary>
    public class AssetBundleGrain
    {
        public Object[] _objs;
        public string _path = string.Empty;
        public string _name_ab = string.Empty;//last/ 和 last. 中间
        public int _refs = 0;
        public AssetBundleGrain(string path, Object[] objs)
        {
            _path = path;
            _objs = objs;
            _parseName();
        }

        public void _parseName()
        {
            string[] strs = _path.Split('/');
            _name_ab = strs[strs.Length - 1];
            int index = _name_ab.IndexOf('.');
            if (index <= 0)
                index = _name_ab.Length;
            _name_ab = _name_ab.Substring(0, index);
        }

        public void AddRef()
        {
            _refs++;
        }

        public void removeRef()
        {
            _refs--;
            My.assert(_refs >=0, "_path：" + _path+" 资源的引用："+_refs);
        }

        public string getABPath()
        {
            return _path;
        }

        public T getMainAsset<T>() where T : Object
        {
            My.assert(!string.IsNullOrEmpty(_name_ab), "主资源名称有问题：(" + _name_ab + ")");
            for (int i = 0; i < _objs.Length; i++)
            {

                if (_objs[i].name == _name_ab && _objs[i] is T)
                    return _objs[i] as T;
            }
            return null;
        }

        public int getRef()
        {
            return _refs;
        }

    }


}


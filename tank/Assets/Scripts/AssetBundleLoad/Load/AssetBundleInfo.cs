using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Object=UnityEngine.Object;
namespace IAssetBundle.Load
{
    public class AssetBundleInfo 
    {
        public string _assetbundle_name;
        public string[] _deps;
        public string _main_assetbundle;
        //public string _CRC;
        //public string _hashCode;
        public List<AssetBundleGrain> _ab_grains=new List<AssetBundleGrain>();


        public AssetBundleInfo(string ab_name,string[] deps)
        {
            _assetbundle_name = ab_name;
            List<string> tmp = new List<string>(deps);
            tmp.Add(ab_name);
            //_main_assetbundle = main;
            _deps = tmp.ToArray() ;
            _parseName();
        }

        public void _parseName()
        {
               string[] strs= _assetbundle_name.Split('/');
            _main_assetbundle = strs[strs.Length-1];
            int index=_main_assetbundle.IndexOf('.');
            if(index<=0)
                index=_main_assetbundle.Length;
            _main_assetbundle = _main_assetbundle.Substring(0, index);
        }


        public string[] getLoadUrl()
        {
            return _deps;
        }

        public void addAssetBundleGrain(string path, Object[] objs)
        {
            AssetBundleGrain ab_grain = new AssetBundleGrain(path, objs);
            _ab_grains.Add(ab_grain);
        }


        public T getMainAsset<T>() where T :Object
        {
            for (int i = 0; i < _ab_grains.Count; i++)
            {
                if(_ab_grains[i].getPath()==_assetbundle_name)
                {
                    return _ab_grains[i].getMainAsset<T>();
                }
            }
            return null;
        }

        
      
    }

    /// <summary>
    /// AB颗粒 最小的单位
    /// </summary>
    public class AssetBundleGrain
    {
        public Object[] _objs;
        public string _path=string.Empty;
        public string _main_assetbundle = string.Empty;
        public int _refs=0;
        public AssetBundleGrain(string path, Object[] objs)
        {
            _path = path;
            _objs = objs;
            _parseName();
        }

        public void _parseName()
        {
            string[] strs = _path.Split('/');
            _main_assetbundle = strs[strs.Length - 1];
            int index = _main_assetbundle.IndexOf('.');
            if (index <= 0)
                index = _main_assetbundle.Length;
            _main_assetbundle = _main_assetbundle.Substring(0, index);
        }

        public void AddRef()
        {
            _refs++;
        }

        public void removeRef()
        {
            _refs--;
        }

        public string getPath()
        {
            return _path;
        }


        public T getMainAsset<T>() where T :Object
        {
            My.assert(!string.IsNullOrEmpty(_main_assetbundle),"主资源名称有问题：("+_main_assetbundle+")");
            for (int i = 0; i < _objs.Length; i++)
            {

                if (_objs[i].name == _main_assetbundle && _objs[i] is T)
                    return _objs[i] as T;
            }
            return null;
        }

    }

    
}


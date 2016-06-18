using System;
using UnityEngine;

namespace IAssetBundle
{
    public class LoadAssetBundle
    {
        private AssetBundle _assetbundle;
        private string _assetbundle_name;
        private int _referenced_count;
        public LoadAssetBundle(AssetBundle assetBundle, string name)
        {
            _assetbundle = assetBundle;
            _assetbundle_name = name;
            _referenced_count = 1;
        }

        public void Retain()
        {
            _referenced_count++;
        }

        public void Release()
        {
            _referenced_count--;
            //当引用计数为0时,卸载资源
            if (_referenced_count > 0) return;
            _assetbundle.Unload(true);
            //LoadAssetCache.FreeBundle(_assetbundle_name);
        }

        public int RetainCount()
        {
            return _referenced_count;
        }

        public AssetBundle getAssetBundel()
        {
            return _assetbundle;
        }

        public string getAssetBundle()
        {
            return _assetbundle_name;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace IAssetBundle.Load
{
    public class ABManager
    {
        private ABManagerInfo _info;

        public ABManager()
        {
            _info = new ABManagerInfo();
        }
    }

    public class ABManagerInfo
    {
        public Dictionary<string, AssetBundleGrain> _loadABs = new Dictionary<string, AssetBundleGrain>();
        public Dictionary<string, WWW> _downLoadWWWs = new Dictionary<string, WWW>();
        public Dictionary<string, string> _downLoad_error = new Dictionary<string, string>();
        public Dictionary<string, LoadOperation> _inprogress_operations = new Dictionary<string, LoadOperation>();


        public string _base_downloading_url = "";
        public string basedownLoadUrl
        {
            get { return _base_downloading_url; }
            set { _base_downloading_url = value; }
        }

        public void addLoadAB(string key, AssetBundleGrain value)
        {
            removeLoadAB(key);
            _loadABs.Add(key, value);
        }

        public void removeLoadAB(string key)
        {
            if (_loadABs.ContainsKey(key)) _loadABs.Remove(key);
        }

        public void addWWW(string key, WWW value)
        {
            removeWWW(key);
            _downLoadWWWs.Add(key, value);
        }

        public void removeWWW(string key)
        {
            if (_downLoadWWWs.ContainsKey(key)) _downLoadWWWs.Remove(key);
        }

        public void addDownLoadError(string key, string value)
        {
            removeDownLoadError(key);
            _downLoad_error.Add(key, value);
        }

        public void removeDownLoadError(string key)
        {
            if (_downLoad_error.ContainsKey(key)) _downLoad_error.Remove(key);
        }

        public void addLoadOperation(string key, LoadOperation value)
        {
            removeLoadOperation(key);
            _inprogress_operations.Add(key, value);
        }

        public void removeLoadOperation(string key)
        {
            if (_inprogress_operations.ContainsKey(key)) _inprogress_operations.Remove(key);
        }
    }
}


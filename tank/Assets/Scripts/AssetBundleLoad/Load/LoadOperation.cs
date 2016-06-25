using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using IAssetBundle;
using UnityEditor;

namespace IAssetBundle.Load
{
    public abstract class LoadOperation : IEnumerator
    {
        public object Current { get { return null; } }

        public bool MoveNext()
        {
            return !IsDone();
        }

        public void Reset() { My.LogError("LoadOperation不能Reset"); }

        public virtual bool Update() { return false; }

        protected abstract bool IsDone();

    }

    public abstract class AssetBundleLoadOperation : LoadOperation
    {
        public abstract T getAsset<T>() where T : Object;
    }

    public abstract class WWWLoadOperation : LoadOperation
    {
        public bool done { get; protected set; }

        public string assetBundleName { get; private set; }

        public AssetBundle assetBundle { get; protected set; }

        public string error { get; protected set; }

        public WWWLoadOperation(string assetBundleName)
        {
            this.assetBundleName = assetBundleName;
        }

        protected override bool IsDone()
        {
            return FinishDownload();
        }

        protected abstract bool FinishDownload();

        public abstract string GetSourceURL();
    }

    public class AssetBundleDownloadFromWebOperation : WWWLoadOperation
    {
        private WWW _www;
        private string _url;

        public AssetBundleDownloadFromWebOperation(string assetbundle_name, WWW www)
            : base(assetbundle_name)
        {
            _url = www.url;
            this._www = www;
        }

        protected override bool FinishDownload()
        {
            if (done) return true;
            if (_www == null || !_www.isDone) return false;

            done = true;
            error = _www.error;
            if (error != null && error.Length > 0)
                My.assert(true, "WWW加载资源错误，ABName" + assetBundleName + " url:" + _url);
            else
            {
                AssetBundle bundle = _www.assetBundle;
                if (bundle == null)
                    error = string.Format("{0}是一个无效的资源", assetBundleName);
                else
                    assetBundle = bundle;
            }

            _www.Dispose();
            _www = null;
            return true;
        }

        public override string GetSourceURL()
        {
            return _url;
        }
    }

    public class AssetForWWWOperation : LoadOperation
    {

        protected string _download_error = null;
        protected AssetBundleRequest _request = null;
        protected AssetBundle _assetbundle = null;
        public AssetForWWWOperation(AssetBundle assetbundle, string download_error)
        {
            _assetbundle = assetbundle;
            _download_error = download_error;
            _init();
        }

        public void _init()
        {
            if (_assetbundle == null) return;
            _request = _assetbundle.LoadAllAssetsAsync();
        }

        protected override bool IsDone()
        {
            if (_request == null && _download_error != null && _download_error.Length > 0)
                return true;
            return _request != null && _request.isDone;
        }

        public Object[] LoadAllAsset()
        {
            if (!IsDone()) return null;
            if (_request == null) return null;
            return _request.allAssets;
        }

        public void onDestroy()
        {
            if (_assetbundle == null) return;
            _assetbundle.Unload(false);
            _assetbundle = null;
        }
    }

    public class AssetOperationFull : AssetBundleLoadOperation
    {
        protected string _assetbundle_name;
        protected string _download_error = null;
        protected AssetBundleRequest _request = null;

        public AssetOperationFull(string bundleName)
        {
            _assetbundle_name = bundleName;
        }

        public override T getAsset<T>()
        {
            if (_request != null && _request.isDone)
                return _request.asset as T;
            else
                return null;
        }

        public override bool Update()
        {
            if (_request == null)
                return false;


            AssetBundle assetbundle = null;// ABManager.GetLoadedAssetBundle(m_AssetBundleName, out _download_error);
            if (assetbundle != null)
            {
                _request = assetbundle.LoadAllAssetsAsync();
                return false;
            }
            else
            {
                return true;
            }
        }

        protected override bool IsDone()
        {
            if (_request == null && _download_error != null)
                return true;
            Debug.Log("轮询" + _assetbundle_name);
            return _request != null && _request.isDone;
        }
    }
}


using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;
using IAssetBundle;
using UnityEditor;

namespace IAssetBundle
{
    public abstract class LoadOperation : IEnumerator
    {
        public object Current { get { return null; } }

        public bool MoveNext()
        {
            return !IsDone();
        }

        public void Reset() { }

        public abstract bool Update();

        protected abstract bool IsDone();

    }

    public abstract class AssetLoadOperation : LoadOperation
    {
        public abstract T getAsset<T>() where T : Object;
    }

    public abstract class WWWLoadOperation : LoadOperation
    {
        public bool done { get; private set; }

        public string assetBundleName { get; private set; }

        public LABInfo assetBundle { get; protected set; }

        public string error { get; protected set; }

        public WWWLoadOperation(string assetBundleName)
        {
            this.assetBundleName = assetBundleName;
        }

        public override bool Update()
        {
            if (!done && downloadIsDone)
            {
                FinishDownload();
                done = true;
            }

            return !done;
        }

        protected override bool IsDone()
        {
            return done;
        }

        protected abstract bool downloadIsDone { get; }

        protected abstract void FinishDownload();

        public abstract string GetSourceURL();
    }

    public class AssetBundleDownloadFromWebOperation : WWWLoadOperation
    {
        WWW _www;
        string _url;

        public AssetBundleDownloadFromWebOperation(string assetBundleName, WWW www)
            : base(assetBundleName)
        {
            if (www == null)
                throw new System.ArgumentNullException("www");
            _url = www.url;
            this._www = www;
        }

        protected override bool downloadIsDone { get { return _www != null && _www.isDone; } }

        protected override void FinishDownload()
        {
            error = _www.error;
            if (!string.IsNullOrEmpty(error))
            {
                return;
            }
               

            AssetBundle bundle = _www.assetBundle;
            if (bundle == null)
                error = string.Format("{0} is not a valid asset bundle.", assetBundleName);
            else
                assetBundle = new LABInfo(_www.assetBundle);

            _www.Dispose();
            _www = null;
        }

        public override string GetSourceURL()
        {
            return _url;
        }
    }


    public class AssetOperationFull : AssetLoadOperation
    {
        protected string _assetbundle_name;
        protected string _asset_name;
        protected string _download_error = null;
        protected System.Type _type;
        protected AssetBundleRequest _request = null;

        public AssetOperationFull(string bundleName, string assetName, System.Type type)
        {
            _assetbundle_name = bundleName;
            _asset_name = assetName;
            _type = type;
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


            LABInfo bundle = null; //ABManager.GetLoadedAssetBundle(m_AssetBundleName, out _download_error);
            if (bundle != null)
            {
                _request = bundle._asset_bundle.LoadAssetAsync(_asset_name, _type);
                return false;
            }
            else
            {
                return true;
            }
            return false;
        }

        protected override bool IsDone()
        {
            if (_request == null && _download_error != null)
                return true;
            Debug.Log("轮询" + _asset_name);
            return _request != null && _request.isDone;
        }
    }

    public class LABInfo
    {
        public AssetBundle _asset_bundle;
        public int _referenced_count;

        public LABInfo(AssetBundle asset_bundle)
        {
            _asset_bundle = asset_bundle;
            _referenced_count = 1;
        }

        public AssetBundle getAssetBundle()
        {
            return _asset_bundle;
        }

        public int referenced
        {
            get { return _referenced_count; }
        }

        public void addReferenced(int count = -1)
        {
            _referenced_count += count;
        }

        public void removeReferenced(int count = -1)
        {
            _referenced_count += count;
        }
    }
}


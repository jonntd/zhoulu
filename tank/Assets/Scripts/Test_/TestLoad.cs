using UnityEngine;
using Object = UnityEngine.Object;
using System.Collections;
using System.Collections.Generic;
using IAssetBundle.Load;
using IAssetBundle;


public class TestLoad : MonoBehaviour, ICommandAB
{

    public AssetBundleManifest _ab_manifest = null;
    public Dictionary<string, AssetBundleInfo> manifest_abinfos = new Dictionary<string, AssetBundleInfo>();//对AssetBundleManifest 解析得到的相关数据

    public AssetBundleCache _cache;
    public List<GameObject> ob = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        StartCoroutine(initialize());
        GameObject go = new GameObject();
        _cache = go.AddComponent<AssetBundleCache>();
        //StartCoroutine(_initLoadPcPrefab("pc001.prefab"));
    }

    public IEnumerator initialize()
    {
        yield return null;
        string path = ABPath.getAssetBundelPrePath() + "Windows";
        AssetBundleDownloadFromWebOperation ab_wwwdown = new AssetBundleDownloadFromWebOperation(path, new WWW(path));
        yield return StartCoroutine(ab_wwwdown);

        AssetForWWWOperation asset_forwwww = new AssetForWWWOperation(ab_wwwdown.assetBundle, ab_wwwdown.error);
        yield return StartCoroutine(asset_forwwww);
        Object[] objs = asset_forwwww.LoadAllAsset();
        _ab_manifest = objs[0] as AssetBundleManifest;
        string[] ab_all = _ab_manifest.GetAllAssetBundles();
        for (int i = 0; i < ab_all.Length; i++)
        {
            string ab_name = ab_all[i];
            string[] ab_dep = _ab_manifest.GetAllDependencies(ab_name);
            Hash128 hashcode = _ab_manifest.GetAssetBundleHash(ab_name);
            AssetBundleInfo ab_info = new AssetBundleInfo(ab_name, ab_dep, hashcode);
            manifest_abinfos.Add(ab_name, ab_info);
        }

        _cache._manifest_abinfos = manifest_abinfos;

        yield return new WaitForSeconds(3f);

        for (int i = 0; i<10;i++ )
        {
             string p001 = path_weapon_prefab + "weapon001.prefab";
            _cache.onGetOutAB(p001, this);
            yield return new WaitForSeconds(1f);
        }

       

    }

    public void excute(Object ab,string path)
    {
        if (ab != null)
        {
            GameObject new_ob = Instantiate(ab) as GameObject;
            new_ob.name = "new ob";
            AssetBundleRef abf=new_ob.AddComponent<AssetBundleRef>();
            abf.path = path;
            ob.Add(new_ob);
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(100, 0, 100, 100), "Destroy"))
        {
            for (int i = 0; i < ob.Count; i++)
            {
                Destroy(ob[i]);
            }
        }

        if (AssetBundleCache._ == null)
            return;

        GUI.Label(new Rect(100, 150, 400, 100), "颗粒个数："+AssetBundleCache._._ab_grains.Count);

        GUI.Label(new Rect(100, 200, 400, 100), "颗粒个数：" + AssetBundleCache._._ab_grains.Count);

        GUI.Label(new Rect(100, 250, 400, 100), "www个数：" + AssetBundleCache._._load_www.Count);

        GUI.Label(new Rect(100, 300, 400, 100), "AssetLoad个数：" + AssetBundleCache._._load_asset.Count);

        GUI.Label(new Rect(100, 350, 400, 100), "引用：：" + AssetBundleCache._.refCount);
    }

    public static string path_pc_prefab = "assets/art/resources/character/pc/";
    public static string path_weapon_prefab = "assets/art/resources/character/weapon/";

}

public class AssetBundleCache : MonoBehaviour
{
    public Dictionary<string, AssetBundleGrain> _ab_grains = new Dictionary<string, AssetBundleGrain>();
    public Dictionary<string, AssetBundleInfo> _manifest_abinfos = new Dictionary<string, AssetBundleInfo>();//对AssetBundleManifest 解析得到的相关数据
    //public delegate void OnLoadSuccess(Object[] objs, string path_asset);
    //public event OnLoadSuccess onLoadSuccess;
    public Dictionary<string, AssetBundleDownloadFromWebOperation> _load_www = new Dictionary<string, AssetBundleDownloadFromWebOperation>();
    public Dictionary<string, AssetForWWWOperation> _load_asset = new Dictionary<string, AssetForWWWOperation>();

    public static AssetBundleCache _ = null;

    public int refCount = 0;

    #region override

    void Awake()
    {
        _ = this;
    }

    void Update()
    {
        refCount = 0;
        foreach (var item in _ab_grains)
        {
            refCount+=item.Value.getRef();
        }
    }

    #endregion

    #region

    public IEnumerator _onGetOutAB(string path, ICommandAB cmd)
    {

        AssetBundleInfo ab_info = getAssetBundleInfo(path);
        if (ab_info == null) yield break;//不在主资源列表中

        bool is_full = _loadInternal(ab_info, cmd);
        if (is_full) yield break;

        //资源不完整
        string[] paths = ab_info.getDeps();
        string path_pre = ABPath.getAssetBundelPrePath();
        int length = paths.Length;
        for (int i = 0; i < length; i++)
        {
            string path_asset = paths[i];
            if (_ab_grains.ContainsKey(path_asset))//已经在缓存中
                continue;

            string load_path = path_pre + path_asset;
            if (_load_asset.ContainsKey(path_asset) || _load_www.ContainsKey(path_asset))//已经在www加载或者AssetBundle load中
                yield break;

            AssetBundleDownloadFromWebOperation ab_wwwdown = new AssetBundleDownloadFromWebOperation(path_asset, new WWW(load_path)); ;
            _load_www.Add(path_asset, ab_wwwdown);
            yield return StartCoroutine(ab_wwwdown);

            AssetForWWWOperation asset_forwwww = new AssetForWWWOperation(ab_wwwdown.assetBundle, ab_wwwdown.error);
            _load_www.Remove(path_asset);
            
            _load_asset.Add(path_asset, asset_forwwww);
            yield return StartCoroutine(asset_forwwww);
            _load_asset.Remove(path_asset);
            Object[] objs = asset_forwwww.LoadAllAsset();
            asset_forwwww.onDestroy();

            if (objs != null && objs.Length > 0)
                _ab_grains.Add(path_asset, new AssetBundleGrain(path_asset, objs));
            else
                My.LogError("加载依赖资源出错,路径：" + path_asset);
        }

        is_full = _loadInternal(ab_info, cmd);
        My.assert(is_full, "加载主资源失败,路径:" + path);
    }

    #endregion

    #region private

    public AssetBundleInfo getAssetBundleInfo(string path)
    {
        if (_manifest_abinfos.ContainsKey(path)) return _manifest_abinfos[path];
        My.assert(false, "manifset找不到这个资源:" + path);
        return null;
    }

    public bool isCheckFull(AssetBundleInfo info)
    {
        string[] paths = info.getDeps();
        int length = paths.Length;
        for (int i = 0; i < length; i++)
        {
            if (!_ab_grains.ContainsKey(paths[i])) return false;
        }

        return true;
    }

    public bool _loadInternal(AssetBundleInfo info, ICommandAB cmd)
    {
        if (!isCheckFull(info)) return false;
        Object ob = null;
        string[] paths = info.getDeps();
        string main_ab_name = info.getABLoadPath();
        int length = paths.Length;
        for (int i = 0; i < length; i++)
        {
            string path = paths[i];
            _ab_grains[path].AddRef();//添加引用
            if (path == main_ab_name && ob == null)
            {
                ob = _ab_grains[path].getMainAsset<Object>();
            }
        }
        if (cmd!=null)
            cmd.excute(ob, main_ab_name);
        return true;

    }


    #endregion

    #region public

    public void onGetOutAB(string path, ICommandAB cmd)
    {
        StartCoroutine(_onGetOutAB(path, cmd));
    }

    public void onTakeInAB(string path)
    {
        AssetBundleInfo ab_info = getAssetBundleInfo(path);
        if (ab_info == null) return;//不在主资源列表中

        string[] paths = ab_info.getDeps();
        int length = paths.Length;
        for (int i = 0; i < length; i++)
        {
            if (_ab_grains.ContainsKey(paths[i]))
                _ab_grains[paths[i]].removeRef();
        }
    }

    public bool isHaveAB(string path)
    {
        if (_ab_grains.ContainsKey(path))
            return true;
        return false;
    }

    #endregion

}

public interface ICommandAB
{
    void excute(Object ab, string path);
}



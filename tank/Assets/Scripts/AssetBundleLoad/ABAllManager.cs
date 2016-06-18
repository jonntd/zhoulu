using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IAssetBundle;

public class ABAllManager : MonoBehaviour
{


    public AssetBundleManifest _assetBundleManifestObject = null;
    public Dictionary<string, string[]> _dependencies = new Dictionary<string,string[]>();

    // Use this for initialization
    void Start()
    {
        initialize();
    }

    public void initialize()//加载manifest文件，全局只有一个文件
    {
        //string path = ABPlatform.GetPrefixPath() + "/";
        string path = ABPath.GetPrefixPath() + "Windows";
        string path1 = ABPath.GetPrefixPath() + "assets/Art/Resources/character/weapon/weapon002.unity3d".ToLower();//;
        StartCoroutine(LoadManifestAssets(path));
        StartCoroutine(LoadAssets(path1));
    }

    IEnumerator LoadManifestAssets(string path)
    {

        WWW www = new WWW(path);

        yield return www;

        if (www.error == null)
        {
            Object[] obs = www.assetBundle.LoadAllAssets();
            _assetBundleManifestObject = obs[0] as AssetBundleManifest;

            LoadDependencies();
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    IEnumerator LoadAssets(string path)
    {
        while(_assetBundleManifestObject==null)
        {
            yield return new WaitForSeconds(0.1f);
        }
        WWW www = new WWW(path);

        yield return www;

        if (www.error == null)
        {
            Object[] obs = www.assetBundle.LoadAllAssets();


            GameObject ab=www.assetBundle.LoadAsset("weapon002") as GameObject;
            Instantiate(ab);
            ab.name = "new obj";

        }
        else
        {
            Debug.Log(www.error);
        }
    }

    protected void LoadDependencies()
    {
        string[] allAssetBundle= _assetBundleManifestObject.GetAllAssetBundles();
        int length = allAssetBundle.Length;

        for (int i = 0; i < length; i++)
        {
            string asset_name = allAssetBundle[i];
            string[] dependencies = _assetBundleManifestObject.GetAllDependencies(asset_name);
            if (dependencies.Length == 0)
            {
                _dependencies[asset_name]=new string[1];
                 continue;
            }
               
            if (_dependencies.ContainsKey(asset_name))
                _dependencies[asset_name] = dependencies;
            else
                _dependencies.Add(asset_name, dependencies);
        }

        /*
        foreach(var data in _dependencies)
        {
            string[] deps = data.Value;
            Debug.Log("data.key:" + data.Key);
            for (int i = 0; i < deps.Length; i++)
            {
                Debug.Log("             data.value:" + deps[i]);
            }
        }*/
        
    }
}

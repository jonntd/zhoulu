using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IAssetBundle;
using GameEngine.Tools;
public class ABAllManager : MonoBehaviour
{
    public DependenciesCache _dependece = new DependenciesCache();
       // Use this for initialization
    void Start()
    {
        initialize();
    }

    public void initialize()
    {
        string path = ABPath.getAssetBundelPrePath() + "assetbundleconfig";
        string name = "weapon001.prefab";
        StartCoroutine(LoadManifestAssets(path));
        StartCoroutine(LoadAssets(name));
    }

    IEnumerator LoadManifestAssets(string path)
    {

        WWW www = new WWW(path);

        yield return www;

        if (www.error == null)
        {
            AssetBundle ab = www.assetBundle;
            Object[] obs = ab.LoadAllAssets();

            TextAsset text = obs[0] as TextAsset;
            _dependece.resetDependencies(text);
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    IEnumerator LoadAssets(string ab_name)
    {
        yield return null;
        while (_dependece.getDependenciesCount()==0)
        {
            yield return new WaitForSeconds(0.1f);
        }

        AssetBundelDependence ab_dep = _dependece.getAssetBundelDependence(ab_name);
        List<string> path = ab_dep.getDependence();
        for (int i = path.Count-1; i >=0; i--)
        {
             WWW www = new WWW(path[i]);
             yield return www;

             if (www.error == null)
             {
                 AssetBundle ab = www.assetBundle;
                 string[] ab_list=ab.GetAllAssetNames();
                 Object[] obs = www.assetBundle.LoadAllAssets();
                 if(i==0)
                 {
                     GameObject go = obs[0] as GameObject;
                     GameObject newObj=Instantiate(go);
                     newObj.name = "new obj";
                 }
             }
             else
             {
                 Debug.Log(www.error);
             }
        }
    }
}

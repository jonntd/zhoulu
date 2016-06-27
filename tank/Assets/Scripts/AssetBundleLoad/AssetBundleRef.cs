using UnityEngine;
using System.Collections;

public class AssetBundleRef : MonoBehaviour {

    public string path = "";

    void OnDestroy()
    {
        AssetBundleCache._.onTakeInAB(path);
    }
}

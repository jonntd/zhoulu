using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class More : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void MoreGame()
    {
        #if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/dev?id=6793500223563275059");
        #elif UNITY_IPHONE
        Application.OpenURL("https://itunes.apple.com/developer/xiaojie-wang/id1040525394");
        #endif  
    }

    // Update is called once per frame
    void Update () {
		
	}
}

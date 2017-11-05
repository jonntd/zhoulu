using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSetting : MonoBehaviour
{
	// Use this for initialization
	void Start () {
    }


    public void on_follow()
    {
        Application.OpenURL("https://www.facebook.com/codefgame");
    }

    public void on_more()
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
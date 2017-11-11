using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSetting : MonoBehaviour
{
    public Button follow;
    public Button more;
	// Use this for initialization
	void Start () {
        follow.onClick.AddListener(on_follow);
        more.onClick.AddListener(on_more);

    }


    public void on_follow()
    {
        Application.OpenURL(ConfigInfo.Instance.GetValue("Follow"));
    }

    public void on_more()
    {
        Application.OpenURL(ConfigInfo.Instance.GetValue("More"));
    }
    // Update is called once per frame
    void Update () {
		
	}
}

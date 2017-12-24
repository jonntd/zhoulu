using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour {

    void OnEnable()
    {
        ShowStarsTutorial();
    }

    void ShowStarsTutorial()
    {
            GameObject.Find("CanvasGlobal").transform.Find("Tutorial").gameObject.SetActive(true);
            Tutorial.Instance.GetTDNum(LevelManager.THIS.currentLevel);
    }

}

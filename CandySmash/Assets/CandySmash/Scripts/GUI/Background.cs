using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public Sprite[] pictures;

    // Use this for initialization
    void OnEnable()
    {
        if (LevelManager.THIS != null)
        {
            int index = (int)((float)LevelManager.Instance.currentLevel / 20f - 0.01f);
            index = index % pictures.Length;
            GetComponent<Image>().sprite = pictures[index];
        }



    }


}

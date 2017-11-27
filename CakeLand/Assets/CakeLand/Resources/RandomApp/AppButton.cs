using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Umeng;

public class AppButton : MonoBehaviour, IPointerClickHandler
{
    public RawImage img;
    public Text text;
    private AppInfo _info;
    private const string MORE_GAMES = "MoreGames";
    // Use this for initialization
    void Start()
    {

    }

    public void SetInfo(AppInfo info)
    {
        _info = info;
        text.text = info.name;
        //Texture texture = Resources.Load(info.icon + ".png") as Texture;
        Object obj = Resources.Load(info.icon);
        obj = Resources.Load(info.icon, typeof(Texture));
        Texture texture = obj as Texture;
        img.texture = texture;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        #if UNITY_ANDROID
                Application.OpenURL(_info.aaddres);
        #elif UNITY_IPHONE
                Application.OpenURL(_info.iaddres);
        #endif
                //此处增加点击评论游戏按钮
                GA.Event(MORE_GAMES, _info.name);
            }
}

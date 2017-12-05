using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 推送文本Icon
/// 1.加载Resources文件夹下面的AppsUI文件
/// 2.当游戏失败的时候随机显示一个Icon
/// </summary>
public class RandomAppsManager : MonoBehaviour
{
    public static RandomAppsManager Instance;
    [SerializeField]
    private GameObject prefab;                      //随机显示的AppIcon的预设
    [SerializeField]
    private Transform holder;                       //所有的AppIcon都会放在这下面
    [SerializeField]
    private List<AppButton> uiList;                 //AppIcon的GameObject的集合
    [SerializeField]
    private TextAsset textAsset;                    //配置文件
    private AppParseManager appsInfo;               //解析配置文件
    void Awake()
    {
        Instance = this;
        appsInfo = new AppParseManager(textAsset);
        List<AppInfo> appList = appsInfo.GetAppList();
        for (int i = 0; i < appList.Count; i++)
        {
            CreateAppUI((appList[i]));
        }
    }

    private void CreateAppUI(AppInfo info)
    {
        GameObject appUI = GameObject.Instantiate(prefab);
        AppButton button = appUI.GetComponent<AppButton>();
        button.SetInfo(info);
        appUI.transform.SetParent(holder, false);
        appUI.SetActive(false);
        uiList.Add(button);
    }

    /// <summary>
    /// 玩家失败的时候，随机一次
    /// </summary>
    public void RandomShowCtrl()
    {

        int num = (int)Random.Range(0, 3);
        Debug.Log("num:" + num);
        if (num == 0)
            RandomAppsShow();
        else
            RandomAppsHide();
    }

    public void RandomAppsHide()
    {
        holder.gameObject.SetActive(false);
    }

    public void RandomAppsShow()
    {
        holder.gameObject.SetActive(true);
        int num = (int)Random.Range(0, uiList.Count);
        _showIcon(num);
        Debug.Log("xianshi");
    }

    private void _showIcon(int index)
    {
        for (int i = 0; i < uiList.Count; i++)
        {
            uiList[i].gameObject.SetActive(i == index);
        }

    }
}

/// <summary>
/// 推送的内容
/// 1.文本/Icon/地址
/// </summary>
public class AppInfo
{
    public string id;
    public string name;
    public string icon;
    public string iaddres;
    public string aaddres;
    /// <summary>
    /// 默认如果推送的地址为空。那么这个推送就是无效的，不进行随机
    /// </summary>
    /// <returns></returns>
    public bool IsValid()
    {
#if UNITY_ANDROID
        if (string.IsNullOrEmpty(aaddres)) return false;
        return true;
#elif UNITY_IPHONE
        if (string.IsNullOrEmpty(iaddres)) return false;
        return true;
#endif
        return false;
    }
}

/// <summary>
/// 解析文本
/// </summary>
public class AppParseManager
{
    private readonly List<AppInfo> appList = new List<AppInfo>();
    public AppParseManager(TextAsset textAsset)
    {
        init(textAsset);
    }

    private void init(TextAsset ta)
    {
        appList.Clear();
        if (ta == null) return;
        //这里解析有问题，由于我是手写的所以会导致分割符号是\r\n 所以只能采取这种写法
        char[] splits = new char[] { '\r', '\n', };
        string[] info = ta.text.Split(splits);

        for (int i = 1; i < info.Length; i++)
        {
            AppInfo app = ParseContent((info[i]));
            if (app == null || !app.IsValid()) continue;
            appList.Add(app);
        }
    }

    private AppInfo ParseContent(string content)
    {
        if (string.IsNullOrEmpty(content) || content.Length == 0) return null;
        AppInfo app = new AppInfo();

        string[] infos = content.Split('\t');
        app.id = infos[0];
        app.name = infos[1];
        app.icon = infos[2];
        app.aaddres = infos[3];
        app.iaddres = infos[4];
        return app;
    }

    public List<AppInfo> GetAppList() { return appList; }
}



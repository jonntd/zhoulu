using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制教程图文显示
/// 1.加载Tutorial文件夹下面的TutorialDetail文件
/// 2.当载入游戏时，根据关卡显示对应教程
/// </summary>
public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance;

    [SerializeField]
    private GameObject prefab;                      //Tutorial的预设prefab模板
    [SerializeField]
    private Transform holder;                       //所有的Tutorial都会放在这下面
    [SerializeField]
    private List<TutorialDetail> uiList;            //Tutorial的GameObject的集合
    [SerializeField]
    private TextAsset TutorialConfig;               //配置文件
    private TDManager TDInfo;                       //解析完配置文件信息

    private int TDNum;                              //教程ID=index
    public GameObject Btns;                         //切换按钮组，在关卡中需要隐藏

    void Awake()
    {
        Instance = this;

        TDInfo = new TDManager(TutorialConfig);
        List<TDInfo> TDList = TDInfo.GetTDList();
        for (int i = 0; i < TDList.Count; i++)
        {
            CreateTDUI((TDList[i]));
        }

    }

    void OnEnable()
    {
		TDNum = 0;
        TutorialShowCtrl(TDNum);
    }

    /// <summary>
    /// 根据当前获取到的关卡ID显示对应教程(已根据关卡名命名list中的prefab),当前关卡没有教程则不显示
    /// </summary>
    public void GetTDNum(int level)
    {

        for (int i = 0; i < uiList.Count; i++)
        {
            uiList[i].gameObject.SetActive(false);
        }

        int num = PlayerPrefs.GetInt("TutorialLevelDetailShowed",0);

        if (GameObject.Find("TutorialDetail").transform.Find(level.ToString()) != null && num < level)
        {
            GameObject.Find("TutorialDetail").transform.Find(level.ToString()).gameObject.SetActive(true);
            PlayerPrefs.SetInt("TutorialLevelDetailShowed", level);
            PlayerPrefs.Save();
        }
        else
        {
            GameObject.Find("CanvasGlobal").transform.Find("Tutorial").gameObject.SetActive(false);
        }
        Btns.gameObject.SetActive(false);
    }

    /// <summary>
    /// 直接打开帮助文件界面时,可以切换教程内容
    /// </summary>
    public void TutorialNumPlus()
    {
        if (TDNum < uiList.Count-1)
        {
            TDNum++;
        }
        else
        {
            TDNum = 0;
        }
        TutorialShowCtrl(TDNum);
    }

    public void TutorialNumMinus()
    {
        if (TDNum ==0)
        {
            TDNum = uiList.Count - 1; 
        }
        else
        {
            TDNum--;
        }
        TutorialShowCtrl(TDNum);
    }

    /// <summary>
    /// 生成换教程内容
    /// </summary>
    private void CreateTDUI(TDInfo info)
    {
        GameObject TDUI = GameObject.Instantiate(prefab);
        TutorialDetail button = TDUI.GetComponent<TutorialDetail>();
        button.SetInfo(info);
        //用关卡ID作为控件名称
        button.name = info.level;
        TDUI.transform.SetParent(holder, false);
        TDUI.SetActive(false);
        uiList.Add(button);
    }

    /// <summary>
    /// 根据当前ID显示对应教程
    /// </summary>
    private void TutorialShowCtrl(int index)
    {
        for (int i = 0; i < uiList.Count; i++)
        {
            uiList[i].gameObject.SetActive(i == index);
        }
        Btns.gameObject.SetActive(true);
    }
}

/// <summary>
/// 推送的内容
/// 1.关卡ID/图片ID/文本内容
/// </summary>
public class TDInfo
{
    public string id;
    public string level;
    public string image;
    public string text;
}
    /// <summary>
    /// 解析文本
    /// </summary>
public class TDManager
{
        private readonly List<TDInfo> TDList = new List<TDInfo>();
        public TDManager(TextAsset textAsset)
        {
            init(textAsset);
        }

        private void init(TextAsset config)
        {
            TDList.Clear();
            if (config == null) return;
            //这里解析有问题，由于我是手写的所以会导致分割符号是\r\n 所以只能采取这种写法
            char[] splits = new char[] { '\r', '\n', };
            string[] info = config.text.Split(splits);

            for (int i = 1; i < info.Length; i++)
            {
                TDInfo TD = ParseContent((info[i]));
                if (TD == null) continue;
                TDList.Add(TD);
            }
        }

        private TDInfo ParseContent(string content)
        {
            if (string.IsNullOrEmpty(content) || content.Length == 0) return null;
            TDInfo TD = new TDInfo();

            string[] infos = content.Split('\t');
            TD.id = infos[0];
            TD.level = infos[1];
            TD.image = infos[2];
            TD.text = infos[3];
            return TD;
        }

        public List<TDInfo> GetTDList() { return TDList; }
}
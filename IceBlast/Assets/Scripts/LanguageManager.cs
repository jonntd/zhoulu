using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    //存放在Resource下的路劲
    public string path = "";
    [HideInInspector]
    public LanguageInfo _curr_lang;//当前语言集
    public static LanguageManager _;
    //当前的语言
    public List<LanguageInfo> _lan_maps=new List<LanguageInfo>();
	// Use this for initialization
	void Start ()
	{
	    _ = this;
        _load_text();
	    _find_default_lan();
        Test();
	}

    public void Test()
    {
        Debug.Log(GetTxtByKey("key1"));
        Debug.Log(GetTxtByKey("key2"));
    }

    public void _load_text()
    {
        TextAsset text=Resources.Load<TextAsset>(path);
        //1.加载文本内容
        byte[] bytes =text.bytes;
        string contents = Encoding.UTF8.GetString(bytes);
        string[] lines=contents.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            //2.解析内容
            string[] info = lines[i].Split('\t');
            //3.填充内容
            _add_key_value(info);
        }
    }

    public void _find_default_lan()
    {
        //查找当前语言
        string lang = Application.systemLanguage.ToString();
        for (int i = 0; i < _lan_maps.Count; i++)
        {
            if (_lan_maps[i].ToString() == lang)
            {
                _curr_lang = _lan_maps[i];
                break;
            }
        }

        //如果找不到默认语言，就默认用第一种作为默认语言
        if (_curr_lang == null && _lan_maps.Count > 0)
            _curr_lang = _lan_maps[0];

    }

    public void _add_key_value(string[] infos)
    {
        string key = infos[0];
        for (int i = 0; i < _lan_maps.Count; i++)
        {
            if (i> infos.Length) continue;
            _lan_maps[i].AddTxtByKey(key,infos[i+1]);
        }
    }

    /// <summary>
    /// 根据Key 得到对应的文本
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetTxtByKey(string key)
    {
        if (_curr_lang == null) return string.Empty;
        return _curr_lang.GetTxtByKey(key);
    }

}


[System.Serializable]
public class LanguageInfo {
    //当前语言
    public SystemLanguage language;
    //key 和文本
    public Dictionary<string,string> txt_map=new Dictionary<string, string>();

    public string GetTxtByKey(string key)
    {
        if (txt_map.ContainsKey(key))
            return txt_map[key];
        Debug.LogError("can not find txt. this key:"+key);
        return string.Empty;
    }

    public void AddTxtByKey(string key,string value)
    {
        Debug.Assert(!txt_map.ContainsKey(key),"# same key# key is"+key+"    value:"+value);
        txt_map.Add(key,value);
    }
}


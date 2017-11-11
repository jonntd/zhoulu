using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigInfo : MonoBehaviour
{
    public Dictionary<string, string> dic = new Dictionary<string, string>();
    public static ConfigInfo Instance;
    private void Awake()
    {
        Instance = this;
        dic.Clear();
        TextAsset ta = Resources.Load("config") as TextAsset;
        string content = ta.text;
        string[] plit = new string[] { "\r\n" };
        string[] infos = content.Split(plit, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < infos.Length; i++)
        {
            string[] info = infos[i].Split('=');
            dic.Add(info[0], info[1]);
        }
    }

    public string GetValue(string key)
    {
        if (dic.ContainsKey(key))
            return dic[key];
        return string.Empty;
    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

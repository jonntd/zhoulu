
using UnityEngine;
using System.Collections;

using Mono.Xml;
using System.IO;
using System.Security;

public class LoadXml : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

       /* SecurityParser SP = new SecurityParser();
        string xml_path = "test";
        TextAsset ta = Resources.Load<TextAsset>(xml_path);
        string content = ta.ToString();
        SP.LoadXml(content);

        SecurityElement SE = SP.ToXml();

        Hashtable ht = SE.Attributes;
        foreach (SecurityElement child in SE.Children)
        {
            Debug.Log(child.Tag);
            if (child.Tag == "properties")
            {
                //orientation="orthogonal" width="10" height="10" tilewidth="78" tileheight="78"
                string version = child.Attribute("version");
                string orthogonal = child.Attribute("orthogonal");
                string width = child.Attribute("width");
                string height = child.Attribute("height");
                string tilewidth = child.Attribute("tilewidth");
                string tileheight = child.Attribute("tileheight");
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}

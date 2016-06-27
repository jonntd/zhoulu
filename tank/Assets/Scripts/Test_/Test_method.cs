using UnityEngine;
using System.Collections;
using System;
using System.Reflection;


public class Test_method : MonoBehaviour {

	// Use this for initialization
	void Start () {
        /*string context = "Test_method";
        Type t = context.GetType();
        BindingFlags f = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
        MethodInfo mi = t.GetMethod("test1");
        string notification = "";
        mi.Invoke(context, new object[] { notification });*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void test1(string s)
    {
        Debug.Log("Success");
    }
}

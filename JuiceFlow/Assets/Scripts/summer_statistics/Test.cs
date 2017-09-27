using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    void Awake()
    {
        Debug.Log("----------------Awake-------------------");
    }

	// Use this for initialization
	void Start () {
        Debug.Log("----------------Start-------------------");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable()
    {
        Debug.Log("----------------OnEnable-------------------");
    }

    void OnDisable()
    {
        Debug.Log("----------------OnDisable-------------------");
    }
}

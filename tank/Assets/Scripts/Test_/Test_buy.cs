using UnityEngine;
using System.Collections;

public class Test_buy : MonoBehaviour {

    void Awake()
    {
        Debug.Log(1);
        GameObject ob=Resources.Load("Cube") as GameObject;
        Debug.Log(2);
        Instantiate(ob);
        Debug.Log(3);
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

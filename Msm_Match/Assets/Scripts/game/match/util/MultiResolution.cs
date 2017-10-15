using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiResolution : MonoBehaviour
{

    public Camera camera;
	// Use this for initialization
	void Start () {
		Debug.Log(camera.orthographic+"_"+ camera.orthographicSize);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

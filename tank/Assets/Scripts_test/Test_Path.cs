using UnityEngine;
using System.Collections;

public class Test_Path : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Debug.Log("dataPath:"+Application.dataPath);
        Debug.Log("streamingAssetsPath:" + Application.streamingAssetsPath);
        Debug.Log("persistentDataPath :" + Application.persistentDataPath);
        Debug.Log("temporaryCachePath :"+ Application.temporaryCachePath );
	}
	
}

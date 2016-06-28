using UnityEngine;
using System.Collections;

public class TestDirection : MonoBehaviour {


    public Transform a;
    public Transform b;
    public Vector3 dir = new Vector3();
    public Vector3 leng = new Vector3(10, 10, 10);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion q=a.transform.rotation;
        Vector3 s=q.eulerAngles + dir;


        float length = Vector3.Distance(Vector3.zero,leng);
        Debug.DrawLine(Vector3.zero, s * length, Color.green);
	}
}

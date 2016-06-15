using UnityEngine;
using System.Collections;

public class TestFSM : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ChildFSM fsm = new ChildFSM();
        Debug.LogError("Current State = " + fsm.currentState);
        Debug.LogError("Command.Begin: Current State = " + fsm.MoveNext(PlayerState.Run));
        Debug.LogError("Invalid transition: " + fsm.CanReachNext(PlayerState.Idle));
        Debug.LogError("Previous State = " + fsm.previusState);
        
	}
	

  
	
}

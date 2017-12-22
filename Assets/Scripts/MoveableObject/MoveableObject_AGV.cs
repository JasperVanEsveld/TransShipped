using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject_AGV : MoveableObject
{
  
    // Use this for initialization
    void Start () {
        // You may want to write sth before this to determine the exact parameters to pass to Init();
        MOInit(new Vector3(0.0f, 0.0f, 0.0f), 20.0f, false, new Vector3(3.0f, 3.0f, 3.0f));
    }
	
	// Update is called once per frame
	void Update () {
        MOMovementUpdate();
    }
}

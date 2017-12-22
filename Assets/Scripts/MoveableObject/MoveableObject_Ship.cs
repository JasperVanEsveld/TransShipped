using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject_Ship : MoveableObject
{
    

    // Use this for initialization
    void Start () {
        // You may want to write sth before this to determine the exact parameters to pass to Init();
        MOInit(new Vector3(65.0f, 0.0f, 35.0f), 20.0f, true, new Vector3(20.0f, 2.0f, 2.0f));

    }
	
	// Update is called once per frame
	void Update () {
        MOMovementUpdate();

    }
}

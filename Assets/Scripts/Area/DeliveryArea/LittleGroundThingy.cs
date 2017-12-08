using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGroundThingy : MonoBehaviour {

    // Used to give names only
    private static int idCount = 0;
    private static Vector3 cranePos = new Vector3(15.28f, 0.0f, 10.9f);
    private static Vector3 stackPos = new Vector3(-5.4f, 0.0f, -31.67f);
    private Vector3 targetPosA;
    private Vector3 targetPosB;

    private Queue<Vector3> movementQueue;

    // Ref to the gameobject
    private GameObject localHandle;

    void Start ()
    {
		
	}
	
	void Update ()
    {
		if(movementQueue.Count!=0) // If movement Queue is not empty
        {
            
        }
	}

    public void GenerateNewInstance()
    {
        localHandle = GameObject.CreatePrimitive(PrimitiveType.Cube);
        localHandle.name = "AGV_" + idCount++;

        // Spawn Pos = Pos of the crane.xz, y = 0.0f
        localHandle.transform.position = cranePos;

        // Init movementQueue
        movementQueue = new Queue<Vector3>();

        // TODO Set Size, probably.

    }
}

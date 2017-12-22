using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour {


    public Transform lightOrigin;
    public float rotationSpeed;
    public float xAngle;
    public float yAngle;
    public float zAngle;
    public Game game;


    // Use this for initialization
    void Start () {
        game = FindObjectOfType<Game>();
        transform.rotation = Quaternion.Euler(xAngle, yAngle, zAngle);
    }
	
	// Update is called once per frame

	void Update () {
        if ((game.currentState is OperationState))
        {
            transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
        }
            

        //transform.RotateAround(lightOrigin.position, Vector3.right, Time.deltaTime * rotationSpeed);
    }
}

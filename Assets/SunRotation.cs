using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour {


    public float rotationSpeed;
    public float xAngle;
    public float yAngle;
    public float zAngle;
    private Game game;


    void Start () {
        game = FindObjectOfType<Game>();
        transform.rotation = Quaternion.Euler(xAngle, yAngle, zAngle);
    }
	
	void Update () {
        if ((game.currentState is OperationState))
        {
            transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
        }
    }
}

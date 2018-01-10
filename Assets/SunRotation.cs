using UnityEngine;

public class SunRotation : MonoBehaviour {


    public float rotationSpeed;
    public float xAngle;
    public float yAngle;
    public float zAngle;


    void Start () {
        transform.rotation = Quaternion.Euler(xAngle, yAngle, zAngle);
    }
	
	void Update () {
        if (Game.instance.currentState is OperationState)
        {
            transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
        }
    }
}

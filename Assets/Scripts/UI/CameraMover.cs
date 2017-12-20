using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {
    public float movementSpeed =1;
    public float rotationSpeed =1;
    public float xmin,xmax,zmin,zmax;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        float rotate = Input.GetAxis("Mouse X") * rotationSpeed;



       // transform.RotateAround(cameraOrigin.transform.position, Vector3.up, -Input.GetAxis("Horizontal") * rotationSpeed);

        if (Input.GetButton("Fire2"))
        {
            transform.Rotate(Vector3.up * rotate * Time.deltaTime, Space.World);
            //transform.RotateAround(cameraOrigin.transform.position, Vector3.up, Input.GetAxis("Mouse X") * rotationSpeed2);
        }

        transform.Translate(Vector3.forward* horizontal);
        transform.Translate(Vector3.right * -vertical );


        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xmin, xmax), 0, Mathf.Clamp(transform.position.z, zmin, zmax));
       

    }
}

using UnityEngine;

public class CameraRotate : MonoBehaviour{
    public GameObject cameraOrigin;
    public float rotationSpeed = 1;
    public float rotationSpeed2 = 1;

    public void Update(){
        //translation = Input.GetAxis("Vertical") * translationspeed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        transform.LookAt(cameraOrigin.transform.position);
        transform.RotateAround(cameraOrigin.transform.position, Vector3.up, -Input.GetAxis("Horizontal") * rotationSpeed);
        
        if (Input.GetButton("Fire1"))
        {
            transform.RotateAround(cameraOrigin.transform.position, Vector3.up, Input.GetAxis("Mouse X") * rotationSpeed2);
        }
    }
}
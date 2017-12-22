using UnityEngine;

public class CameraRotate : MonoBehaviour{
    public GameObject cameraOrigin;
    float zoom;
    public float zoomMin;
    public float zoomMax;
    public float zoomSensitivity;

    private void Start()
    {
        zoom = GetComponent<Camera>().fieldOfView;
    }

    public void Update(){
        //translation = Input.GetAxis("Vertical") * translationspeed;

        transform.LookAt(cameraOrigin.transform.position);

        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
        GetComponent<Camera>().fieldOfView =zoom;


    }
}
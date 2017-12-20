using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapacitySlider : MonoBehaviour {


    public Transform target;
    public Camera cam;
    public int height = 50;
    public float barValue;


    public void SetTarget(Transform targ)
    {

    target = targ.transform;
    }



    // Use this for initialization
    void Start () {
        cam = Camera.main;
        //barValue = GetComponent<Slider>().value;

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 screenPos = cam.WorldToScreenPoint(target.position);
        transform.position = screenPos + Vector3.up* height;
        GetComponent<Slider>().value = barValue;
    }


    public void ChangeSliderValue(float value)
    {
        barValue = value;
    }

}

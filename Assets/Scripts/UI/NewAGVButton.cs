using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewAGVButton : MonoBehaviour {
    
    private Road road;
    
    void Start () {
        road = FindObjectOfType<Road>();
    }

    public void AddVehicle()
    {
        GameObject v = Instantiate(GameObject.Find("Vehicle"));
        road.vehicles.Add(v.GetComponent(typeof(Vehicle)) as Vehicle);
    }
}

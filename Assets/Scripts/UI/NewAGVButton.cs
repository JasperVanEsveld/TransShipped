using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewAGVButton : MonoBehaviour {

    private Game game;
    private Road road;
    
    void Start () {
        game = FindObjectOfType<Game>();
        road = FindObjectOfType<Road>();
        OnMouseDown();
    }

    private void OnMouseDown()
    {
        GameObject v = Instantiate(GameObject.Find("Vehicle"));
        road.vehicles.Add(v.GetComponent(typeof(Vehicle)) as Vehicle);
    }
}

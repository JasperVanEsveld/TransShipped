using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void tellShipToEnter(int i)
    {
        GameObject.Find("Ship_" + i).GetComponent<Ship>().EnterTerminal();
    }
}

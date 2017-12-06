using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void testFun0(float x)
    {
        GameObject obj = GameObject.Find("Cube");
        Debug.Log(obj);
        VehicleShip test = obj.GetComponent<VehicleShip>();
        Debug.Log(test);
        test.setScale(new Vector3(x,0,0));
    }

    public void testFun1()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.AddComponent<VehicleShip>();
        cube.transform.position = new Vector3(0, 0, 0);
    }

    public void testFun2(float x)
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject_HowToUse : MonoBehaviour {



    public void ShipTestFun1()
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.AddComponent<MoveableObject_Ship>();
        obj.name = "ExampleShip";
    }

    public void ShipTestFun2()
    {
        GameObject obj = GameObject.Find("ExampleShip");
        obj.GetComponent<MoveableObject_Ship>().EnterTerminal(new Vector3(15.0f, 0.0f, 17.0f));
    }

    public void ShipTestFun3()
    {
        GameObject obj = GameObject.Find("ExampleShip");
        obj.GetComponent<MoveableObject_Ship>().EnterTerminal(new Vector3(15.0f, 0.0f, -17.0f));
    }

    public void ShipTestFun4()
    {
        GameObject obj = GameObject.Find("ExampleShip");
        obj.GetComponent<MoveableObject_Ship>().LeaveTerminal();
    }

    public void AGVTestFun1()
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.AddComponent<MoveableObject_AGV>();
        obj.name = "ExampleAGV";
    }

    public void AGVTestFun2()
    {
        GameObject obj = GameObject.Find("ExampleAGV");
        obj.GetComponent<MoveableObject_AGV>().PushNewDest(new Vector3(15.0f, 0.0f, 10.0f));
    }

    public void AGVTestFun3()
    {
        GameObject obj = GameObject.Find("ExampleAGV");
        obj.GetComponent<MoveableObject_AGV>().PushNewDest(new Vector3(-16.0f, 0.0f, 30.0f));
    }

    public void AGVTestFun4()
    {
        GameObject obj = GameObject.Find("ExampleAGV");
        obj.GetComponent<MoveableObject_AGV>().PushNewDest(new Vector3(-5.0f, 0.0f, -25.0f));
    }

    public void AGVTestFun5()
    {
        GameObject obj = GameObject.Find("ExampleAGV");
        obj.GetComponent<MoveableObject_AGV>().PushNewDest(new Vector3(-15.0f, 0.0f, 0.0f));
    }
}

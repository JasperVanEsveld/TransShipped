using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTestScript : MonoBehaviour {

    private Vector3 scale_ = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 targetPos_ = new Vector3(12.0f, -21.0f, 12.0f);
    private float speed_ = 1.0f;

    /*
    public void setScaleX(float i) { scale_.x = i; }
    public void setScaleY(float i) { scale_.y = i; }
    public void setScaleZ(float i) { scale_.z = i; }
    public void setTargetPosX(float i) { targetPos_.x = i; }
    public void setTargetPosY(float i) { targetPos_.y = i; }
    public void setTargetPosZ(float i) { targetPos_.z = i; }
    */

    public void GenShip()
    {
        GameObject ship = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ship.AddComponent<VehicleShip>();
        ship.name = "ShipPrototype";
        //ship.transform.position = new Vector3(100.0f, -1.0f, 40.0f);
        //ship.GetComponent<VehicleShip>().setScale(new Vector3(20, 4, 2));
    }

    public void ShipEnter()
    {
        GameObject.Find("ShipPrototype").GetComponent<VehicleShip>().EnterTerminal();
    }

    public void ShipLeave()
    {
        GameObject.Find("ShipPrototype").GetComponent<VehicleShip>().LeaveTerminal();
    }

    public void ShipSize(float in_f)
    {
        GameObject.Find("ShipPrototype").GetComponent<VehicleShip>().SetSize(in_f);
    }
    /*
    public void testFun2()
    {
        GameObject.Find("Cube").GetComponent<VehicleShip>().setScale(scale_);
    }

    public void testFun3(float i_dis)
    {
        GameObject.Find("Cube").GetComponent<VehicleShip>().MoveForward(i_dis);
    }

    public void testFun5(float i_dis)
    {
        GameObject.Find("Cube").GetComponent<VehicleShip>().MoveSideway(i_dis);
    }

    public void testFun4(float i_angle)
    {
        GameObject.Find("Cube").GetComponent<VehicleShip>().Rotate(i_angle);
    }
    */
}

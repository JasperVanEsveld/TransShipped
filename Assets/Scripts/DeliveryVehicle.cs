using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeliveryVehicle : MonoBehaviour
{
    private List<MonoContainer> carrying;
    private List<Container> incoming;
    private List<Container> outgoing;

    protected Queue<Vector3> movementQueue_ = new Queue<Vector3>();

    protected Vector3 destPos_ = new Vector3(17.0f, -1.0f, 17.0f);
    protected Vector3 targetPos_;
    protected Vector3 spawnPos_ = new Vector3(100.0f, -1.0f, 40.0f);
    protected Vector3 spawnScale_ = new Vector3(20, 4, 2);
    protected bool moveAxisOrder = false;


    protected Vector3 direction_ = new Vector3(1.0f, 0.0f, 0.0f);
    protected float speed_ = 20.0f;
    protected float forwardSpeed_ = 20.0f;
    protected float sidewaySpeed_ = 10.0f;


    public void EnterParkingSpot()
    {

    }

    public void LeaveParkingSpot()
    {

    }

    /*
    public void Rotate(float i_angle)
    {
        transform.Rotate(new Vector3(0.0f, i_angle, 0.0f), Space.World);
        direction_ = Quaternion.AngleAxis(i_angle, Vector3.up) * direction_;
    }
    */



    void Update()
    {
        /*
        float step = speed_ * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos_, step);
        */
    }

    private void Start()
    {

    }

    public List<Container> Incoming
    {
        get { return incoming; }
        set { incoming = value; }
    }

    public List<Container> Outgoing
    {
        get { return outgoing; }
        set { outgoing = value; }
    }

    public abstract void EnterTerminal();
    public abstract void LeaveTerminal();
    /*
    public void setScale(Vector3 in_scale)
    {
        transform.localScale = in_scale;
    }

    public void MoveForward(float i_dis)
    {
        targetPos_ += i_dis * direction_;
    }

    public void MoveSideway(float i_dis)
    {
        targetPos_ += i_dis * (Quaternion.AngleAxis(90, Vector3.up) * direction_);
    }
    */
}


//Legacy
public class Train : DeliveryVehicle
{
    public override void EnterTerminal()
    {
        throw new NotImplementedException();
    }

    public override void LeaveTerminal()
    {
        throw new NotImplementedException();
    }
}

public class Ship : DeliveryVehicle
{
    public override void EnterTerminal()
    {
        throw new NotImplementedException();
    }

    public override void LeaveTerminal()
    {
        throw new NotImplementedException();
    }
}

public class Truck : DeliveryVehicle
{
    public override void EnterTerminal()
    {
        throw new NotImplementedException();
    }

    public override void LeaveTerminal()
    {
        throw new NotImplementedException();
    }
}
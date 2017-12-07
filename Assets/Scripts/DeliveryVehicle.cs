using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeliveryVehicle : MonoBehaviour
{
    public List<MonoContainer> carrying;
    //public List<Container> incoming;
    public List<Container> outgoing;

    protected Queue<Vector3> movementQueue_ = new Queue<Vector3>();

    protected Vector3 destPos_ = new Vector3(17.0f, -1.0f, 17.0f);
    protected Vector3 targetPos_ = new Vector3();
    protected Vector3 spawnPos_ = new Vector3(100.0f, -1.0f, 40.0f);
    protected Vector3 spawnScale_ = new Vector3(20, 4, 2);
    protected Vector3 interPos_ = new Vector3();
    protected bool moveAxisOrder = false;

    protected float height_ = 0.0f;

    protected Vector3 direction_ = new Vector3(1.0f, 0.0f, 0.0f);
    protected float speed_ = 20.0f;
    protected float forwardSpeed_ = 20.0f;
    protected float sidewaySpeed_ = 10.0f;


    void Update()
    {

    }

    private void Start()
    {

    }

    public List<Container> Outgoing
    {
        get { return outgoing; }
        set { outgoing = value; }
    }

    protected Vector3 getNextPos()
    {

        float step = speed_ * Time.deltaTime;
        Vector3 tempTarget = movementQueue_.Peek();
        return Vector3.MoveTowards(transform.position, tempTarget, step);
    }

    public void EnterTerminal()
    {
        interPos_.x = destPos_.x;
        interPos_.y = height_;
        interPos_.z = spawnPos_.z;

        movementQueue_.Enqueue(interPos_);
        movementQueue_.Enqueue(destPos_);

    }

    protected bool isAtDest()
    {
        if (Vector3.Distance(destPos_, transform.position) < speed_ * Time.deltaTime)
            return true;
        else
            return false;
    }

    public void LeaveTerminal()
    {
        interPos_.x = destPos_.x;
        interPos_.y = height_;
        interPos_.z = spawnPos_.z;

        movementQueue_.Enqueue(interPos_);
        movementQueue_.Enqueue(spawnPos_);
    }

}


//Legacy
public class Train : DeliveryVehicle
{

}

public class Ship : DeliveryVehicle
{

}

public class Truck : DeliveryVehicle
{
 
}
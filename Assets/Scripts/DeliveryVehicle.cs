using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeliveryVehicle : MonoBehaviour
{
    private List<MonoContainer> carrying;
    private List<Container> incoming;
    private List<Container> outgoing;

    protected Vector3 targetPos_ = new Vector3(0.0f, 0.0f, 0.0f);
    protected float speed_ = 1.0f;

    protected Vector3 direction_ = new Vector3(1.0f, 0.0f, 0.0f);
    protected float forwardSpeed_ = 0.5f;
    protected float sidewaySpeed_ = 0.2f;

    public void Rotate(float i_angle)
    {
        transform.Rotate(new Vector3(0.0f, i_angle, 0.0f), Space.World);
    }

    public void MoveForward(float i_dis)
    {
        targetPos_ += i_dis * direction_;
    }

    void Update()
    {
        float step = speed_ * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos_, step);
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

    protected abstract void EnterTerminal();
    protected abstract void LeaveTerminal();

    public void setScale(Vector3 in_scale)
    {
        transform.localScale = in_scale;
    }

    public void modifyPos(Vector3 in_pos)
    {
        transform.position += in_pos;
    }

    public void moveToPos(Vector3 in_pos)
    {
        targetPos_ = in_pos;
    }
}


//Legacy
public class Train : DeliveryVehicle
{
    protected override void EnterTerminal()
    {
        throw new NotImplementedException();
    }

    protected override void LeaveTerminal()
    {
        throw new NotImplementedException();
    }
}

public class Ship : DeliveryVehicle
{
    protected override void EnterTerminal()
    {
        throw new NotImplementedException();
    }

    protected override void LeaveTerminal()
    {
        throw new NotImplementedException();
    }
}

public class Truck : DeliveryVehicle
{
    protected override void EnterTerminal()
    {
        throw new NotImplementedException();
    }

    protected override void LeaveTerminal()
    {
        throw new NotImplementedException();
    }
}
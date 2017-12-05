using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeliveryVehicle : MonoBehaviour
{
    List<MonoContainer> carrying;
    List<Container> incoming;
    List<Container> outgoing;

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
}

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
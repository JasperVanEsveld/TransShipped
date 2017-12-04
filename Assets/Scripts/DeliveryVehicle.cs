using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeliveryVehicle : MonoBehaviour
{
    List<MonoContainer> _carrying;
    List<Container> _incoming;
    List<Container> _outgoing;

    private void Start()
    {
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
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeliveryVehicle : MonoBehaviour
{
    public List<MonoContainer> carrying;
    List<Container> incoming = new List<Container>();
    List<Container> outgoing = new List<Container>();

    private void Start()
    {
    }

    public List<MonoContainer> Carrying
    {
        get { return carrying; }
        set { carrying = value; }
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
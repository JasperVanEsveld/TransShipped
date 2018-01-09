using System.Collections.Generic;
using UnityEngine;

public abstract class DeliveryVehicle : MoveableObject
{
    public double reward;
    public double timeOutTime;
    public List<MonoContainer> carrying = new List<MonoContainer>();
    public List<Container> outgoing = new List<Container>();
    protected Game game { get; private set; }
    protected bool isAtDestination { get; set; }
    public Vector3 areaPos;

    public void Awake()
    {
        game = (Game) FindObjectOfType(typeof(Game));
        game.RegisterWaiting(this);
    }

    public void EnterTerminal()
    {
        game.vehicles.Remove(this);
        if (GetType() == typeof(Ship))
            MOShipEnterTerminal(areaPos);
        else
            MOPushDestination(areaPos);
    }

    public void LeaveTerminal()
    {
        if (GetType() == typeof(Ship))
            MOShipLeaveTerminal();
        else
            MOPushDestination(new Vector3(-50, 0, 42));
    }
}
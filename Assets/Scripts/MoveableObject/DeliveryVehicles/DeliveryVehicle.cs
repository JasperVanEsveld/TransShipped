using System.Collections.Generic;
using UnityEngine;

public abstract class DeliveryVehicle : MoveableObject
{
    public Stack targetStack;
    public double reward;
    public double timeOutTime;
    public List<MonoContainer> carrying = new List<MonoContainer>();
    public List<Container> outgoing = new List<Container>();
    protected bool isAtDestination { get; set; }
    public Vector3 areaPos;
    protected static readonly Vector3 truckSpawnPos = new Vector3(-50, 0, 42);
    protected static readonly Vector3 trainSpawnPos = new Vector3(-50, 0, -42);
    protected static readonly Vector3 shipSpawnPos = new Vector3(65, 0, 35);

    public void Awake()
    {
        Game.instance.RegisterWaiting(this);
    }

    public abstract void OnSelected();

    public void EnterTerminal()
    {
        Game.instance.vehicles.Remove(this);
        if (GetType() == typeof(Ship))
            MOShipEnterTerminal(areaPos);
        else
            MOPushDestination(areaPos);
    }

    public abstract void LeaveTerminal();

    protected abstract void DestroyIfDone();
    
    protected abstract void Enter();

    private void Update()
    {
        DestroyIfDone();
        if (!(Game.instance.currentState is OperationState)) return;
        MOMovementUpdate();

        if (isAtDestination || !MOIsAtTheThisPos(areaPos)) return;
        isAtDestination = true;

        Enter();
    }
}
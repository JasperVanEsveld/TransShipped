using System.CodeDom;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeliveryVehicle : MoveableObject
{
    public List<MonoContainer> carrying = new List<MonoContainer>();
    public List<Container> outgoing = new List<Container>();
    protected Game game { get; private set; }

    protected readonly Queue<Vector3> movementQueue = new Queue<Vector3>();

    public Vector3 areaPos;
    public Vector3 spawnPos;
    protected Vector3 interPos;

    protected float height = 0.0f;
    protected float speed = 20.0f;

    public void Awake(){
        game = (Game) FindObjectOfType(typeof(Game));
        game.RegisterWaiting(this);
    }

    protected Vector3 getNextPos()
    {
        //Debug.Log(movementQueue);
        float step = speed * Time.deltaTime;
        Vector3 tempTarget = movementQueue.Peek();
        return Vector3.MoveTowards(transform.position, tempTarget, step);
    }

    public void EnterTerminal() {
        game.vehicles.Remove(this);
        MOEnterTerminal(areaPos);
    }

    public void LeaveTerminal()
    {
        MOLeaveTerminal();
        
    }

    protected void GenerateRandomContainers(int from, int to)
    {
    }
}
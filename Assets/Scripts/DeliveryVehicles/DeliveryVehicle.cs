using System.Collections.Generic;
using UnityEngine;

public abstract class DeliveryVehicle : MonoBehaviour
{
    public List<MonoContainer> carrying;
    private List<Container> outgoing = new List<Container>();


    protected readonly Queue<Vector3> movementQueue = new Queue<Vector3>();

    private Vector3 destPos = new Vector3(17.0f, -1.0f, 17.0f);
    private Vector3 spawnPos = new Vector3(100.0f, -1.0f, 40.0f);
    private Vector3 interPos;

    protected float height = 0.0f;

    protected float speed = 20.0f;

    public List<Container> Outgoing
    {
        get { return outgoing; }
        set { outgoing = value; }
    }

    protected Vector3 getNextPos()
    {
        float step = speed * Time.deltaTime;
        Vector3 tempTarget = movementQueue.Peek();
        return Vector3.MoveTowards(transform.position, tempTarget, step);
    }

    protected void EnterTerminal()
    {
        interPos.x = destPos.x;
        interPos.y = height;
        interPos.z = spawnPos.z;

        movementQueue.Enqueue(interPos);
        movementQueue.Enqueue(destPos);
    }

    protected bool isAtDest()
    {
        return Vector3.Distance(destPos, transform.position) < speed * Time.deltaTime;
    }

    public void LeaveTerminal()
    {
        interPos.x = destPos.x;
        interPos.y = height;
        interPos.z = spawnPos.z;

        movementQueue.Enqueue(interPos);
        movementQueue.Enqueue(spawnPos);
    }
}
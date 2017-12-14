using System.Collections.Generic;
using UnityEngine;

public abstract class DeliveryVehicle : MonoBehaviour
{
    public List<MonoContainer> carrying = new List<MonoContainer>();
    public List<Container> outgoing = new List<Container>();
    protected Game game { get; private set; }

    protected readonly Queue<Vector3> movementQueue = new Queue<Vector3>();

    protected Vector3 destPos;
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
        print("Entering");
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

    protected void GenerateRandomContainers(int from, int to)
    {
    }
}
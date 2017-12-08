using System.Collections.Generic;
using UnityEngine;

public abstract class DeliveryVehicle : MonoBehaviour
{
    public List<MonoContainer> carrying;
    private List<Container> outgoing = new List<Container>();


    protected readonly Queue<Vector3> movementQueue = new Queue<Vector3>();

    protected Vector3 destPos = new Vector3(17.0f, -1.0f, 17.0f);
    protected Vector3 spawnPos = new Vector3(100.0f, -1.0f, 40.0f);
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

    protected void GenerateRandomContainers(int from, int to)
    {
        var rnd = new System.Random();
        var conCount = rnd.Next(from, to);

        for (var i = 0; i < conCount; ++i)
        {
            MonoContainer temp;
            switch (rnd.Next(0, 3))
            {
                case 0:
                    temp = Instantiate(Resources.Load("BlueContainer") as GameObject, transform.position,
                            transform.rotation)
                        .GetComponent<MonoContainer>();
                    temp.container = new Container(rnd.Next(0, 2) != 0, 0);
                    break;
                case 1:
                    temp = Instantiate(Resources.Load("RedContainer") as GameObject, transform.position,
                            transform.rotation)
                        .GetComponent<MonoContainer>();
                    temp.container = new Container(rnd.Next(0, 2) != 0, 1);
                    break;
                default:
                    temp = Instantiate(Resources.Load("GreenContainer") as GameObject, transform.position,
                            transform.rotation)
                        .GetComponent<MonoContainer>();
                    temp.container = new Container(rnd.Next(0, 2) != 0, 2);
                    break;
            }
            temp.movement = null;
            carrying.Add(temp);
        }
    }
}
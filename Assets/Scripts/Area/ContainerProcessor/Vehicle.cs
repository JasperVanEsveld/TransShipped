using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    private Game game;
    public Road road;
    public int capacity;
    public List<MonoContainer> containers = new List<MonoContainer>();
    public bool isMoving;
    public int speed;
    private Area nextStop;

    

    public Queue<Area> request { get; private set; }

    private void Awake()
    {
        request = new Queue<Area>();
        game = (Game) FindObjectOfType(typeof(Game));
    }

    public bool AddContainer(MonoContainer monoContainer)
    {
        if (containers.Count >= capacity) return false;
        containers.Add(monoContainer);
        monoContainer.transform.SetParent(transform);
        return true;
    }

    private bool isAtDestination(Area targetArea)
    {
        if (Vector3.Distance(targetArea.transform.position, transform.position) < speed * Time.deltaTime)
            return true;
        else
            return false;
    }

    public bool IsFull()
    {
        return containers.Count >= capacity;
    }

    private Vector3 movementStartPos;
    private Vector3 movementEndPos;
    private readonly static Vector3 cranePos = new Vector3(15.28f, 0.0f, 10.9f);

    public void GoTo(Area targetArea)
    {
        if(!isAtDestination(targetArea))
            isMoving = true;

        movementStartPos = cranePos;
        movementEndPos = targetArea.transform.position;

        Vector3 interPos = new Vector3();
        interPos.x = movementEndPos.x;
        interPos.y = 0.0f;
        interPos.z = movementStartPos.z;

        movementQueue.Enqueue(interPos);
        movementQueue.Enqueue(movementEndPos);


        nextStop = targetArea;
    }

    private Queue<Vector3> movementQueue;

    private void Start()
    {
        movementQueue = new Queue<Vector3>();

        // Spawn pos
        transform.position = cranePos;

    }

    private Vector3 getNextPos()
    {
        float step = speed * Time.deltaTime;
        Vector3 tempTarget = movementQueue.Peek();
        return Vector3.MoveTowards(transform.position, tempTarget, step);
    }

    private void Update()
    {
        if (!(game.currentState is OperationState)) return;
        for (var i = 0; i < containers.Count; i++)
            containers[i].transform.position = new Vector3(transform.position.x,
                transform.position.y + transform.lossyScale.y / 2 + i, transform.position.z);
        if (isMoving)
        {
            //todo Moving
            transform.position = getNextPos();
            isMoving = false;
        }
        else
        {
            if (containers.Count != 0)
            {
                GoTo(containers[0].movement.TargetArea);
                if (nextStop.AddContainer(containers[0]))
                {
                    containers.Remove(containers[0]);
                }
            }
            else
            {
                if (request.Count != 0)
                {
                    GoTo(request.Dequeue());
                    nextStop.AreaAvailable(road);
                }
            }
        }
    }
}
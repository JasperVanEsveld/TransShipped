using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    private Game game;
    public Road road;
    public int capacity;
    public List<MonoContainer> containers = new List<MonoContainer>();
    public bool isMoving;
    public float speed;
    public Area currentArea;
    private Area nextStop;

    public Queue<Area> request { get; private set; }
    private Queue<Vector3> movementQueue;

    private void Awake()
    {
        Debug.Log("Awake()");
        request = new Queue<Area>();
        game = (Game) FindObjectOfType(typeof(Game));
    }

    public bool AddContainer(MonoContainer monoContainer)
    {
        Debug.Log("AddContainer()");
        if (containers.Count >= capacity) return false;
        containers.Add(monoContainer);
        monoContainer.transform.SetParent(transform);
        return true;
    }

    public bool IsFull()
    {
        Debug.Log("IsFull()");
        return containers.Count >= capacity;
    }

    public void GoTo(Area targetArea)
    {
        Debug.Log("GoTo()");
        isMoving = true;
        nextStop = targetArea;
        if (NeedIntermediatePoint_(targetArea))
        {
            Vector3 temp1 = CreateIntermediatePoint_(targetArea);
            Vector3 temp2 = targetArea.transform.position;
            temp2.y = 0.0f;
            movementQueue.Enqueue(temp1);
            movementQueue.Enqueue(temp2);
        }
        else
        {
            Vector3 temp1 = new Vector3(0.0f, 0.0f, 0.0f);
            temp1.x = targetArea.transform.position.x;
            temp1.z = transform.position.z;
            Vector3 temp2 = targetArea.transform.position;
            temp2.y = 0.0f;
            movementQueue.Enqueue(temp1);
            movementQueue.Enqueue(temp2);
        }
    }

    private void MoveForThisFrame_()
    {
        if (movementQueue.Count == 0) return;
        else
        {
            float step = speed * Time.deltaTime;
            Vector3 tempTarget = movementQueue.Peek();
            transform.position = Vector3.MoveTowards(transform.position, tempTarget, step);
        }

        if (Vector3.Distance(movementQueue.Peek(), transform.position) < speed * Time.deltaTime){
            movementQueue.Dequeue();
        }

        if (movementQueue.Count == 0) {
            currentArea = nextStop;
            isMoving = false;
        } 
    }

    //Horrible Hack
    private bool NeedIntermediatePoint_(Area targetArea)
    {
        float u = transform.position.x;
        float v = transform.position.z;

        // If both located east of center zone
        if ((v >= 11.0f) && (targetArea.transform.position.z >= 11.0f))
        {
            return false;
        }
        // If both located west of center zone
        else if ((v <= -11.0f) && (targetArea.transform.position.z <= -11.0f))
        {
            return false;
        }
        // If both located south of center zone, aka near the crane
        else if ((u >= 0.0f) && (targetArea.transform.position.z >= 0.0f))
        {
            return false;
        }

        if ((v >= -11.0f) && (v <= 11.0f) && (u >= -25.0f) && (u <= -0.5f))
           return false;
        else 
           return true;
    }

    //Another Horrible Hack
    private Vector3 CreateIntermediatePoint_(Area targetArea)
    {
        Vector3 temp = transform.position;
        // If located east of center zone
        if (temp.z >= 11.0f)
        {
            temp.z = targetArea.transform.position.z;
            return temp;
        }
        // If located west of center zone
        else if (temp.z <= -11.0f)
        {
            temp.z = targetArea.transform.position.z;
            return temp;
        }
        // If located south of center zone, aka near the crane
        else
        {
            temp.x = targetArea.transform.position.x;
            return temp;
        }
    }

    private void Update()
    {
        
        if (!(game.currentState is OperationState)) { return; }
        for (var i = 0; i < containers.Count; i++)
            containers[i].transform.position = new Vector3(transform.position.x,
                transform.position.y + transform.lossyScale.y / 2 + i, transform.position.z);
        if (isMoving) {
            MoveForThisFrame_();

            
        }
        else {
            if (containers.Count != 0) {
                nextStop = ((OperationState)game.currentState).manager.GetNextArea(road, containers[0].movement);
                GoTo(nextStop);
                Debug.Log(currentArea);
                if(currentArea == nextStop) {
                    road.MoveToNext(containers[0]);
                }
            }
            else {
                //Debug.Log("Update: Branch 3");
                if (request.Count == 0) return;
                Debug.Log("Update: Branch 3.1");
                GoTo(request.Dequeue());
                nextStop.AreaAvailable(road);
            }
        }
    }

    private Vector3 cranePos_ = new Vector3(15.28f, 0.0f, 10.9f);

    private void Start() {
        movementQueue = new Queue<Vector3>();
        isMoving = false;
    }
}
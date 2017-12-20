using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MoveableObject
{
    private Game game;
    public Road road;
    public int capacity;
    public List<MonoContainer> containers = new List<MonoContainer>();
    public float speed;
    public Area currentArea;
    private Area targetArea;

    public Queue<Area> request { get; private set; }

    private void Awake()
    {
        Debug.Log("Awake()");
        request = new Queue<Area>();
        game = (Game) FindObjectOfType(typeof(Game));
        MOInit(transform.position, speed, false);
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
        //Debug.Log("IsFull()");
        return containers.Count >= capacity;
    }

    public void GoTo(Area i_targetArea)
    {
        MOPushNewDest(i_targetArea.transform.position);
        this.targetArea = i_targetArea;
    }


    private void Update()
    {
        if (!(game.currentState is OperationState))
        {
            return;
        }
        for (var i = 0; i < containers.Count; i++)
            containers[i].transform.position = new Vector3(transform.position.x,
                transform.position.y + transform.lossyScale.y / 2 + i, transform.position.z);
        MOMovementUpdate();
        if (!MOIsObjectMoving())
        {
            if (containers.Count != 0)
            {
                targetArea = ((OperationState) game.currentState).manager.GetNextArea(road, containers[0].movement);
                print(targetArea);

                GoTo(targetArea);
                print(targetArea);

                if (MOIsAtTheThisPos(targetArea.transform.position))
                    road.MoveToNext(containers[0]);
            }
            else
            {
                //Debug.Log("Update: Branch 3");
                if (request.Count == 0) return;
                GoTo(request.Dequeue());
                Debug.Log("no container: " + targetArea);
                targetArea.AreaAvailable(road);
            }
        }
    }
}
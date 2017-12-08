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

    public bool IsFull()
    {
        return containers.Count >= capacity;
    }

    public void GoTo(Area targetArea)
    {
        isMoving = true;
        nextStop = targetArea;
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
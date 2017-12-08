using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public Road road;
    public int capacity;
    public List<MonoContainer> containers = new List<MonoContainer>();
    public bool isOccupied;

    public Vehicle()
    {
        request = new Queue<Area>();
    }

    public Queue<Area> request { get; private set; }

    public bool AddContainer(MonoContainer monoContainer)
    {
        if (containers.Count >= capacity) return false;
        containers.Add(monoContainer);
        monoContainer.transform.SetParent(transform);
        return true;
    }

    public void GoTo(Area targetArea)
    {
        //isOccupied = true;
        //todo Animation for going to targetArea
    }

    public bool IsFull()
    {
        return containers.Count >= capacity;
    }

    private void Update()
    {
        for (var i = 0; i < containers.Count; i++)
            containers[i].transform.position = new Vector3(transform.position.x,
                transform.position.y + transform.lossyScale.y / 2 + i, transform.position.z);
        if (isOccupied) return;
        Area nextStop;
        if (containers.Count != 0)
        {
            nextStop = containers[0].movement.TargetArea;
            GoTo(nextStop);
            if (nextStop.AddContainer(containers[0]))
                containers.Remove(containers[0]);
        }
        else if (request.Count == 0) isOccupied = false;
        else
        {
            nextStop = request.Dequeue();
            GoTo(nextStop);
            nextStop.AreaAvailable(road);
        }
    }
}
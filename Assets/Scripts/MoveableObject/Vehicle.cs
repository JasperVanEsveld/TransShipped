using System;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MoveableObject
{
    private Game game;
    public Road road;
    public int capacity;
    public List<MonoContainer> containers = new List<MonoContainer>();
    public float speed;
    private Area targetArea;
    public Queue<Area> request = new Queue<Area>();
    public bool reserved;
    private Area reservedBy;

    public bool IsAvailable(Area origin)
    {
        return IsAvailable() || (!MOIsObjectMoving() && !IsFull() && reserved && reservedBy.Equals(origin));
    }

    public bool IsAvailable()
    {
        return !MOIsObjectMoving() && !IsFull() && !reserved;
    }

    public bool ReserveVehicle(Area origin){
        if(IsAvailable()){
            reservedBy = origin;
            reserved = true;
            return true;
        } else{
            return false;
        }
    }

    private void Awake()
    {
        request = new Queue<Area>();
        game = (Game) FindObjectOfType(typeof(Game));
        MOInit(transform.position, speed, false);
    }

    public bool AddContainer(MonoContainer monoContainer)
    {
        if (!IsAvailable(monoContainer.movement.originArea)) return false;
        containers.Add(monoContainer);
        monoContainer.transform.SetParent(transform);
        return true;
    }

    public bool IsFull()
    {
        return containers.Count >= capacity;
    }

    public void GoTo(Area i_targetArea)
    {
        MOPushNewDest(i_targetArea.transform.position);
        targetArea = i_targetArea;
    }


    private void Update()
    {
        if (!(game.currentState is OperationState)) return;
        for (var i = 0; i < containers.Count; i++)
            containers[i].transform.position = new Vector3(transform.position.x,
                transform.position.y + transform.lossyScale.y / 2 + i, transform.position.z);
        if (!MOIsObjectMoving())
        {
            if (containers.Count != 0)
            {
                targetArea = ContainerManager.GetNextArea(road, containers[0].movement);
                GoTo(targetArea);
                if (MOIsAtTheThisPos(targetArea.transform.position))
                    road.MoveToNext(containers[0]);
            }
            else
            {
                //Debug.Log("Update: Branch 3");
                if (request.Count == 0) return;
                GoTo(request.Dequeue());
                targetArea.AreaAvailable(road);
            }
        }

        MOMovementUpdate();
    }

    internal void RemoveContainer(MonoContainer monoContainer)
    {
        reserved = false;
        containers.Remove(monoContainer);
    }
}
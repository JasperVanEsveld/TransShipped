using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MoveableObject
{
    public Road road;
    public int capacity;
    public List<MonoContainer> containers = new List<MonoContainer>();
    private Area targetArea;
    public Queue<Area> request = new Queue<Area>();
    public bool reserved;
    private Area reservedBy;
    private static int countAGV;
    private static int level;
    private const int maxLevel = 4;
    private static readonly int[] costOfPurchase = {2, 3, 5, 7, 10};
    private static readonly int[] costOfUpgrade = {1, 2, 2, 3};
    private static readonly float[] speedAtEachLevel = {10, 12, 14, 18, 24};
    private static float speed = speedAtEachLevel[0];

    private static bool IsFullyUpgraded()
    {
        return level >= maxLevel;
    }

    public static int price
    {
        get { return costOfPurchase[level];}
    }

    public static int Upgrade()
    {
        if (IsFullyUpgraded())
            return -1;

        if (!UpgradeState.Buy(costOfUpgrade[level] * countAGV)) return 0;
        speed = speedAtEachLevel[++level];
        return level + 1;
    }

    public bool IsReservedBy(Area reference)
    {
        return reserved && reservedBy.Equals(reference);
    }

    public bool IsAvailable(Area origin)
    {
        return IsAvailable() || !MOIsObjectMoving() && !IsFull() && reserved && reservedBy.Equals(origin);
    }

    public bool IsAvailable()
    {
        return !MOIsObjectMoving() && !IsFull() && !reserved;
    }

    public bool ReserveVehicle(Area origin)
    {
        if (reserved) return false;
        reservedBy = origin;
        reserved = true;
        return true;
    }

    private void Awake()
    {
        countAGV++;
        transform.position = new Vector3(Random.Range(-35.0f, -5.0f), transform.position.y, Random.Range(-10.0f, 10.0f));
        request = new Queue<Area>();
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
        MOPushDestination(i_targetArea.transform.position);
        targetArea = i_targetArea;
    }


    private void Update()
    {
        UpdateSpeed(speed);
        if (!(Game.instance.currentState is OperationState)) return;
        UpdateSpeed(speed);
        for (var i = 0; i < containers.Count; i++)
            containers[i].transform.position = new Vector3(transform.position.x,
                transform.position.y + transform.lossyScale.y / 2 + i, transform.position.z);
        if (!MOIsObjectMoving())
        {
            if (containers.Count != 0)
            {
                targetArea = Game.GetManager().GetNextArea(road, containers[0].movement);
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
        containers.Remove(monoContainer);
    }
}
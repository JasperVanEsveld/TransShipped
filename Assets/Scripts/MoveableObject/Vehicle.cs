using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MoveableObject
{
    private Game game;

    /// <summary>
    /// For upgrading
    /// </summary>
    private static int countAGV;

    private static int level;
    private const int maxLevel = 4;
    private static readonly int[] costOfPurchase = {2, 3, 5, 7, 10};
    private static readonly int[] costOfUpgrade = {1, 2, 2, 3};
    private static readonly float[] speedAtEachLevel = {5, 6, 7.5f, 8, 30};
    private static float speed = speedAtEachLevel[0];
    
    public static bool IsFullyUpgraded()
    {
        return level >= maxLevel;
    }

    public static int UpgradeCost()
    {
        return IsFullyUpgraded() ? -1 : costOfUpgrade[level];
    }

    public static int PurchaseCost()
    {
        return costOfPurchase[level];
    }

    public bool IsUpgradeAffordable()
    {
        return game.money >= UpgradeCost() * countAGV;
    }

    public bool IsUpgradable()
    {
        return !IsFullyUpgraded() && IsUpgradeAffordable();
    }

    public bool Upgrade()
    {
        if (!(game.currentState is UpgradeState)) return false;
        if (!IsUpgradable()) return false;
        game.SetMoney(game.money - UpgradeCost() * countAGV);
        speed = speedAtEachLevel[++level];
        return true;
    }

    public Road road;
    public int capacity;
    public List<MonoContainer> containers = new List<MonoContainer>();
    private Area targetArea;
    public Queue<Area> request = new Queue<Area>();

    private void Awake()
    {
        request = new Queue<Area>();
        game = (Game) FindObjectOfType(typeof(Game));
        MOInit(transform.position, speed, false);
        ++countAGV;
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
                targetArea = ((OperationState) game.currentState).manager.GetNextArea(road, containers[0].movement);
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
}
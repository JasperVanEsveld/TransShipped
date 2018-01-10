using System;
using UnityEngine;

public class Crane : MonoBehaviour
{
    private const double upbound = 10;
    private double speed;
    private double baseTime;
    private DateTime startTime;
    public MonoContainer container { private get; set; }
    public CraneArea craneArea;
    public bool reserved;
    private Area reservedBy;

    // Upgrade and stuff
    private const int maxLevel = 3;
    private int level;
    private static readonly int[] costOfNextUpgrade = {5, 10, 20};
    private static readonly double[] speedAtEachLevel = {0.1, 2, 4, 8};

    private bool IsFullyUpgraded()
    {
        return level >= maxLevel;
    }

    private bool CanUpgrade()
    {
        return !IsFullyUpgraded() && !(Game.instance.money < costOfNextUpgrade[level]);
    }

    private bool Upgrade()
    {
        if (!CanUpgrade()) return false;
        Game.instance.money -= costOfNextUpgrade[level];
        speed = speedAtEachLevel[++level];
        return true;
    }
    // End of Upgrade and stuff

    private void Start()
    {
        speed = 1;
        baseTime = upbound - speed;
        container = null;
    }

    /// <summary>
    /// Check if the crane is available<br/>
    /// Return true if it is available and not reserved or reserved by you<br/>
    /// Otherwise return false
    /// </summary>
    /// <returns>Available or not</returns>
    public bool IsReady(Area origin)
    {
        return IsReady() || container == null && reserved && reservedBy.Equals(origin);
    }

    /// <summary>
    /// Check if the crane is available<br/>
    /// Return true if it is available<br/>
    /// Otherwise return false
    /// </summary>
    /// <returns>Available or not</returns>
    private bool IsReady()
    {
        return container == null && !reserved;
    }

    public bool IsReservedBy(Area reference)
    {
        return reserved && reservedBy.Equals(reference);
    }

    /// <summary>
    /// Reserves this crane if possible
    /// </summary>
    /// <param name="origin"></param>
    /// <returns></returns>
    public bool ReserveCrane(Area origin)
    {
        if (reserved) return false;
        reservedBy = origin;
        reserved = true;
        return true;
    }


    // TODO: Associate this with sth in the GUI
    private void OnMouseDown()
    {
        Upgrade();
    }

    public bool AddContainer(MonoContainer monoContainer)
    {
        if (!IsReady(monoContainer.movement.originArea))
            return false;

        reserved = false;
        container = monoContainer;
        container.transform.SetParent(transform);
        container.transform.position = transform.position;
        startTime = DateTime.Now;
        return true;
    }

    private void Update()
    {
        baseTime = upbound - speed;
        if (!(Game.instance.currentState is OperationState)) return;
        if (container != null && DateTime.Now.Subtract(startTime).TotalSeconds >= baseTime)
            craneArea.MoveToNext(container);
        else if (IsReady())
            craneArea.AreaAvailable();
    }
}
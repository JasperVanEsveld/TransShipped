using System;
using System.Collections.Generic;
using UnityEngine;

public class Crane : HighlightAble
{
    public delegate void CraneListener(Crane source);

    public static event CraneListener MouseDownEvent;
    public static event CraneListener CraneBought;
    public static event CraneListener NotEnough;

    private const double upbound = 10;
    public double speed { get; set; }
    private double baseTime;
    private DateTime startTime;
    public MonoContainer container { private get; set; }
    public CraneArea craneArea;
    public Queue<Area> reservedBy = new Queue<Area>();
    public BuildingPanel buildingPanel;

    // Upgrade and stuff
    private const int maxLevel = 3;

    private int level;

    public int Level
    {
        get { return level + 1; }
    }

    private static readonly int[] costOfUpgrade = {10, 20, 30};
    private static readonly double[] speedAtEachLevel = {7, 8, 9, 10};

    public bool IsFullyUpgraded()
    {
        return level >= maxLevel;
    }

    public int CostOfUpgrade
    {
        get { return costOfUpgrade[level]; }
    }

    private int i;

    public void Upgrade()
    {
        if (!(Game.instance.currentState is UpgradeState)) return;
        if (UpgradeState.Buy(costOfUpgrade[level]))
        {
            i = 0;
            speed = speedAtEachLevel[++level];
        }
        else if (i == 1)
            print("You don't have enough money!");

        i++;
    }
    // End of Upgrade and stuff

    private void Start()
    {
        buildingPanel = GameObject.Find("BuildingPanel").GetComponent<BuildingPanel>();
        speed = speedAtEachLevel[0];
        baseTime = upbound - speed;
        container = null;
        InitHighlight();
    }

    /// <summary>
    /// Check if the crane is available<br/>
    /// Return true if it is available and not reserved or reserved by you<br/>
    /// Otherwise return false
    /// </summary>
    /// <returns>Available or not</returns>
    public bool IsReady(Area origin)
    {
        return IsReady() || container == null && (reservedBy.Count == 0 || reservedBy.Peek() == origin);
    }

    /// <summary>
    /// Check if the crane is available<br/>
    /// Return true if it is available<br/>
    /// Otherwise return false
    /// </summary>
    /// <returns>Available or not</returns>
    private bool IsReady()
    {
        return container == null && reservedBy.Count == 0;
    }

    public bool IsReservedBy(Area reference)
    {
        return reservedBy.Count != 0 && reservedBy.Peek() == reference;
    }

    /// <summary>
    /// Reserves this crane if possible
    /// </summary>
    /// <param name="origin"></param>
    /// <returns></returns>
    public bool ReserveCrane(Area origin)
    {
        if (reservedBy.Contains(origin)) return false;
        reservedBy.Enqueue(origin);
        return true;
    }
    
    private void OnMouseDown() {
        if (!(Game.instance.currentState is UpgradeState)) return;
        if (MouseDownEvent != null) { MouseDownEvent.Invoke(this); }
        buildingPanel.SelectCrane(this);
        Game.ForceRemoveHighlights();
        this.Highlight(true);
        lastClicked = true;
    }

    private void OnMouseEnter() {
        if (!(Game.instance.currentState is UpgradeState)) return;
        Game.RemoveHighlights();
        this.Highlight(true);
    }

    private void OnMouseExit() {
        if (!(Game.instance.currentState is UpgradeState) || lastClicked) return;
        this.Highlight(false);
    }

    public bool AddContainer(MonoContainer monoContainer)
    {
        if (!IsReady(monoContainer.movement.originArea))
            return false;
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
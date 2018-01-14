using System.Collections.Generic;
using UnityEngine;

public class CraneArea : Area
{
    private readonly Dictionary<MonoContainer, Crane> containerCrane = new Dictionary<MonoContainer, Crane>();
    public List<Crane> cranes = new List<Crane>();
    public double priceForOneCrane = 10;
    public int maxCranes = 2;
    public int offSet = 5;

    public GameObject cranePrefab;
    private readonly Color selected = new Color32(0xC0, 0xC0, 0xC0, 0xFF);
    private readonly Color craneAreaColor = new Color32(169, 158, 16, 255);
    private BuildingPanel buildingPanel;
    public string areaName, attribute;

    private new void Start()
    {
        buildingPanel = GameObject.Find("BuildingPanel").GetComponent<BuildingPanel>();
        Game.RegisterArea(this);
    }

    private int i;

    public void BuyCrane()
    {
        if (!(Game.currentState is UpgradeState)) return;
        if (cranes.Count < maxCranes && UpgradeState.Buy(priceForOneCrane))
        {
            i = 0;
            var crane = Instantiate(cranePrefab,
                new Vector3(transform.position.x + cranes.Count * offSet, 0, transform.position.z),
                transform.rotation).GetComponent<Crane>();
            cranes.Add(crane);
            crane.craneArea = this;
            crane.transform.SetParent(transform);
        }
        else if (i == 1)
            print("You don't have enough money!");

        i++;
    }

    private void OnMouseDown()
    {
        buildingPanel.SelectCraneArea(this, areaName, attribute);
    }

    private void OnMouseEnter()
    {
        if (!(Game.currentState is UpgradeState)) return;
        GetComponent<Renderer>().material.color = selected;
    }

    private void OnMouseExit()
    {
        if (!(Game.currentState is UpgradeState)) return;
        GetComponent<Renderer>().material.color = craneAreaColor;
    }

    /// <summary>
    /// Find an available crane in this crane area.<br/>
    /// If found, return it, otherwise return null.
    /// </summary>
    /// <returns>Available crane found</returns>
    private Crane FindAvailableCrane()
    {
        foreach (var crane in cranes)
            if (!crane.reserved)
                return crane;
        return null;
    }

    private Crane CompleteReservation(Area reference)
    {
        foreach (Crane crane in cranes)
        {
            if (!crane.IsReservedBy(reference) || !crane.IsReady(reference)) continue;
            crane.reserved = false;
            return crane;
        }

        return null;
    }

    /// <summary>
    /// Find an available crane in this crane area.<br/>
    /// If found, return it, otherwise return null.
    /// </summary>
    /// <returns>Available crane found</returns>
    private Crane FindReadyCrane(Area origin)
    {
        foreach (Crane crane in cranes)
            if (crane.IsReady(origin))
                return crane;
        return null;
    }

    /// <summary>
    /// Call a crane to move the container to its target area.<br/>
    /// Return true is the operation is a success.<br/>
    /// Otherwise return false.
    /// </summary>
    /// <param name="monoContainer">The container to be moved</param>
    /// <returns>Whether the operation is a success</returns>
    public override bool AddContainer(MonoContainer monoContainer)
    {
        Crane crane = CompleteReservation(monoContainer.movement.originArea);
        if (crane != null)
        {
            containerCrane.Add(monoContainer, crane);
            return crane.AddContainer(monoContainer);
        }

        crane = FindReadyCrane(monoContainer.movement.originArea);

        if (crane == null) return false;
        Area next = Game.GetManager().GetNextArea(this, monoContainer.movement);
        if (!next.ReserveArea(this, monoContainer.movement)) return false;
        containerCrane.Add(monoContainer, crane);
        return crane.AddContainer(monoContainer);

    }

    protected override void RemoveContainer(MonoContainer monoContainer)
    {
        containerCrane[monoContainer].container = null;
        containerCrane.Remove(monoContainer);
    }

    public override bool ReserveArea(Area origin, Movement move)
    {
        Crane crane = FindAvailableCrane();
        if (crane == null || !Game.GetManager().GetNextArea(this, move).ReserveArea(this, move)) return false;
        bool reserved = crane.ReserveCrane(origin);
        return reserved;

    }
}
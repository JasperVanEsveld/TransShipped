using System.Collections.Generic;
using UnityEngine;

public class CraneArea : Area
{
    public delegate void CraneAreaListener(CraneArea source);
    public static event CraneAreaListener MouseDownEvent;
    public static event CraneAreaListener CraneBought;
    public static event CraneAreaListener NotEnough;
    public static event CraneAreaListener NoCraneWarning;

    private readonly Dictionary<MonoContainer, Crane> containerCrane = new Dictionary<MonoContainer, Crane>();
    public List<Crane> cranes = new List<Crane>();
    public double priceForOneCrane = 10;
    public int maxCranes = 2;
    public int offSet = 5/2;

    public GameObject cranePrefab;
    private readonly Color selected = new Color32(0xC0, 0xC0, 0xC0, 0xFF);
    private readonly Color craneAreaColor = new Color32(169, 158, 16, 255);
    private BuildingPanel buildingPanel;
    public string areaName, attribute;

    private new void Start()
    {
        buildingPanel = GameObject.Find("BuildingPanel").GetComponent<BuildingPanel>();
        Game.instance.RegisterArea(this);
        InitHighlight();
    }

    private int i;

    public bool NoMoreSpace()
    {
        return cranes.Count >= maxCranes;
    }

    public void BuyCrane()
    {
        if (!(Game.instance.currentState is UpgradeState)) return;
        if (cranes.Count < maxCranes && UpgradeState.Buy(priceForOneCrane))
        {
            i = 0;
            var crane = Instantiate(cranePrefab,
                new Vector3(transform.position.x + cranes.Count * offSet, 0, transform.position.z),
                transform.rotation).GetComponentInChildren<Crane>();
            Vector3 temp = crane.transform.localScale;
            temp.x /= 2; temp.y /= 2; temp.z /= 2;
            crane.transform.localScale = temp;
            cranes.Add(crane);
            crane.craneArea = this;
            crane.transform.SetParent(transform);
            if(CraneBought != null){
                CraneBought.Invoke(this);
            }
        }
        else if (i == 1)
            print("You don't have enough money!");
            if(NotEnough != null){
                NotEnough.Invoke(this);
            }
        i++;
    }

    private void OnMouseDown()
    {
        if (!(Game.instance.currentState is UpgradeState)) return;
        if(MouseDownEvent != null){
            MouseDownEvent.Invoke(this);
        }
        buildingPanel.SelectCraneArea(this, areaName, attribute);
        Game.instance.ForceRemoveHighlights();
        Highlight(true);
        lastClicked = true;
    }

    private void OnMouseEnter()
    {
        if (!(Game.instance.currentState is UpgradeState)) return;
        Game.instance.RemoveHighlights();
        Highlight(true);
    }

    private void OnMouseExit()
    {
        if (!(Game.instance.currentState is UpgradeState)) return;
        Highlight(false);
    }

    /// <summary>
    /// Find an available crane in this crane area.<br/>
    /// If found, return it, otherwise return null.
    /// </summary>
    /// <returns>Available crane found</returns>
    private Crane FindAvailableCrane(Area origin)
    {
        foreach (var crane in cranes)
            if (!crane.reservedBy.Contains(origin))
                return crane;
        return null;
    }

    private Crane CompleteReservation(Area reference)
    {
        foreach (Crane crane in cranes)
        {
            if (!crane.IsReservedBy(reference) || !crane.IsReady(reference)) continue;
            crane.reservedBy.Dequeue();
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
        if(NoCraneWarning != null && cranes.Count == 0) {
            NoCraneWarning.Invoke(this);
        }
        Crane crane = CompleteReservation(monoContainer.movement.originArea);
        if (crane != null)
        {
            containerCrane.Add(monoContainer, crane);
            return crane.AddContainer(monoContainer);
        }

        crane = FindReadyCrane(monoContainer.movement.originArea);

        if (crane == null) return false;
        Area next = Game.instance.GetManager().GetNextArea(this, monoContainer.movement);
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
        Crane crane = FindAvailableCrane(origin);
        if(crane != null && crane.reservedBy.Count >= 1){
            print("Trying first servicing: " + crane.reservedBy.Peek());
        } else if(crane == null){
            print("Already servicing: " + origin);
        } else{
            print("Trying no one in queue");
        }
        if (crane == null || !Game.instance.GetManager().GetNextArea(this, move).ReserveArea(this, move)) return false;
        bool reserved = crane.ReserveCrane(origin);
        print(crane.reservedBy.Count);
        return reserved;
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Area : MonoBehaviour
{
    protected Game game;

    protected ContainerManager manager { get; private set; }

    protected Area()
    {
        containers = new List<MonoContainer>();
        manager = game.manager;
    }

    /// <summary>
    /// Max number of containers possible
    /// </summary>
    protected int max { private get; set; }

    /// <summary>
    /// Check if it is full, return true if number of containers = max
    /// </summary>
    /// <returns>True if number of containers = max</returns>
    public bool IsFull()
    {
        return containers.Count >= max;
    }

    /// <summary>
    /// Containers this holds
    /// </summary>
    public List<MonoContainer> containers { get; private set; }

    private void Start()
    {
        manager = game.manager;
    }

    protected virtual bool AddContainer(MonoContainer monoContainer)
    {
        return true;
    }

    protected virtual bool RemoveContainer(MonoContainer monoContainer)
    {
        return true;
    }

    public virtual void OnVehicleEnter(Vehicle v)
    {
    }

    /// <summary>
    /// Search for an idle vehicle in manager's list of vehicles<br/>
    /// If found, return the idle vehicle<br/>
    /// Otherwise return null
    /// </summary>
    /// <returns></returns>
    public Vehicle SummonIdleVehicle()
    {
        foreach (var vehicle in manager.vehicles)
        {
            if (!vehicle.isIdle) continue;
            vehicle.Transport(this);
            return vehicle;
        }
        return null;
    }

    public override bool Equals(object o)
    {
        var that = o as Area;
        return that != null && gameObject.Equals(that.gameObject);
    }

    public override int GetHashCode()
    {
        var hashCode = base.GetHashCode();
        return hashCode;
    }

    [Obsolete("I don't know what this is for")] public List<Area> connected;

    [Obsolete("I don't know what this is for")]
    protected void MoveToNext(MonoContainer monoContainer)
    {
        if (monoContainer.movement == null) return;
        var nextArea = manager.GetNextArea(this, monoContainer.movement);
        if (!nextArea.AddContainer(monoContainer)) return;
        RemoveContainer(monoContainer);
    }
}
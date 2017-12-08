using System.Collections.Generic;
using UnityEngine;

public abstract class Area : MonoBehaviour
{
    public Game game { get; private set; }
    public List<Area> connected;

    public void Awake()
    {
        game = (Game) FindObjectOfType(typeof(Game));
        game.RegisterArea(this);
    }

    public void Connect(Area connectArea)
    {
        connected.Add(connectArea);
        connectArea.connected.Add(this);
    }

    protected abstract bool AddContainer(MonoContainer monoContainer);

    protected abstract void RemoveContainer(MonoContainer monoCont);

    protected bool MoveToNext(MonoContainer monoCont)
    {
        if (monoCont.movement == null || !(game.currentState is OperationState)) return false;
        var nextArea = ((OperationState) game.currentState).manager.GetNextArea(this, monoCont.movement);
        if (!nextArea.AddContainer(monoCont)) return false;
        RemoveContainer(monoCont);
        return true;
    }

    public override bool Equals(object o)
    {
        var that = o as Area;
        return that != null && gameObject.Equals(that.gameObject);
    }
}
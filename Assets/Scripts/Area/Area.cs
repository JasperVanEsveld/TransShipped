using System.Collections.Generic;
using UnityEngine;

public delegate void OnAreaAvailable();

public abstract class Area : MonoBehaviour
{
    protected Game game { get; private set; }
    public List<Area> connected;
    private Dictionary<Area, Queue<MonoContainer>> containerQueue = new Dictionary<Area, Queue<MonoContainer>>();
    public List<Area> listening = new List<Area>();

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

    public void AddListener(Area area)
    {
        if (!listening.Contains(area))
        {
            listening.Add(area);
        }
    }

    public void AreaAvailable(Area availableTo)
    {
        if (!listening.Contains(availableTo)) return;
        availableTo.OnAreaAvailable(this);
        listening.Remove(availableTo);
    }

    protected void AreaAvailable()
    {
        for (var i = listening.Count - 1; i >= 0; i--)
        {
            listening[i].OnAreaAvailable(this);
        }
    }

    public void OnAreaAvailable(Area area)
    {
        Queue<MonoContainer> queue = containerQueue[area];
        MonoContainer container;
        if (queue.Count > 0)
        {
            container = queue.Dequeue();
        }
        else
        {
            area.listening.Remove(this);
            return;
        }
        MoveToNext(container);
        if (queue.Count == 0)
        {
            area.listening.Remove(this);
        }
    }

    public abstract bool AddContainer(MonoContainer monoContainer);

    protected abstract void RemoveContainer(MonoContainer monoCont);

    protected bool MoveToNext(MonoContainer monoCont)
    {
        if (monoCont.movement == null || !(game.currentState is OperationState)) return false;
        var nextArea = ((OperationState) game.currentState).manager.GetNextArea(this, monoCont.movement);
        monoCont.movement.originArea = this;
        if (!nextArea.AddContainer(monoCont)) return false;
        monoCont.transform.SetParent(nextArea.transform);
        monoCont.transform.position = nextArea.transform.position;
        RemoveContainer(monoCont);
        return true;
    }

    protected void AddToQueue(MonoContainer monoCont)
    {
        if (monoCont.movement == null || !(game.currentState is OperationState)) return;
        Area nextArea = ((OperationState) game.currentState).manager.GetNextArea(this, monoCont.movement);
        if (!containerQueue.ContainsKey(nextArea))
        {
            containerQueue.Add(nextArea, new Queue<MonoContainer>());
        }
        if (!containerQueue[nextArea].Contains(monoCont))
        {
            containerQueue[nextArea].Enqueue(monoCont);
        }
        nextArea.AddListener(this);
    }
}
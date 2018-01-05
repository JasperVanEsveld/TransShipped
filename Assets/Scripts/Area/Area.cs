using System.Collections.Generic;
using UnityEngine;

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

    private void AddListener(Area area)
    {
        if (!listening.Contains(area))
            listening.Add(area);
    }

    public void AreaAvailable(Area availableTo)
    {
        if (listening.Contains(availableTo))
            availableTo.OnAreaAvailable(this);
    }

    public void AreaAvailable()
    {
        for (var i = listening.Count - 1; i >= 0; i--)
            listening[i].OnAreaAvailable(this);
    }

    private void OnAreaAvailable(Area area)
    {
        Queue<MonoContainer> queue = containerQueue[area];
        MonoContainer container;
        if (queue.Count > 0)
            container = queue.Dequeue();
        else
        {
            area.listening.Remove(this);
            return;
        }

        MoveToNext(container);
        if (queue.Count == 0)
            area.listening.Remove(this);
    }

    public abstract bool AddContainer(MonoContainer monoContainer);

    public abstract void RemoveContainer(MonoContainer monoCont);

    public bool MoveToNext(MonoContainer monoCont)
    {
        if (monoCont.movement == null || !(game.currentState is OperationState)) return false;
        var nextArea = ContainerManager.GetNextArea(this, monoCont.movement);
        Area previousOrigin = monoCont.movement.originArea;
        monoCont.movement.originArea = this;
        if (!nextArea.AddContainer(monoCont))
        {
            monoCont.movement.originArea = previousOrigin;
            return false;
        }

        RemoveContainer(monoCont);
        return true;
    }

    protected void AddToQueue(MonoContainer monoCont)
    {
        if (monoCont.movement == null || !(game.currentState is OperationState)) return;
        Area nextArea = ContainerManager.GetNextArea(this, monoCont.movement);
        if (!containerQueue.ContainsKey(nextArea))
            containerQueue.Add(nextArea, new Queue<MonoContainer>());

        if (!containerQueue[nextArea].Contains(monoCont))
            containerQueue[nextArea].Enqueue(monoCont);

        nextArea.AddListener(this);
    }
}
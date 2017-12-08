using System.Collections.Generic;
using UnityEngine;

public delegate void OnAreaAvailable();

public abstract class Area : MonoBehaviour {
    protected Game game { get; private set; }
    public List<Area> connected;
    private Dictionary<Area,Queue<MonoContainer>> containerQueue = new Dictionary<Area, Queue<MonoContainer>>();
    public List<Area> listening = new List<Area>();

    public void Awake() {
        game = (Game) FindObjectOfType(typeof(Game));
        game.RegisterArea(this);
    }

    public void Connect(Area connectArea) {
        connected.Add(connectArea);
        connectArea.connected.Add(this);
    }

    public void AddListener(Area area){
        if(!listening.Contains(area)){
            listening.Add(area);
        }
    }

    protected void AreaAvailable(Area availableTo){
        if(listening.Contains(availableTo)){
            availableTo.OnAreaAvailable(this);
            listening.Remove(availableTo);
        }
    }

    protected void AreaAvailable(){
        foreach(Area a in listening){
            a.OnAreaAvailable(this);
        }
        listening.Clear();
    }

    public void OnAreaAvailable(Area area){
        Queue<MonoContainer> queue = containerQueue[area];
        MonoContainer container;
        if(queue.Count > 0){
            container = queue.Dequeue();
        } else{
            return;
        }
        MoveToNext(container);
        print("Ready!");
    }

    protected abstract bool AddContainer(MonoContainer monoContainer);

    protected abstract void RemoveContainer(MonoContainer monoCont);

    protected bool MoveToNext(MonoContainer monoCont) {
        if (monoCont.movement == null || !(game.currentState is OperationState)) return false;
        var nextArea = ((OperationState) game.currentState).manager.GetNextArea(this, monoCont.movement);
        if (!nextArea.AddContainer(monoCont)) return false;
        monoCont.transform.SetParent(nextArea.transform);
        monoCont.transform.position = nextArea.transform.position;
        RemoveContainer(monoCont);

        return true;
    }

    protected void AddToQueue(MonoContainer monoCont){
        if (monoCont.movement == null || !(game.currentState is OperationState)) return;
        Area nextArea = ((OperationState) game.currentState).manager.GetNextArea(this, monoCont.movement);
        if(!containerQueue.ContainsKey(nextArea)){
            containerQueue.Add(nextArea,new Queue<MonoContainer>());
        }
        if(!containerQueue[nextArea].Contains(monoCont)){
            containerQueue[nextArea].Enqueue(monoCont);
            print("NotAvailable added to queue, queue count: " + containerQueue[nextArea].Count);
            
        }
        nextArea.AddListener(this);
    }
}
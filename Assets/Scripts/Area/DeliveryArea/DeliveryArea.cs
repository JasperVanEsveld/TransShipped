using System;
using System.Collections.Generic;

public class DeliveryArea<T> : Area where T : DeliveryVehicle
{
    public bool occupied;
    private DateTime start;
    private List<Container> remainingRequests;
    private Queue<T> waiting = new Queue<T>();
    private T current;
    private bool loading;

    
    private void OnMouseDown()
    {
        if (!highlight || !(Game.instance.currentState is OperationState)) return;
        CommandPanel commandPanel = FindObjectOfType<CommandPanel>();
        commandPanel.SetDeliveryArea(this);
    }

    public void OnVehicleEnter(T vehicle)
    {
        if (vehicle == current || waiting.Contains(vehicle)) return;
        if (!(Game.instance.currentState is OperationState)) return;
        if (current == null)
        {
            current = vehicle;
            Service(current);
        }
        else
            waiting.Enqueue(vehicle);
    }

    private void Service(T vehicle)
    {
        start = DateTime.Now;
        loading = false;
        remainingRequests = new List<Container>(vehicle.outgoing);
        foreach (var container in vehicle.carrying)
            ((OperationState) Game.instance.currentState).manager.Store(this, container, vehicle.targetStack);

        for (var i = vehicle.carrying.Count - 1; i >= 0; i--)
            if (!MoveToNext(vehicle.carrying[i]))
                AddToQueue(vehicle.carrying[i]);
    }

    public void requestOutgoing()
    {
        for (int i = remainingRequests.Count - 1; i >= 0; i--)
        {
            Container c = remainingRequests[i];
            ContainerManager manager = Game.GetManager();
            if (manager == null) continue;
            if (manager.Request(this, c))
                remainingRequests.RemoveAt(i);
        }
    }

    private void OnVehicleLeaves()
    {
        current.LeaveTerminal();
        occupied = false;
        current = null;
        if (waiting.Count == 0) return;
        current = waiting.Dequeue();
        Service(current);
    }

    private void Update()
    {
        if (current == null) return;
        if (start.Subtract(DateTime.Now).TotalSeconds >= current.timeOutTime)
            OnVehicleLeaves();
        else if (current.carrying.Count == 0 && !loading)
            loading = true;
        else if (loading && remainingRequests.Count > 0)
            requestOutgoing();
        else if (loading && current.outgoing.Count == current.carrying.Count)
        {
            Game.money += current.reward;
            OnVehicleLeaves();
        }
    }

    public override bool AddContainer(MonoContainer monoContainer)
    {
        current.carrying.Add(monoContainer);
        monoContainer.transform.SetParent(current.transform);
        monoContainer.transform.position = current.transform.position;
        monoContainer.movement = null;
        return true;
    }

    protected override void RemoveContainer(MonoContainer monoContainer)
    {
        current.carrying.Remove(monoContainer);
    }
}
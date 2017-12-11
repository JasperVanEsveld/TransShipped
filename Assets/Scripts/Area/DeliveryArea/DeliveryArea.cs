using System.Collections.Generic;

public class DeliveryArea<T> : Area where T : DeliveryVehicle
{
    private Queue<T> waiting = new Queue<T>();
    private T current;

    public void OnVehicleEnter(T vehicle) {
        
        if(vehicle == current || waiting.Contains(vehicle)) return;
        if (!(game.currentState is OperationState)) return;
        if (current == null)
        {
            current = vehicle;
            Service(current);
        }
        else
        {
            waiting.Enqueue(vehicle);
        }
    }

    private void Service(T vehicle){
        foreach (var container in vehicle.carrying)
        {
            ((OperationState) game.currentState).manager.Store(this, container);
        }
        foreach (var container in vehicle.outgoing)
        {
            ((OperationState) game.currentState).manager.Request(this, container);
        }
        for (var i = vehicle.carrying.Count - 1; i >= 0; i--) {
            if (!MoveToNext(vehicle.carrying[i])) {
                AddToQueue(vehicle.carrying[i]);
            }
        }
    }

    private void OnVehicleLeaves()
    {
        current.LeaveTerminal();
        current = null;
        if (waiting.Count != 0) {
            current = waiting.Dequeue();
            Service(current);
        }
    }

    private void Update() {
        if(current != null && current.carrying.Count == 0){
            OnVehicleLeaves();
        }
    }

    public override bool AddContainer(MonoContainer monoContainer)
    {
        current.carrying.Add(monoContainer);
        monoContainer.movement = null;
        return true;
    }

    protected override void RemoveContainer(MonoContainer monoContainer)
    {
        current.carrying.Remove(monoContainer);
    }
}
using System.Collections.Generic;

public class DeliveryArea<T> : Area where T : DeliveryVehicle
{
    private Queue<T> waiting = new Queue<T>();

    private T current;

    public void OnVehicleEnter(T vehicle)
    {
        if (!(game.currentState is OperationState)) return;
        foreach (var container in vehicle.carrying)
        {
            ((OperationState) game.currentState).manager.Store(this, container);
        }
        foreach (var container in vehicle.Outgoing)
        {
            ((OperationState) game.currentState).manager.Request(this, container);
        }

        if (waiting.Count == 0)
        {
            current = vehicle;
        }
        else
        {
            waiting.Enqueue(vehicle);
        }
    }

    void OnVehicleLeaves()
    {
        current.LeaveTerminal();
        if (waiting.Count != 0)
        {
            current = waiting.Dequeue();
        }
    }

    private void Update()
    {
        if (current == null) return;
        foreach (var container in current.carrying)
        {
            if (MoveToNext(container))
            {
                break;
            }
        }
    }

    protected override bool AddContainer(MonoContainer monoContainer)
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
using System.Collections.Generic;

public class DeliveryArea<T> : Area where T : DeliveryVehicle
{
    /// <summary>
    /// Vehicles waiting outside this area
    /// </summary>
    private Queue<T> waiting = new Queue<T>();

    private T current;

    /// <summary>
    /// A new vehicle just entered this area, put it in the queue
    /// </summary>
    /// <param name="vehicle"></param>
    public void OnVehicleEnter(T vehicle)
    {
        if (!(game.currentState is OperationState)) return;
        foreach (var container in vehicle.Carrying)
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
        if (waiting.Count != 0)
        {
            current = waiting.Dequeue();
        }
    }

    /// <summary>
    /// Serving a vehicle, 
    /// </summary>
    void Update()
    {
        if (current == null) return;
        foreach (var container in current.Carrying)
        {
            if (MoveToNext(container))
            {
                break;
            }
        }
    }

    protected override bool AddContainer(MonoContainer monoContainer)
    {
        current.Carrying.Add(monoContainer);
        monoContainer.movement = null;
        return true;
    }

    protected override void RemoveContainer(MonoContainer monoContainer)
    {
        current.Carrying.Remove(monoContainer);
    }
}
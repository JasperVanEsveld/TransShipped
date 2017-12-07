using System.Collections.Generic;

public abstract class DeliveryArea<T> : Area where T : DeliveryVehicle
{
    /// <summary>
    /// This is a queue of delivery vehicles waiting at a delivery area
    /// </summary>
    private Queue<T> waiting { get; set; }

    /// <summary>
    /// The delivery vehicle currently being served, which is the first<br/>
    /// one of the queue
    /// </summary>
    private T current { get; set; }

    /// <summary>
    /// Vehicle to take containers unloaded from the delivery vehicle<br/>
    /// If it is full, need to summon another vehicle
    /// </summary>
    private Vehicle vehicle { get; set; }

    /// <summary>
    /// The crane over this area
    /// </summary>
    private Crane crane { get; set; }

    protected DeliveryArea()
    {
        waiting = new Queue<T>();
        current = null;
        vehicle = null;
        crane = null;
    }

    /// <summary>
    /// A new delivery vehicle is entering this area, if none is being served right now,<br/>
    /// serve this delivery vehicle immediately, otherwise put it in the queue<br/>
    /// At the same time, summon idle vehicle to take containers when there isn't one
    /// </summary>
    /// <param name="deliveryVehicle">The incoming delivery vehicle</param>
    public void OnDeliveryEnter(T deliveryVehicle)
    {
        if (waiting.Count == 0) current = deliveryVehicle;
        else waiting.Enqueue(deliveryVehicle);
        while (vehicle == null) vehicle = SummonIdleVehicle();
    }

    /// <summary>
    /// When a vehicle arrives, make it to take containers unloaded from the delivery vehicle
    /// </summary>
    /// <param name="v">The incoming vehicle</param>
    public override void OnVehicleEnter(Vehicle v)
    {
        vehicle = v;
    }

    /// <summary>
    /// When a delivery vehicle leaves, start to serve next first one of the queue<br/>
    /// If the queue is empty, dismiss the vehicle
    /// </summary>
    private void OnDeliveryLeft()
    {
        current.LeaveTerminal();
        if (waiting.Count == 0)
        {
            vehicle.LeaveArea();
            return;
        }
        current = waiting.Dequeue();
    }

    /// <summary>
    /// As for a delivery area, AddContainer means dismounting containers from incoming<br/>
    /// vehicle to the terminal and take the player's input to decide which stack to go to
    /// </summary>
    /// <param name="monoContainer">containers from the vehicle</param>
    /// <returns></returns>
    protected override bool AddContainer(MonoContainer monoContainer)
    {
        return crane.Reposition(current, monoContainer, vehicle);
    }

    protected override bool RemoveContainer(MonoContainer monoContainer)
    {
        return crane.Reposition(vehicle, monoContainer, current);
    }

    /// <summary>
    /// Serving the current delivery vehicle
    /// </summary>
    private void Update()
    {
        if (current == null)
        {
            if (waiting.Count == 0) return;
            current = waiting.Dequeue();
            while (vehicle == null)
                vehicle = SummonIdleVehicle();
        }
        for (var i = current.containers.Count - 1; i >= 0; i--)
        {
            if (AddContainer(current.containers[i])) continue;
            vehicle.LeaveArea();
            do vehicle = SummonIdleVehicle(); while (vehicle == null);
            AddContainer(current.containers[i]);
        }
        foreach (var container in current.outgoing)
        {
            var temp = manager.Request(this, container);
            if (temp == null) continue;
            var v = temp.movement.originArea.SummonIdleVehicle();
            v.request.Add(temp);
            v.Transport(temp.movement.originArea);
        }
        OnDeliveryLeft();
    }

//    private void Update()
//    {
//        if (current == null)
//        {
//            if (waiting.Count == 0) return;
//            current = waiting.Dequeue();
//        }
//
//        var deliver = current.containers;
//        while (deliver.Count > 0)
//        {
//            if (MoveToNext(deliver[0]))
//                break;
//        }
//        foreach (var container in deliver)
//        {
//            AddContainer(container);
//        }
//
//        var fetch = current.outgoing;
//        foreach (var container in fetch)
//        {
//            game.manager.Request(this, container);
//        }
//        OnDeliveryLeft();
//    }
}
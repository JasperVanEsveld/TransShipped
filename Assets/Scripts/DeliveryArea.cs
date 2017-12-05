using System;
using System.Collections.Generic;

public class DeliveryArea<DeliveryVehicle> : Area
{
    /// <summary>
    /// Vehicles currently waiting to fetch or deliver containers
    /// </summary>
    private Queue<DeliveryVehicle> waiting;
    private DeliveryVehicle processing;

    /// <summary>
    /// A new vehicle just entered this area, put it in the queue
    /// </summary>
    /// <param name="vehicle"></param>
    void OnVehicleEnter(DeliveryVehicle vehicle)
    {
        waiting.Enqueue(vehicle);
    }

    /// <summary>
    /// If a vehicle is coming in empty, then give it containers.
    /// </summary>
    void OnVehicleEmpty()
    {
        p
    }

    /// <summary>
    /// When a vehicle left a delivery area, the first one at<br/>
    /// the beginning of the queue is going to be processed next
    /// </summary>
    void OnVehicleLeft()
    {
        if (waiting.Count > 0)
        {
            processing = waiting.Dequeue();
        }
    }

    protected override bool AddContainer(MonoContainer monoContainer)
    {
        throw new NotImplementedException();
    }
}

public class TrainArea : DeliveryArea<Train>
{
}

public class ShipArea : DeliveryArea<Ship>
{
}

public class TruckArea : DeliveryArea<Truck>
{
}
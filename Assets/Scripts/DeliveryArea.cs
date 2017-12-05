using System;
using System.Collections.Generic;

public class DeliveryArea<T> : Area where T : DeliveryVehicle
{
    /// <summary>
    /// Vehicles waiting outside this area
    /// </summary>
    private Queue<DeliveryVehicle> waiting;

    /// <summary>
    /// A new vehicle just entered this area, put it in the queue
    /// </summary>
    /// <param name="vehicle"></param>
    void OnVehicleEnter(DeliveryVehicle vehicle)
    {
        waiting.Enqueue(vehicle);
    }

    /// <summary>
    /// Serving a vehicle, 
    /// </summary>
    void OnServingVehicles()
    {
        while (waiting.Count > 0)
        {
            var current = waiting.Dequeue();
            var fetch = current.Outgoing;
            var deliver = current.Incoming;
            
            while (deliver.Count > 0)
            {
                if (!AddContainer(deliver[0]))
                    break;
                deliver.RemoveAt(0);
            }
            current.Incoming = deliver;
            while (fetch.Count > 0)
            {
                if (!manager.Request(this, fetch[0]))
                    break;
                fetch.RemoveAt(0);
            }
            current.Outgoing = fetch;
        }
    }


    public override bool AddContainer(Container container)
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
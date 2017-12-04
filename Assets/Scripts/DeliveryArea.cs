using System;
using System.Collections.Generic;

public class DeliveryArea<DeliveryVehicle> : Area
{
    Queue<DeliveryVehicle> _waiting;
    DeliveryVehicle _vehicle;

    void OnVehicleEnter(DeliveryVehicle vehicle)
    {
    }

    void OnVehicleEmpty()
    {
    }

    void OnVehicleLeft()
    {
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
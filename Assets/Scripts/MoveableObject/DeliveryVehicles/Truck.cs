using System.Collections.Generic;

public class Truck : DeliveryVehicle
{
    public TruckArea area;

    private void Start()
    {
        MOInit(truckSpawnPos, 20, false);

        List<TruckArea> areaList = Game.GetAreasOfType<TruckArea>();

        //TODO:get the first free area
        area = areaList[0];
    }

    private void Update()
    {
        if (!(Game.currentState is OperationState)) return;
        MOMovementUpdate();

        if (isAtDestination || !MOIsAtTheThisPos(areaPos)) return;
        isAtDestination = true;

        area.OnVehicleEnter(this);
    }
}
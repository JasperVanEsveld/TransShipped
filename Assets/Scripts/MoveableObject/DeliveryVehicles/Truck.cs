using System.Collections.Generic;
using UnityEngine;

public class Truck : DeliveryVehicle
{
    public TruckArea area;

    private void Start()
    {
        MOInit(truckSpawnPos, 20, false);

        List<TruckArea> areaList = Game.instance.GetAreasOfType<TruckArea>();

        //TODO:get the first free area
        area = areaList[0];
    }

    private void Update()
    {
        if (!(Game.instance.currentState is OperationState)) return;
        MOMovementUpdate();

        if (isAtDestination || !MOIsAtTheThisPos(areaPos)) return;
        isAtDestination = true;

        area.OnVehicleEnter(this);
    }
}
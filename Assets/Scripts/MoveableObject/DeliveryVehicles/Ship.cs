using System.Collections.Generic;
using UnityEngine;

public class Ship : DeliveryVehicle
{
    public ShipArea area;

    private void Start()
    {
        MOInit(shipSpawnPos, 20, true);

        List<ShipArea> areaList = Game.instance.GetAreasOfType<ShipArea>();

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
using System.Collections.Generic;
using UnityEngine;

public class Train : DeliveryVehicle
{
    public TrainArea area;

    private void Start()
    {
        MOInit(trainSpawnPos, 20, false);

        List<TrainArea> areaList = Game.instance.GetAreasOfType<TrainArea>();

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
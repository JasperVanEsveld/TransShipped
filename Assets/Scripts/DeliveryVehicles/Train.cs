using System.Collections.Generic;
using UnityEngine;

public class Train : DeliveryVehicle
{
    public TrainArea area;

    private void Start()
    {
        MOInit(new Vector3(65, 0, 35), 20, true);

        List<TrainArea> areaList = GameObject.Find("Game").GetComponent<Game>().GetAreasOfType<TrainArea>();

        //TODO:get the first free area
        area = areaList[0];
    }

    private void Update()
    {
        if (!(game.currentState is OperationState)) return;
        MOMovementUpdate();
        
        if(isAtDestination || !MOIsAtTheThisPos(areaPos)) return;
        isAtDestination = true;
        
        area.OnVehicleEnter(this);
    }
}
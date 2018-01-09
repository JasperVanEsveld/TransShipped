using System.Collections.Generic;
using UnityEngine;

public class Train : DeliveryVehicle
{
    public Railway area;

    private void Start()
    {
        MOInit(new Vector3(-5, 0, 35), 20, false);

        List<Railway> areaList = GameObject.Find("Game").GetComponent<Game>().GetAreasOfType<Railway>();

        //TODO:get the first free area
        area = areaList[0];
    }

    private void Update()
    {
        if (!(game.currentState is OperationState)) return;
        MOMovementUpdate();

        if (isAtDestination || !MOIsAtTheThisPos(areaPos)) return;
        isAtDestination = true;

        area.OnVehicleEnter(this);
    }
}
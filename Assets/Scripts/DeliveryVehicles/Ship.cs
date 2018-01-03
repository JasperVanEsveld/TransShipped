using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : DeliveryVehicle
{
    public ShipArea area;
    private bool isAtDestination = false;


    // Use this for initialization
    private void Start()
    {
        MOInit(new Vector3(65.0f, 0.0f, 35.0f), 20.0f, true);

        GenerateRandomContainers(32, 50);
        List<ShipArea> areaList = GameObject.Find("Game").GetComponent<Game>().GetAreasOfType<ShipArea>();

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
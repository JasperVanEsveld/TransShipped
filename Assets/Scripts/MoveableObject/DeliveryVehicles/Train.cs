using System.Collections.Generic;

public class Train : DeliveryVehicle
{
    public TrainArea area;

    private void Start()
    {
        MOInit(trainSpawnPos, 20, false);

        List<TrainArea> areaList = Game.GetAreasOfType<TrainArea>();

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
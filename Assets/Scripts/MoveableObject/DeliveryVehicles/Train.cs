using System.Collections.Generic;

public class Train : DeliveryVehicle
{
    public TrainArea area;

    public override void LeaveTerminal() {
        MOPushDestination(trainSpawnPos);
    }

    public override void OnSelected()
    {
        List<TrainArea> areas = Game.OnlyHighlight<TrainArea>();
        foreach(TrainArea currentArea in areas){
            if( currentArea.occupied) {
                currentArea.Highlight(false);
            }
        }
    }

    private void Start()
    {
        MOInit(trainSpawnPos, 20, false);

        List<TrainArea> areaList = Game.GetAreasOfType<TrainArea>();

        //TODO:get the first free area
        area = areaList[0];
    }

    protected override void DestroyIfDone(){
        if(isAtDestination && MOIsAtTheThisPos(trainSpawnPos)){
            Destroy(this.gameObject);
        }
    }

    protected override void Enter()
    {
        area.OnVehicleEnter(this);
    }
}
using System.Collections.Generic;

public class Ship : DeliveryVehicle
{
    public ShipArea area;

    public override void LeaveTerminal() {
        MOShipLeaveTerminal();
    }

    public override void OnSelected()
    {
        List<ShipArea> areas = Game.OnlyHighlight<ShipArea>();
        foreach(ShipArea currentArea in areas){
            if( currentArea.occupied) {
                currentArea.Highlight(false);
            }
        }
    }

    private void Start()
    {
        MOInit(shipSpawnPos, 20, true);

        List<ShipArea> areaList = Game.GetAreasOfType<ShipArea>();

        //TODO:get the first free area
        area = areaList[0];
    }

    protected override void DestroyIfDone(){
        if(isAtDestination && MOIsAtTheThisPos(shipSpawnPos)){
            Destroy(this.gameObject);
        }
    }

    protected override void Enter()
    {
        area.OnVehicleEnter(this);
    }
}
using System.Collections.Generic;

public class Truck : DeliveryVehicle {
    public TruckArea area;

    public override void LeaveTerminal() {
        MOPushDestination(truckSpawnPos);
    }

    public override void OnSelected() {
        List<TruckArea> areas = Game.OnlyHighlight<TruckArea>();
        if (areas.Count == 1) {
            CommandPanel commandPanel = FindObjectOfType<CommandPanel>();
            if (!areas[0].occupied)
                commandPanel.SetDeliveryArea(areas[0]);

            areas[0].Highlight(false);
            return;
        }

        foreach (TruckArea currentArea in areas)
            if (currentArea.occupied)
                currentArea.Highlight(false);
    }

    private void Start() {
        MOInit(truckSpawnPos, 20, false);

        List<TruckArea> areaList = Game.GetAreasOfType<TruckArea>();

        //TODO:get the first free area
        area = areaList[0];
    }

    protected override void DestroyIfDone() {
        if (isAtDestination && MOIsAtTheThisPos(truckSpawnPos))
            Destroy(gameObject);
    }

    protected override void Enter() {
        area.OnVehicleEnter(this);
    }
}
using System.Collections.Generic;

public class Train : DeliveryVehicle {
    public TrainArea area;

    public override void LeaveTerminal() {
        MOPushDestination(trainSpawnPos);
    }

    public override void OnSelected() {
        List<TrainArea> areas = Game.OnlyHighlight<TrainArea>();
        if (areas.Count == 1) {
            CommandPanel commandPanel = FindObjectOfType<CommandPanel>();
            if (!areas[0].occupied) {
                commandPanel.SetDeliveryArea(areas[0]);
                area = areas[0];
                areaPos = area.transform.position;
            }

            areas[0].Highlight(false);
            return;
        }

        foreach (TrainArea currentArea in areas)
            if (currentArea.occupied)
                currentArea.Highlight(false);
    }

    private void Start() {
        MOInit(trainSpawnPos, 20, false);
    }

    protected override void DestroyIfDone() {
        if (isAtDestination && MOIsAtTheThisPos(trainSpawnPos))
            Destroy(gameObject);
    }

    protected override void Enter() {
        area.OnVehicleEnter(this);
    }
}
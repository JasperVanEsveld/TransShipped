using System.Collections.Generic;
using UnityEngine;

public class Ship : DeliveryVehicle {
    public ShipArea area;

    public override void LeaveTerminal() {
        MOShipLeaveTerminal();
    }

    public override void OnSelected() {
        List<ShipArea> areas = Game.OnlyHighlight<ShipArea>();
        if (areas.Count == 1) {
            CommandPanel commandPanel = FindObjectOfType<CommandPanel>();
            if (!areas[0].occupied)
                commandPanel.SetDeliveryArea(areas[0]);

            areas[0].Highlight(false);
            return;
        }

        foreach (ShipArea currentArea in areas)
            if (currentArea.occupied)
                currentArea.Highlight(false);
    }

    private void Start() {
        MOInit(shipSpawnPos, 20, true);

        List<ShipArea> areaList = Game.GetAreasOfType<ShipArea>();

        //TODO:get the first free area
        area = areaList[0];
    }

    protected override void DestroyIfDone() {
        if (isAtDestination && MOIsAtTheThisPos(shipSpawnPos))
            Destroy(gameObject);
    }

    protected override void Enter() {
        area.OnVehicleEnter(this);
    }

    private void Update() {
        DestroyIfDone();
        if (!(Game.currentState is OperationState)) return;
        MOMovementUpdate();
        for (var i = 0; i < carrying.Count; i++) {
            var n = i % 9;
            carrying[i].transform.position = new Vector3(
                (float) (transform.position.x - 1.4 + n / 3 * 2),
                (float) (transform.position.y + i / 9 + 2.6),
                transform.position.z - 1 + i % 3
            );
        }

        if (isAtDestination || !MOIsAtTheThisPos(areaPos)) return;
        isAtDestination = true;

        Enter();
    }
}
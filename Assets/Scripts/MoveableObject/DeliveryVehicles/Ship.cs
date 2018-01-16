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
            if (!areas[0].occupied) {
                commandPanel.SetDeliveryArea(areas[0]);
                area = areas[0];
                areaPos = area.transform.position;
            }

            areas[0].Highlight(false);
            return;
        }

        foreach (ShipArea currentArea in areas)
            if (currentArea.occupied)
                currentArea.Highlight(false);
    }

    private void Start() {
        MOInit(shipSpawnPos, 20, true);
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
        for (int i = 0; i < carrying.Count; i++) {
            int n = i % 9;
            carrying[i].transform.position = new Vector3(
                transform.position.x - 1.4f + n / 3 * 2,
                transform.position.y + (int) (i / 9f) + 2.6f,
                transform.position.z - 1 + i % 3
            );
        }

        if (isAtDestination || !MOIsAtTheThisPos(areaPos)) return;
        isAtDestination = true;

        Enter();
    }
}
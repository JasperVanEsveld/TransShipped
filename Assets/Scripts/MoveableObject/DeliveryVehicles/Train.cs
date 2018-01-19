using System.Collections.Generic;
using UnityEngine;

public class Train : DeliveryVehicle {
    public TrainArea area;

    public override void LeaveTerminal() {
        MOPushDestination(trainSpawnPos);
    }

    private void Awake() {
        if (!Game.instance.trains.Contains(this)) Game.instance.trains.Add(this);
    }

    public void OnSelected() {
        List<TrainArea> areas = Game.instance.OnlyHighlight<TrainArea>();
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
        if (isAtDestination && MOIsAtTheThisPos(trainSpawnPos)) Destroy(gameObject);
    }

    protected override void Enter() {
        area.OnVehicleEnter(this);
    }

    private void Update() {
        DestroyIfDone();
        if (!(Game.instance.currentState is OperationState)) return;
        MOMovementUpdate();
        for (int i = 0; i < carrying.Count; i++) {
            int j = i/2;
            carrying[i].transform.position = new Vector3(transform.position.x -  i * 2 - j / 4f +0.11f,
                                                         transform.position.y + 0.8f,
                                                         transform.position.z);
        }

        if (isAtDestination || !MOIsAtTheThisPos(areaPos)) return;
        isAtDestination = true;

        Enter();
    }
}
using System;
using Object = UnityEngine.Object;
using UnityEngine;

public class OperationState : GameState
{
    public delegate void OperationStateListener();
    public static event OperationStateListener vehicleGeneratedEvent;
    public ContainerManager manager;
    public VehicleGenerator generator;
    public DateTime startTime = DateTime.Now;
    private DateTime lastTime;

    public OperationState() : base() {
        manager = new ContainerManager(Game.instance.GetAreasOfType<Stack>(), this);
        generator = new VehicleGenerator();
        Game.instance.ForceRemoveHighlights();

        foreach(Ship vehicle in Game.instance.ships){
            GameObject.Destroy(vehicle.gameObject);
        }
        Game.instance.ships.Clear();
        foreach(Train vehicle in Game.instance.trains){
            GameObject.Destroy(vehicle.gameObject);
        }
        Game.instance.trains.Clear();
        foreach(Truck vehicle in Game.instance.trucks){
            GameObject.Destroy(vehicle.gameObject);
        }
        Game.instance.trucks.Clear();
    }

    public void OnMovementComplete()
    {
        Game.instance.movements += 1;
    }

    public override void Update()
    {
        if (Game.instance.currentStage.duration < (DateTime.Now - startTime).TotalSeconds)
            Game.instance.ChangeState(new StageEndState());

        int vehicles = Object.FindObjectsOfType<DeliveryVehicle>().Length;
        if (!((DateTime.Now - lastTime).TotalSeconds >= Game.instance.currentStage.spawnInterval) ||
            !(vehicles < Game.instance.currentStage.maxVehicles)) return;
        generator.GenerateRandomVehicle();
        if(vehicleGeneratedEvent != null){
            vehicleGeneratedEvent.Invoke();
        }
        lastTime = DateTime.Now;
    }
}
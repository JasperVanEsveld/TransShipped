using System;
using Object = UnityEngine.Object;

public class OperationState : GameState
{
    public delegate void OperationStateListener();
    public static event OperationStateListener vehicleGeneratedEvent;
    public ContainerManager manager;
    public VehicleGenerator generator;
    public DateTime startTime = DateTime.Now;
    private DateTime lastTime;

    public OperationState()
    {
        manager = new ContainerManager(Game.GetAreasOfType<Stack>(), this);
        generator = new VehicleGenerator();
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
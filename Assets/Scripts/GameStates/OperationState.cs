using System;
using Object = UnityEngine.Object;

public class OperationState : GameState
{
    public ContainerManager manager;
    public VehicleGenerator generator;
    public DateTime startTime = DateTime.Now;
    private DateTime lastTime;

    public OperationState() : base()
    {
        manager = new ContainerManager(Game.instance.GetAreasOfType<Stack>(), this);
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
        lastTime = DateTime.Now;
    }
}
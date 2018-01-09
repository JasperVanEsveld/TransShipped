using System;

public class OperationState : GameState
{
    public ContainerManager manager;
    public VehicleGenerator generator;
    public DateTime startTime = DateTime.Now;
    private DateTime lastTime;

    public OperationState(Game game) : base(game)
    {
        manager = new ContainerManager(game.GetAreasOfType<Stack>(), this);
        generator = new VehicleGenerator(game);
    }

    public void OnMovementComplete()
    {
        game.movements += 1;
    }

    public override void Update()
    {
        if (game.currentStage.duration < (DateTime.Now - startTime).TotalSeconds)
            game.ChangeState(new StageEndState(game));

        int vehicles = Game.FindObjectsOfType<DeliveryVehicle>().Length;
        if (!((DateTime.Now - lastTime).TotalSeconds >= game.currentStage.spawnInterval) ||
            !(vehicles < game.currentStage.maxVehicles)) return;
        lastTime = DateTime.Now;
        switch (new Random().Next(0, 1))
        {
            case 0:
                generator.GenerateVehicle<Truck>(VehicleType.Truck);
                break;
            case 1:
                generator.GenerateVehicle<Ship>(VehicleType.Ship);
                break;
            default:
                generator.GenerateVehicle<Train>(VehicleType.Train);
                break;
        }
    }
}
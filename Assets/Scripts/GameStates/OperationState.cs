using System;

public class OperationState : GameState
{
    public ContainerManager manager;
    public DateTime startTime = DateTime.Now;

    public OperationState(Game game) : base(game)
    {
        manager = new ContainerManager(game.GetAreasOfType<Stack>(), this);
    }

    public void OnMovementComplete()
    {
        game.movements += 1;
    }

    public override void Update() {
        if(this.game.currentStage.duration < DateTime.Now.Subtract(startTime).Seconds){
            game.ChangeState(new StageEndState(game));
        }
    }
}
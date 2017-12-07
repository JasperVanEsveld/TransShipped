public class OperationState : GameState
{
    public ContainerManager manager;

    public OperationState(Game game) : base(game)
    {
        manager = new ContainerManager(game.GetAreasOfType<Stack>(), this);
    }

    public void OnMovementComplete()
    {
        game.movements += 1;
    }
}
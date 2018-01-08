public abstract class GameState
{
    public Game game;

    protected GameState(Game game)
    {
        this.game = game;
        Game.print("Current state: " + this);
    }

    public virtual void Update() {

    }
}
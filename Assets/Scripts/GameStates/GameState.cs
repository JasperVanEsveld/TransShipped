using UnityEngine;

public abstract class GameState
{
    public Game game;

    protected GameState(Game game)
    {
        this.game = game;
        MonoBehaviour.print("Current state: " + this);
    }

    public virtual void Update() {

    }
}
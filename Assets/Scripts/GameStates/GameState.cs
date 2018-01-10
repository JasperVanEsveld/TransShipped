using UnityEngine;

public abstract class GameState
{
    protected GameState()
    {
        MonoBehaviour.print("Current state: " + this);
    }

    public virtual void Update() {

    }
}
using UnityEngine;

public class StageEndState : GameState
{
    public StageEndState(Game game) : base(game)
    {
        MonoBehaviour.print(game.stage.IsSuccess(this) ? "Stage passed" : "Stage failed");
    }
}
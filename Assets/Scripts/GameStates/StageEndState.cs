using UnityEngine;

public class StageEndState : GameState
{
    public StageEndState()
    {
        MonoBehaviour.print(Game.currentStage.IsSuccess(this) ? "Stage passed" : "Stage failed");
        if (Game.currentStage.IsSuccess(this))
            Game.money += Game.currentStage.reward;
        else
            Game.money += Game.currentStage.penalty;
    }
}
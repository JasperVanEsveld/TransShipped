using UnityEngine;

public class StageEndState : GameState
{
    public StageEndState()
    {
        MonoBehaviour.print(Game.instance.currentStage.IsSuccess(this) ? "Stage passed" : "Stage failed");
        if (Game.instance.currentStage.IsSuccess(this))
            Game.money += Game.instance.currentStage.reward;
        else
            Game.money += Game.instance.currentStage.penalty;
    }
}
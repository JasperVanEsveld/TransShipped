using UnityEngine;

public class StageEndState : GameState
{
    public delegate void StageEndListener();
    public static event StageEndListener StageComplete;

    public StageEndState()
    {
        if(StageComplete != null){
            StageComplete.Invoke();
        }
        MonoBehaviour.print(Game.instance.currentStage.IsSuccess(this) ? "Stage passed" : "Stage failed");
        if (Game.instance.currentStage.IsSuccess(this))
            Game.money += Game.instance.currentStage.reward;
        else
            Game.money += Game.instance.currentStage.penalty;
    }
}
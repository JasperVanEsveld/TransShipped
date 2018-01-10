using UnityEngine;

public class StageEndState : GameState
{
    public StageEndState() : base()
    {
        MonoBehaviour.print(Game.instance.currentStage.IsSuccess(this) ? "Stage passed" : "Stage failed");
        if(Game.instance.currentStage.IsSuccess(this)){
            Game.instance.SetMoney(Game.instance.money + Game.instance.currentStage.reward);
        } else{
            Game.instance.SetMoney(Game.instance.money + Game.instance.currentStage.penalty);
        }
    }
}
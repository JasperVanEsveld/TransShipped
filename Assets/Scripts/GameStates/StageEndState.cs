using UnityEngine;

public class StageEndState : GameState
{
    public StageEndState(Game game) : base(game)
    {
        MonoBehaviour.print(game.currentStage.IsSuccess(this) ? "Stage passed" : "Stage failed");
        if(game.currentStage.IsSuccess(this)){
            game.money += game.currentStage.reward;
            if(game.stages.Peek() != null){
                game.currentStage = game.stages.Dequeue();
            }
            else{
                game.currentState = new LevelEndState(game);
            }
        } else{
            game.money += game.currentStage.penalty;

        }
    }
}
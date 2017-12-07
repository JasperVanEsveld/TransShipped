public class StageEndState : GameState
{
    public StageEndState(Game game) : base(game) {
        if(game.stage.IsSuccess(this)){
            Game.print("Stage passed");
        } else{
            Game.print("Stage failed");
        }
    }
}
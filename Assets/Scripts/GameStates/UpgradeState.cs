public class UpgradeState : GameState {

    public UpgradeState(Game game) : base(game){}

    public bool Buy(double price){
        if(this.game.money - price > 0){
            this.game.money -= price;
            return true;
        }
        return false;
    }
}

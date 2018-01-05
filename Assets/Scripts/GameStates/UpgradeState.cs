public class UpgradeState : GameState
{
    public UpgradeState(Game game) : base(game)
    {
    }

    public bool Buy(double price)
    {
        if (!(game.money - price >= 0)) return false;
        game.SetMoney(game.money - price);
        return true;
    }

    public override void Update()
    {
    }
}
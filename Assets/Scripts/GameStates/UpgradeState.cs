public class UpgradeState : GameState
{
    public UpgradeState() : base()
    {
    }

    public bool Buy(double price)
    {
        if (!(Game.instance.money - price >= 0)) return false;
        Game.instance.SetMoney(Game.instance.money - price);
        return true;
    }

    public override void Update() {
    }
}
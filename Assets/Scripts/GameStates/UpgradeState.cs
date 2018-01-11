public class UpgradeState : GameState
{
    public static bool Buy(double price)
    {
        if (Game.money < price) return false;
        Game.money -= price;
        return true;
    }

    public override void Update()
    {
    }
}
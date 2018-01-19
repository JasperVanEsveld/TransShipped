using UnityEngine;

public class UpgradeState : GameState
{
    public static bool Buy(double price)
    {
        if (Game.instance.money < price) return false;
        Game.instance.money -= price;
        return true;
    }

    public override void Update()
    {
    }
}
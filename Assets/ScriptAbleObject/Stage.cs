using UnityEngine;

[CreateAssetMenu(menuName = "Stage")]
public class Stage : ScriptableObject
{
    public double duration;
    public double moneyRequired;
    public int movementsRequired;
    public double reward;
    public double penalty;

    public bool IsSuccess(GameState state)
    {
        return state.game.money > moneyRequired && state.game.movements > movementsRequired;
    }
}
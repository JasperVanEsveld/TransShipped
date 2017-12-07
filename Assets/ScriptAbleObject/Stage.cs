using UnityEngine;
using System.Collections;
using UnityEditor;

[CreateAssetMenu( menuName = "Stage")]
public class Stage : ScriptableObject{
    public double moneyRequired;
    public int movementsRequired;
    public double reward;
	public double penalty;

    public bool IsSuccess(GameState state){
        if(state.game.money > moneyRequired && state.game.movements > movementsRequired){
            return true;
        }
        return false;
    }
}

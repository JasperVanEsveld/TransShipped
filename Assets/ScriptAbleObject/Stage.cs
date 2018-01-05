using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(menuName = "Stage")]
public class Stage : ScriptableObject
{
    public double duration;
    public double moneyRequired;
    public int movementsRequired;
    public double reward;
    public double penalty;
    public List<VehicleTemplate> vehicleTemplates;
    public double spawnInterval;
    public double maxVehicles;

    public VehicleTemplate GetTemplate(VehicleType type){
        List<VehicleTemplate> templates =vehicleTemplates.FindAll(template => template.type == type);
        var rnd = new Random();
        int randomSelection = rnd.Next(0, templates.Count - 1);


        return templates[randomSelection];
    }
    public bool IsSuccess(GameState state)
    {
        return state.game.money >= moneyRequired && state.game.movements >= movementsRequired;
    }
}
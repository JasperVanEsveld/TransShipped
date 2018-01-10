using UnityEngine;

public class GenerateShip : MonoBehaviour
{
    private VehicleGenerator generator;
    private int shipCount_;

    public void Start(){
        generator = Game.instance.GetGenerator();
    }

    public void generateShip()
    {
        if (shipCount_ == 5) return;
        int i = shipCount_;
        Ship obj = generator.GenerateVehicle<Ship>(VehicleType.Ship);
        obj.name = "Ship_" + i;
        ++shipCount_;
    }
}

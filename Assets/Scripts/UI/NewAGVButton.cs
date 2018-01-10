using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewAGVButton : MonoBehaviour
{
    private Road road;
    private Game game;
    private int i;

    private void Start()
    {
        game = (Game) FindObjectOfType(typeof(Game));
        road = FindObjectOfType<Road>();
    }

    public void AddVehicle()
    {
        if (((UpgradeState) game.currentState).Buy(Vehicle.PurchaseCost()))
        {
            GameObject v = Instantiate(GameObject.Find("Vehicle"));
            road.vehicles.Add(v.GetComponent(typeof(Vehicle)) as Vehicle);
        }
        else if (i == 0)
            print("You don't have enough money");

        i++;
    }

    public void UpgradeAGV()
    {
        var ret = Vehicle.Upgrade();
        switch (ret)
        {
            case -1:
                print("You have already reached the maximum level");
                break;
            case 0:
                print("You don't have enough money");
                break;
            default:
                print("All AGVs now have been upgraded into level " + ret);
                break;
        }
    }
}
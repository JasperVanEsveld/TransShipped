using UnityEngine;
using Random = System.Random;

public class VehicleGenerator
{
    private Game game;

    public VehicleGenerator(Game game)
    {
        this.game = game;
    }

    public T GenerateVehicle<T>(VehicleType type) where T : DeliveryVehicle
    {
        VehicleTemplate template = game.currentStage.GetTemplate(type);
        Transform transform = Game.Instantiate(template.prefab, template.spawnPosition, template.spawnRotation);
        T vehicle = transform.gameObject.AddComponent<T>();
        ApplyTemplate(vehicle, template);
        return vehicle;
    }

    private void ApplyTemplate(DeliveryVehicle vehicle, VehicleTemplate template)
    {
        GenerateCarrying(vehicle, template);
        CreateRequestList(vehicle, template);
        vehicle.reward = template.reward;
        vehicle.timeOutTime = template.timeOutTime;
    }

    private static void GenerateCarrying(DeliveryVehicle vehicle, VehicleTemplate template)
    {
        Random rnd = new Random();
        int conCount = rnd.Next(template.carryMin, template.carryMax);
        for (int i = 0; i < conCount; ++i)
        {
            GameObject tempGO;
            MonoContainer tempMC;
            switch (rnd.Next(0, 3))
            {
                case 0:
                    tempGO = Game.Instantiate(Resources.Load("Containers/BlueContainer") as GameObject,
                        vehicle.transform.position,
                        vehicle.transform.rotation);
                    tempMC = tempGO.GetComponent<MonoContainer>();
                    tempMC.container = new Container(containerType.ShipContainer);
                    break;
                case 1:
                    tempGO = Game.Instantiate(Resources.Load("Containers/RedContainer") as GameObject,
                        vehicle.transform.position,
                        vehicle.transform.rotation);
                    tempMC = tempGO.GetComponent<MonoContainer>();
                    tempMC.container = new Container(containerType.TruckContainer);
                    break;
                default:
                    tempGO = Game.Instantiate(Resources.Load("Containers/GreenContainer") as GameObject,
                        vehicle.transform.position,
                        vehicle.transform.rotation);
                    tempMC = tempGO.GetComponent<MonoContainer>();
                    tempMC.container = new Container(containerType.TrainContainer);
                    break;
            }

            tempGO.transform.SetParent(vehicle.transform);
            tempMC.movement = null;
            tempMC.gameObject.transform.SetParent(vehicle.transform);
            vehicle.carrying.Add(tempMC);
        }
    }

    private static void CreateRequestList(DeliveryVehicle vehicle, VehicleTemplate template)
    {
        int conCount = new Random().Next(template.requestMin, template.requestMax);
        containerType type;
        switch (template.type)
        {
            case VehicleType.Ship:
                type = containerType.ShipContainer;
                break;
            case VehicleType.Truck:
                type = containerType.TruckContainer;
                break;
            default:
                type = containerType.TrainContainer;
                break;
        }

        for (int i = 0; i < conCount; ++i)
            vehicle.outgoing.Add(new Container(type));
    }
}
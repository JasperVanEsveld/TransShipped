using UnityEngine;
using Random = System.Random;

public class VehicleGenerator
{
    private static Random rnd = new Random();

    private int currentType = 0;

    public DeliveryVehicle GenerateRandomVehicle()
    {
        if(currentType >= Game.currentStage.vehicleTemplates.Count){
            currentType = 0;
        }
        VehicleTemplate template = Game.currentStage.vehicleTemplates[currentType];
        DeliveryVehicle vehicle = null;
        switch (template.type)
        {
            case VehicleType.Truck:
                vehicle = GenerateVehicle<Truck>(VehicleType.Truck);
                break;
            case VehicleType.Ship:
                vehicle = GenerateVehicle<Ship>(VehicleType.Ship);
                break;
            case VehicleType.Train:
                vehicle = GenerateVehicle<Train>(VehicleType.Train);
                break;
        }
        currentType++;
        return vehicle;
    }

    public T GenerateVehicle<T>(VehicleType type) where T : DeliveryVehicle
    {
        VehicleTemplate template = Game.currentStage.GetTemplate(type);
        Transform transform = Object.Instantiate(template.prefab, template.spawnPosition, template.spawnRotation);
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

    private static void CreateRequestList(DeliveryVehicle vehicle, VehicleTemplate template)
    {
        int conCount = rnd.Next(template.requestMin, template.requestMax);
        for (int i = 0; i < conCount; ++i)
        {
            int typeIndex = rnd.Next(0, template.requestTypes.Count - 1);
            Container cont = new Container(template.requestTypes[typeIndex]);
            vehicle.outgoing.Add(cont);
        }
    }

    private static void GenerateCarrying(DeliveryVehicle vehicle, VehicleTemplate template)
    {
        int conCount = rnd.Next(template.carryMin, template.carryMax);
        for (int i = 0; i < conCount; ++i)
        {
            GameObject tempGO = null;
            MonoContainer tempMC = null;
            int maxTypes = template.carryingTypes.Count;
            int index = rnd.Next(0, maxTypes);
            
            switch (template.carryingTypes[index])
            {
                case containerType.ShipContainer:
                    tempGO = Object.Instantiate(Resources.Load("Containers/BlueContainer") as GameObject);
                    tempMC = tempGO.GetComponent<MonoContainer>();
                    tempMC.container = new Container(containerType.ShipContainer);
                    break;
                case containerType.TruckContainer:
                    tempGO = Object.Instantiate(Resources.Load("Containers/RedContainer") as GameObject);
                    tempMC = tempGO.GetComponent<MonoContainer>();
                    tempMC.container = new Container(containerType.TruckContainer);
                    break;
                case containerType.TrainContainer:
                    tempGO = Object.Instantiate(Resources.Load("Containers/GreenContainer") as GameObject);
                    tempMC = tempGO.GetComponent<MonoContainer>();
                    tempMC.container = new Container(containerType.TrainContainer);
                    break;
            }
            tempGO.transform.position = vehicle.transform.position;
            tempGO.transform.SetParent(vehicle.transform);
            tempMC.movement = null;
            tempMC.gameObject.transform.SetParent(vehicle.transform);
            vehicle.carrying.Add(tempMC);
        }
    }
}
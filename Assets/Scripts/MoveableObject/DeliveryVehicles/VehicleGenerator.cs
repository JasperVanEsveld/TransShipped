using UnityEngine;

public class VehicleGenerator {
    public DeliveryVehicle GenerateRandomVehicle()
    {
        int i = new System.Random().Next(0, Game.instance.currentStage.vehicleTemplates.Count);
        VehicleTemplate template = Game.instance.currentStage.vehicleTemplates[i];
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

        return vehicle;
    }

    public T GenerateVehicle<T>(VehicleType type) where T : DeliveryVehicle
    {
        VehicleTemplate template = Game.instance.currentStage.GetTemplate(type);
        Transform transform = Object.Instantiate(template.prefab, template.spawnPosition, template.spawnRotation);
        T vehicle = transform.gameObject.AddComponent<T>();
        ApplyTemplate(vehicle, template);
        return vehicle;
    }

    private void ApplyTemplate(DeliveryVehicle vehicle, VehicleTemplate template){
        GenerateCarrying(vehicle, template);
        CreateRequestList(vehicle, template);
        vehicle.reward = template.reward;
        vehicle.timeOutTime = template.timeOutTime;
    }

    private static void CreateRequestList(DeliveryVehicle vehicle, VehicleTemplate template){
        System.Random rnd = new System.Random();
        int conCount = rnd.Next(template.requestMin, template.requestMax);
        for (int i = 0; i < conCount; ++i) {
            int typeIndex = rnd.Next(0, template.containerTypes.Count-1);
            Container cont = new Container(template.containerTypes[typeIndex]);
            vehicle.outgoing.Add(cont);
        }
    }
    
    private static void GenerateCarrying(DeliveryVehicle vehicle, VehicleTemplate template)
    {
        System.Random rnd = new System.Random();
        int conCount = rnd.Next(template.carryMin, template.carryMax);
        for (int i = 0; i < conCount; ++i)
        {
            GameObject tempGO;
            MonoContainer tempMC;
            switch (rnd.Next(0, 3))
            {
                case 0:
                    tempGO = Object.Instantiate(Resources.Load("Containers/BlueContainer") as GameObject,
                        vehicle.transform.position,
                        vehicle.transform.rotation);
                    tempMC = tempGO.GetComponent<MonoContainer>();
                    tempMC.container = new Container(containerType.ShipContainer);
                    break;
                case 1:
                    tempGO = Object.Instantiate(Resources.Load("Containers/RedContainer") as GameObject,
                        vehicle.transform.position,
                        vehicle.transform.rotation);
                    tempMC = tempGO.GetComponent<MonoContainer>();
                    tempMC.container = new Container(containerType.TruckContainer);
                    break;
                default:
                    tempGO = Object.Instantiate(Resources.Load("Containers/GreenContainer") as GameObject,
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
}
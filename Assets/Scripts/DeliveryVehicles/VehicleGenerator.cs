using UnityEngine;

public class VehicleGenerator {
    private Game game;

    public VehicleGenerator(Game game){
        this.game = game;
    }

    public T GenerateVehicle<T>(VehicleType type) where T : DeliveryVehicle {
        VehicleTemplate template = game.currentStage.GetTemplate(type);
        Transform transform = Game.Instantiate(template.prefab, template.spawnPosition, template.spawnRotation);
        T vehicle = transform.gameObject.AddComponent<T>();
        ApplyTemplate(vehicle, template);
        return vehicle;
    }

    private void ApplyTemplate(DeliveryVehicle vehicle, VehicleTemplate template){
        GenerateCarrying(vehicle, template);
        CreateRequestList(vehicle, template);
    }
    private void CreateRequestList(DeliveryVehicle vehicle, VehicleTemplate template){
        System.Random rnd = new System.Random();
        int conCount = rnd.Next(template.requestMin, template.requestMax);
        for (int i = 0; i < conCount; ++i) {
            int typeIndex = rnd.Next(0, template.containerTypes.Count-1);
            Container cont = new Container(template.containerTypes[typeIndex]);
            vehicle.outgoing.Add(cont);
        }
    }
    
    private void GenerateCarrying(DeliveryVehicle vehicle, VehicleTemplate template){
        System.Random rnd = new System.Random();
        int conCount = rnd.Next(template.carryMin, template.carryMax);
        for (int i = 0; i < conCount; ++i)
        {
            GameObject tempGO;
            MonoContainer tempMC;
            switch (rnd.Next(0, 3))
            {
                case 0:
                    tempGO = Game.Instantiate(Resources.Load("Containers/Container_Blue") as GameObject, vehicle.transform.position,
                        vehicle.transform.rotation);
                    tempMC = tempGO.GetComponent<MonoContainer>();
                    tempMC.container = new Container(containerType.Blue);
                    break;
                case 1:
                    tempGO =Game. Instantiate(Resources.Load("Containers/Container_Red") as GameObject, vehicle.transform.position,
                        vehicle.transform.rotation);
                    tempMC = tempGO.GetComponent<MonoContainer>();
                    tempMC.container = new Container(containerType.Red);
                    break;
                default:
                    tempGO = Game.Instantiate(Resources.Load("Containers/Container_Green") as GameObject, vehicle.transform.position,
                        vehicle.transform.rotation);
                    tempMC = tempGO.GetComponent<MonoContainer>();
                    tempMC.container = new Container(containerType.Green);
                    break;
            }
            tempGO.transform.SetParent(vehicle.transform);
            tempMC.movement = null;
            tempMC.gameObject.transform.SetParent(vehicle.transform);
            vehicle.carrying.Add(tempMC);
        }
    }
}
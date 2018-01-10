using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Road : ContainerProcessor
{
    private readonly Dictionary<MonoContainer, Vehicle> containerVehicle = new Dictionary<MonoContainer, Vehicle>();
    public List<Vehicle> vehicles = new List<Vehicle>();

    private Vehicle FindAvailableVehicle() {
        return vehicles.FirstOrDefault(vehicle => vehicle.IsAvailable());
    }

    private Vehicle FindAvailableVehicle(Area origin) {
        return vehicles.FirstOrDefault(vehicle => vehicle.IsAvailable(origin));
    }

    private Vehicle FindAvailableReadyVehicle(Area origin)
    {
        return vehicles.FirstOrDefault(vehicle => vehicle.IsAvailable(origin) && vehicle.MOIsAtTheThisPos(origin.transform.position));
    }

    private Vehicle FindShortedQueueVehicle()
    {
        int min = int.MaxValue, minIndex;
        minIndex = 0;
        for (var i = 0; i < vehicles.Count; i++)
        {
            if (vehicles[i].request.Count <= min)
            {
                minIndex = i;
                min = vehicles[i].request.Count;
            }
        }
        return vehicles[minIndex];
    }

    public override bool AddContainer(MonoContainer monoContainer)
    {
        Vehicle vehicle = FindAvailableReadyVehicle(monoContainer.movement.originArea);
        Area next = game.GetManager().GetNextArea(this,monoContainer.movement);
        if (vehicle != null && next.ReserveArea(this)) 
        {
            vehicle.GoTo(monoContainer.movement.originArea);
            containerVehicle.Add(monoContainer, vehicle);
            return vehicle.AddContainer(monoContainer);
        }
        return false;
    }

    protected override void RemoveContainer(MonoContainer monoContainer)
    {
        containerVehicle[monoContainer].RemoveContainer(monoContainer);
        containerVehicle.Remove(monoContainer);
    }

    public override bool ReserveArea(Area origin) {
        Vehicle vehicle = FindAvailableVehicle();
        bool reserved = false;
        if(vehicle != null){
            reserved = vehicle.ReserveVehicle(origin);
            vehicle.request.Enqueue(origin);
            print("This area wants to reserve a vehicle: " + origin + "\n" + "Result was: " + reserved);
            return reserved;
        }
        return false;
    }
}
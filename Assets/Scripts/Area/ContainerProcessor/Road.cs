using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Road : ContainerProcessor
{
    private readonly Dictionary<MonoContainer, Vehicle> containerVehicle = new Dictionary<MonoContainer, Vehicle>();
    public List<Vehicle> vehicles = new List<Vehicle>();

    private Vehicle FindAvailableVehicle()
    {
        return vehicles.FirstOrDefault(vehicle => !vehicle.isMoving && !vehicle.IsFull());
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
            }
        }
        return vehicles[minIndex];
    }

    public override bool AddContainer(MonoContainer monoContainer)
    {
        Vehicle vehicle = FindAvailableVehicle();
        if (vehicle != null)
        {
            vehicle.GoTo(monoContainer.movement.originArea);
            containerVehicle.Add(monoContainer, vehicle);
            Debug.Log("adding containers");
            return vehicle.AddContainer(monoContainer);
        }
        FindShortedQueueVehicle().request.Enqueue(monoContainer.movement.originArea);
        return false;
    }

    protected override void RemoveContainer(MonoContainer monoContainer)
    {
        containerVehicle[monoContainer].containers.Remove(monoContainer);
    }
}
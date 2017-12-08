using System.Collections.Generic;
using System.Linq;

public class Road : ContainerProcessor
{
    Dictionary<MonoContainer, Vehicle> containerVehicle = new Dictionary<MonoContainer, Vehicle>();
    private readonly List<Vehicle> vehicles = new List<Vehicle>();

    private Vehicle FindAvailableVehicle()
    {
        return vehicles.FirstOrDefault(vehicle => !vehicle.isOccupied && !vehicle.IsFull());
    }

    private Vehicle FindShortedQueueVehicle()
    {
        int min = int.MaxValue, minIndex;
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
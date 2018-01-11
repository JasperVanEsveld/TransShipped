using System.Collections.Generic;
using System.Linq;

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

    private Vehicle CompleteReservation(Area reference){
        foreach(Vehicle vehicle in vehicles){
            if (!vehicle.IsReservedBy(reference) || !vehicle.IsAvailable(reference) ||
                !vehicle.MOIsAtTheThisPos(reference.transform.position)) continue;
            vehicle.reserved = false;
            return vehicle;
        }
        return null;
    }

    private Vehicle FindShortedQueueVehicle()
    {
        int min = int.MaxValue;
        int minIndex = 0;
        for (var i = 0; i < vehicles.Count; i++)
        {
            if (vehicles[i].request.Count > min) continue;
            minIndex = i;
            min = vehicles[i].request.Count;
        }
        return vehicles[minIndex];
    }

    public override bool AddContainer(MonoContainer monoContainer)
    {
        Vehicle vehicle = CompleteReservation(monoContainer.movement.originArea);
        if(vehicle != null)
        {
            vehicle.GoTo(monoContainer.movement.originArea);
            containerVehicle.Add(monoContainer, vehicle);
            return vehicle.AddContainer(monoContainer);
        }
        vehicle = FindAvailableReadyVehicle(monoContainer.movement.originArea);
        if (vehicle == null) return false;
        Area next = Game.GetManager().GetNextArea(this,monoContainer.movement);
        if (!next.ReserveArea(this, monoContainer.movement)) return false;
        vehicle.GoTo(monoContainer.movement.originArea);
        containerVehicle.Add(monoContainer, vehicle);
        return vehicle.AddContainer(monoContainer);
    }

    protected override void RemoveContainer(MonoContainer monoContainer)
    {
        containerVehicle[monoContainer].RemoveContainer(monoContainer);
        containerVehicle.Remove(monoContainer);
    }

    public override bool ReserveArea(Area origin, Movement move) {
        Vehicle vehicle = FindAvailableVehicle();
        bool reserved;
        if (vehicle == null || !Game.GetManager().GetNextArea(this, move).ReserveArea(this, move)) return false;
        reserved = vehicle.ReserveVehicle(origin);
        vehicle.request.Enqueue(origin);
        return reserved;
    }
}
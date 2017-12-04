using System.Collections.Generic;

public class ContainerManager
{
    List<DeliveryArea<DeliveryVehicle>> _deliver;
    List<Stack> _stacks;

    void OnMovementComplete(Movement movement)
    {
    }

    void Store(MonoContainer container)
    {
    }

    void Request(Area area, Container container)
    {
    }

    Area GetNextArea(Area area, Movement movement)
    {
        return null;
    }
}
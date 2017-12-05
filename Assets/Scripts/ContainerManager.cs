using System.Collections.Generic;

public class ContainerManager
{
    List<DeliveryArea<DeliveryVehicle>> deliver;
    List<Stack> stacks;

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
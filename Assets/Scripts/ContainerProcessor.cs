public abstract class ContainerProcessor : Area
{
    ProcessorState currentState;
    float bastTime;
    MonoContainer container;

    void Process(MonoContainer container, Area target)
    {
    }

    private void Update()
    {
    }
}

public class Crane : ContainerProcessor
{
    protected override bool AddContainer(MonoContainer monoContainer)
    {
    }
}

public class Vehicle : ContainerProcessor
{
    protected override bool AddContainer(MonoContainer monoContainer)
    {
    }
}
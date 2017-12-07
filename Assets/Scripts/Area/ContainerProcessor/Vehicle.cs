public class Vehicle : ContainerProcessor
{
    protected override void RemoveContainer(MonoContainer monoContainer)
    {
        container = null;
    }
}
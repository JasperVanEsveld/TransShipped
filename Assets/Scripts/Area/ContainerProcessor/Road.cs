public class Road : ContainerProcessor
{
    protected override void RemoveContainer(MonoContainer monoContainer)
    {
        container = null;
    }
}
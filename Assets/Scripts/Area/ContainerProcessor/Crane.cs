public class Crane : ContainerProcessor
{
    protected override void RemoveContainer(MonoContainer monoContainer)
    {
        container =  null;
    }
}
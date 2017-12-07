public class CraneArea : ContainerProcessor
{
    protected override void RemoveContainer(MonoContainer monoContainer)
    {
        container = null;
    }
}
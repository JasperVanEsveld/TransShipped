using System;

public abstract class ContainerProcessor : Area
{
    public float baseTime;
    private DateTime startTime;
    public MonoContainer container;

    public override bool AddContainer(MonoContainer toAddContainer)
    {
        if (container != null) return false;
        container = toAddContainer;
        startTime = DateTime.Now;
        return true;
    }

    private void Update()
    {
        if (container != null && DateTime.Now.Subtract(startTime).Seconds >= baseTime)
        {
            MoveToNext(container);
        }
    }
}
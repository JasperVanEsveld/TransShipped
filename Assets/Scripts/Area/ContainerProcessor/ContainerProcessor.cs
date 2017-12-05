using System;
using System.Timers;

public abstract class ContainerProcessor : Area 
{
    public float baseTime;
    private DateTime startTime;
    protected MonoContainer container;
    System.Timers.Timer baseTimer;

    public override bool AddContainer(MonoContainer toAddContainer)
    {
        if(container == null)
        {
            container = toAddContainer;
            startTime = DateTime.Now;
            return true;
        }
        return false;
    }

    private void Update()
    {
        if(container != null && DateTime.Now.Subtract(startTime).Seconds >= baseTime){
            this.MoveToNext(container);
        }
    }


}
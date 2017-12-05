using System;
using System.Timers;

public abstract class ContainerProcessor : Area 
{
    ProcessorState currentState;
    float baseTime;
    MonoContainer container;
    Area targetArea;
    System.Timers.Timer baseTimer;

    void Process(MonoContainer container, Area target) {
        baseTimer = new System.Timers.Timer();
        baseTimer.Elapsed += new ElapsedEventHandler(MoveAfterTime);
        baseTimer.Interval = baseTime;
        baseTimer.Enabled = true;
    }

    void MoveAfterTime(object source, ElapsedEventArgs e)
    {
        if(source is ContainerProcessor){
            ContainerProcessor cp = (ContainerProcessor)source;
            targetArea.AddContainer(container);
            container = null;
            targetArea = null;
            if(cp.baseTimer != null) cp.baseTimer.Dispose();
        }
    }

    public override bool AddContainer(MonoContainer toAddContainer)
    {
        if(container == null)
        {
            container = toAddContainer;
            return true;
        }
        return false;
    }

    private void Update()
    {
    }


}

public class Crane : ContainerProcessor
{
    // do stuff
}

public class Vehicle : ContainerProcessor
{
    // do stuff
}
public abstract class ContainerProcessor : Area
{
    import System.Timers.Timer;
    ProcessorState currentState;
    float baseTime;
    MonoContainer container;
    public System.Timers.Timer baseTimer;

    void Process(MonoContainer container, Area target) {
        baseTimer = new System.Timers.Timer();
        baseTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        baseTimer.Interval = baseTime;
        baseTimer.Enabled = true;
    }

    void MoveAfterTime(object source, ElapsedEventArgs e)
    {
        if(source.GetType().Equals(this.GetType())){
            ContainerProcessor cp = (ContainerProcessor)source;
            target.AddContainer(container);
            container = null;
            source.baseTimer.Dispose();
        }
    }

    protected override bool AddContainer(MonoContainer toAddContainer)
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
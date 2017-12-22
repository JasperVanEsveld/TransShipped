using System;

public abstract class ContainerProcessor : Area
{
    public float baseTime;
    private DateTime startTime;
    public MonoContainer container;

    public override bool AddContainer(MonoContainer toAddContainer)
    {
        if (container != null){
            if(container.movement.originArea != toAddContainer.movement.originArea  && toAddContainer.movement.originArea is Road ) {
                print("Holding container from: " + container.movement.originArea);
                print("New container from: " + toAddContainer.movement.originArea);
                Area previous = container.movement.originArea;
                container.movement.originArea = this;
                if (!previous.AddContainer(container)) return false;
                container.transform.SetParent(previous.transform);
                container.transform.position = previous.transform.position;
                RemoveContainer(container);
            } else{
                return false;
            }
        }
        print("Received container from: " + toAddContainer.movement.originArea);
        container = toAddContainer;
        startTime = DateTime.Now;
        return true;
    }

    private void Update()
    {
        if (container != null && DateTime.Now.Subtract(startTime).Seconds >= baseTime)
        {
            MoveToNext(container);
        } else if(container == null){
            AreaAvailable();
        }
    }
}
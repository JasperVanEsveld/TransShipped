using System.Collections.Generic;

public class Stack : Area
{
    public List<MonoContainer> containers { private set; get; }

    /// <summary>
    /// The max volume of containers this stack could stack
    /// </summary>
    public int max;

    public new void Start()
    {
        containers = new List<MonoContainer>();
    }

    public int Contains(Container container)
    {
        for (var i = 0; i < containers.Count; i++)
        {
            if (containers[i].container.Equals(container))
            {
                return i;
            }
        }
        return -1;
    }

    public void Update()
    {
        foreach (var cont in containers)
        {
            if (MoveToNext(cont))
            {
                break;
            }
        }
    }

    /// <summary>
    /// Stack a container to the stack<br/>
    /// If max volume has been reached, return false and abort the operation<br/>
    /// Otherwise, add the container to the list and return true
    /// </summary>
    /// <param name="monoContainer">The container to be stacked</param>
    /// <returns>Whether the operation is successful</returns>
    protected override bool AddContainer(MonoContainer monoContainer)
    {
        if (containers.Count >= max) return false;
        containers.Add(monoContainer);
        if (monoContainer.movement.TargetArea == this)
        {
            monoContainer.movement = null;
        }
        return true;
    }

    protected override void RemoveContainer(MonoContainer monoContainer)
    {
        containers.Remove(monoContainer);
    }
}
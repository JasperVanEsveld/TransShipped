using System.Collections.Generic;

public class Stack : Area
{
    /// <summary>
    /// The list of containers stacked in this stack
    /// </summary>
    public List<MonoContainer> containers;

    /// <summary>
    /// The max volume of containers this stack could stack
    /// </summary>
    public int max = 10;

    /// <summary>
    /// When instantiating a stack, its max volume should be decided
    /// </summary>
    /// <param name="max">The max number of containers this stack could stack</param>
    public Stack(int max)
    {
        this.max = max;
        containers = new List<MonoContainer>();
        manager.stacks.Add(this);
    }

    public int Contains(Container container){
        for(int i = 0; i < containers.Count; i++){
            if(containers[i].container.Equals(container)){
                return i;
            }
        }
        return -1;
    }

    public void Update(){
        foreach(MonoContainer cont in containers){
            if(MoveToNext(cont)){
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
    public override bool AddContainer(MonoContainer monoContainer)
    {
        if (containers.Count >= max) return false;
        containers.Add(monoContainer);
        if(monoContainer.movement.TargetArea == this){
            monoContainer.movement = null;
        }
        return true;
    }
    
    protected override void RemoveContainer(MonoContainer monoContainer)
    {
        containers.Remove(monoContainer);
    }
}
using System.Collections.Generic;

public class Stack : Area
{
    /// <summary>
    /// The list of containers stacked in this stack
    /// </summary>
    private readonly List<MonoContainer> _containers;

    /// <summary>
    /// The max volume of containers this stack could stack
    /// </summary>
    private readonly int _max;

    /// <summary>
    /// When instantiating a stack, its max volume should be decided
    /// </summary>
    /// <param name="max">The max number of containers this stack could stack</param>
    public Stack(int max)
    {
        _max = max;
        _containers = new List<MonoContainer>();
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
        if (_containers.Count >= _max) return false;
        _containers.Add(monoContainer);
        return true;
    }
}
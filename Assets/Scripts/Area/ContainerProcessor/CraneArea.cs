using System.Collections.Generic;
using System.Linq;

public class CraneArea : ContainerProcessor
{
    private readonly Dictionary<MonoContainer, Crane> containerCrane = new Dictionary<MonoContainer, Crane>();
    public List<Crane> cranes = new List<Crane>();

    /// <summary>
    /// Find an available crane in this crane area.<br/>
    /// If found, return it, otherwise return null.
    /// </summary>
    /// <returns>Available crane found</returns>
    private Crane FindAvailableCrane()
    {
        return cranes.FirstOrDefault(crane => crane.isAvailable);
    }

    /// <summary>
    /// Call a crane to move the container to its target area.<br/>
    /// Return true is the operation is a success.<br/>
    /// Otherwise return false.
    /// </summary>
    /// <param name="monoContainer">The container to be moved</param>
    /// <returns>Whether the operation is a success</returns>
    public override bool AddContainer(MonoContainer monoContainer)
    {
        var crane = FindAvailableCrane();
        if (crane == null) return false;
        containerCrane.Add(monoContainer, crane);
        return crane.AddContainer(monoContainer);
    }

    protected override void RemoveContainer(MonoContainer monoContainer)
    {
        containerCrane[monoContainer].container = null;
        containerCrane.Remove(monoContainer);
    }
}
using System.Collections.Generic;

public class CraneArea : Area
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
        foreach (var crane in cranes)
            if (crane.IsAvailable())
                return crane;
        return null;
    }

    /// <summary>
    /// Find an available crane in this crane area.<br/>
    /// If found, return it, otherwise return null.
    /// </summary>
    /// <returns>Available crane found</returns>
    private Crane FindAvailableCrane(Area origin)
    {
        foreach (var crane in cranes)
            if (crane.IsAvailable(origin))
                return crane;
        return null;
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
        var crane = FindAvailableCrane(monoContainer.movement.originArea);
        Area next = ContainerManager.GetNextArea(this,monoContainer.movement);
        if (crane == null || !next.ReserveArea(this)) return false;
        containerCrane.Add(monoContainer, crane);
        return crane.AddContainer(monoContainer);
    }

    public override void RemoveContainer(MonoContainer monoContainer)
    {
        containerCrane[monoContainer].container = null;
        containerCrane.Remove(monoContainer);
    }

    public override bool ReserveArea(Area origin) {
        Crane crane = FindAvailableCrane();
        if(crane != null){
            bool reserved = crane.ReserveCrane(origin);
            print("This area wants to reserve a crane: " + origin + "\n" + "Result was: " + reserved);
            return reserved;
        }
        return false;
    }
}
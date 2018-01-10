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
            if (!crane.reserved)
                return crane;
        return null;
    }

    private Crane CompleteReservation(Area reference){
        foreach(Crane crane in cranes){
            if(crane.IsReservedBy(reference) && crane.IsReady(reference)) {
                crane.reserved = false;
                return crane;
            }
        }
        return null;
    }

    /// <summary>
    /// Find an available crane in this crane area.<br/>
    /// If found, return it, otherwise return null.
    /// </summary>
    /// <returns>Available crane found</returns>
    private Crane FindReadyCrane(Area origin)
    {
        foreach (Crane crane in cranes)
            if (crane.IsReady(origin))
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
        Crane crane = CompleteReservation(monoContainer.movement.originArea);
        if(crane != null) {
            containerCrane.Add(monoContainer, crane);
            return crane.AddContainer(monoContainer);
        }
        crane = FindReadyCrane(monoContainer.movement.originArea);

        if(crane != null){
            Area next = Game.instance.GetManager().GetNextArea(this,monoContainer.movement);
            if(next.ReserveArea(this, monoContainer.movement)) {
                containerCrane.Add(monoContainer, crane);
                return crane.AddContainer(monoContainer);
            }
        }
        return false;
    }

    protected override void RemoveContainer(MonoContainer monoContainer)
    {
        containerCrane[monoContainer].container = null;
        containerCrane.Remove(monoContainer);
    }

    public override bool ReserveArea(Area origin, Movement move) {
        Crane crane = FindAvailableCrane();
        if(crane != null && Game.instance.GetManager().GetNextArea(this,move).ReserveArea(this, move)) {
            bool reserved = crane.ReserveCrane(origin);
            return reserved;
        }
        return false;
    }
}
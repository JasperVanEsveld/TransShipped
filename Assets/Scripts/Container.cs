public enum containerType
{
    ShipContainer = 0,
    TruckContainer = 1,
    TrainContainer = 2
}

public class Container
{
    private containerType transType { get; set; }

    public Container(containerType transType)
    {
        this.transType = transType;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (obj.GetType() != GetType()) return false;
        return ((Container) obj).transType == transType;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() * 17 + transType.GetHashCode();
    }
}
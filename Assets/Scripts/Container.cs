public enum containerType
{
    Red = 0,
    Blue = 1,
    Green = 2
}

public class Container
{
    public containerType transType { get; private set; }

    public Container(containerType transType)
    {
        this.transType = transType;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (GetType() != obj.GetType()) return false;
        return ((Container) obj).transType == transType;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() * 17 + transType.GetHashCode();
    }
}
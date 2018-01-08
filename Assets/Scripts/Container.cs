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
        if (obj == null || GetType() != obj.GetType()) return false;
        Container other = obj as Container;

        return other.transType == this.transType;

    }

    public override int GetHashCode()
    {
        return base.GetHashCode() * 17 + transType.GetHashCode();
    }
}
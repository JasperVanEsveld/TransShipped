public enum containerType{Red = 0,Blue = 1,Green = 2};

public class Container
{

    public containerType transType { get; set; }

    public Container(containerType transType)
    {
        this.transType = transType;
    }

    public override bool Equals(object obj) {        
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }
        Container other = obj as Container;
        if(other.transType != this.transType ) {
            return false;
        }
        return base.Equals (obj);
    }
    
    public override int GetHashCode() {
        return base.GetHashCode()*17 + transType.GetHashCode();
    }
}
public enum containerType{Red = 0,Blue = 1,Green = 2};

public class Container
{
    public bool needsFridge { get; set; }

    public containerType transType { get; set; }

    public Container(bool needsFridge, containerType transType)
    {
        this.needsFridge = needsFridge;
        this.transType = transType;
    }

    public override bool Equals(object obj) {        
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }
        Container other = obj as Container;
        if(other.transType != this.transType || other.needsFridge != this.needsFridge) {
            return false;
        }
        return base.Equals (obj);
    }
    
    public override int GetHashCode() {
        return base.GetHashCode()*17 + needsFridge.GetHashCode() + transType.GetHashCode();
    }
}
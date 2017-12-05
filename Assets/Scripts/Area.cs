using System.Collections.Generic;
using UnityEngine;

public abstract class Area : MonoBehaviour
{
    ContainerManager manager;
    List<ContainerProcessor> connected;
    public abstract bool AddContainer(MonoContainer monoContainer);

    public bool Equals(object o)
    {
        if (o is Area){
            Area that = (Area)o;
            return this.gameObject.Equals(that.gameObject);
        }
        return false;
    }
}
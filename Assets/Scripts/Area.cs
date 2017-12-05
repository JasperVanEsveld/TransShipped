using System.Collections.Generic;
using UnityEngine;

public abstract class Area : MonoBehaviour
{
    protected ContainerManager manager;
    protected List<ContainerProcessor> connected;
    public abstract bool AddContainer(Container Container);
}
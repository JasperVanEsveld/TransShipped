using System.Collections.Generic;
using UnityEngine;

public abstract class Area : MonoBehaviour
{
    ContainerManager manager;
    List<ContainerProcessor> connected;
    public abstract bool AddContainer(MonoContainer monoContainer);
}
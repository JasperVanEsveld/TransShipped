using System.Collections.Generic;
using UnityEngine;

public abstract class Area : MonoBehaviour
{
    ContainerManager _manager;
    List<ContainerProcessor> _connected;
    protected abstract bool AddContainer(MonoContainer monoContainer);
}
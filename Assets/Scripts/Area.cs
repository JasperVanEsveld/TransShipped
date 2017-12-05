using System.Collections.Generic;
using UnityEngine;

public abstract class Area : MonoBehaviour
{
    private ContainerManager manager;
    private List<ContainerProcessor> connected;
    protected abstract bool AddContainer(MonoContainer monoContainer);
}
using System.Collections.Generic;
using UnityEngine;

public abstract class DeliveryVehicle : MonoBehaviour
{
    public List<MonoContainer> carrying;
    private List<Container> outgoing = new List<Container>();

    public List<MonoContainer> Carrying
    {
        get { return carrying; }
    }

    public IEnumerable<Container> Outgoing
    {
        get { return outgoing; }
    }
}
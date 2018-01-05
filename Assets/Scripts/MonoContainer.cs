using UnityEngine;

public class MonoContainer : MonoBehaviour
{
    public Container container;
    public Movement movement;

    public MonoContainer(Container container, Movement movement)
    {
        this.container = container;
        this.movement = movement;
    }
}
using UnityEngine;

public class MonoContainer : MonoBehaviour
{
    Container container;
    Movement movement;

    public MonoContainer(Container container, Movement movement)
    {
        this.container = container;
        this.movement = movement;
    }

    private void Start()
    {
        //todo This function initialize the MonoContainer with different colours according to their transType
    }
}
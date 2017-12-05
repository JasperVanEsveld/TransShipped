using UnityEngine;

public class MonoContainer : MonoBehaviour
{
    public Container _container;
    public Movement _movement;

    public MonoContainer(Container container, Movement movement)
    {
        _container = container;
        _movement = movement;
    }

    private void Start()
    {
        //todo This function initialize the MonoContainer with different colours according to their transType
    }
}
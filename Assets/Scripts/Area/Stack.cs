using System.Collections.Generic;

public class Stack : Area
{
    /// <summary>
    /// The queue of vehicle waiting to be served
    /// </summary>
    private Queue<Vehicle> waiting { get; set; }

    /// <summary>
    /// The vehicle currently being served
    /// </summary>
    private Vehicle current { get; set; }

    /// <summary>
    /// The crane over this stack
    /// </summary>
    private Crane crane { get; set; }

    /// <summary>
    /// Max number of containers possible should be decided when initiating a stack
    /// </summary>
    /// <param name="max">Max number of containers possible</param>
    public Stack(int max)
    {
        this.max = max;
    }

    /// <summary>
    /// A new vehicle is entering the stack, if none is being served right now,<br/>
    /// serve this vehicle immediately, otherwise put it in the queue
    /// </summary>
    /// <param name="v">The incoming vehicle</param>
    public override void OnVehicleEnter(Vehicle v)
    {
        if (waiting.Count == 0) current = v;
        else waiting.Enqueue(v);
    }

    /// <summary>
    /// When a vehicle leaves, start to serve next first one of the queue
    /// </summary>
    private void OnVehicleLeft()
    {
        current.LeaveArea();
        if (waiting.Count == 0) return;
        current = waiting.Dequeue();
    }

    /// <summary>
    /// Check whether this stack stacks a certain container<br/>
    /// If found, return its index in the list<br/>
    /// Otherwise return -1
    /// </summary>
    /// <param name="container">The object container</param>
    /// <returns>The index of the object container</returns>
    public int Contains(Container container)
    {
        for (var i = 0; i < containers.Count; i++)
        {
            if (containers[i].container.Equals(container))
                return i;
        }
        return -1;
    }

    /// <summary>
    /// As for a stack, AddContainer means it's destionation of the incoming container<br/>
    /// Call the crane to reposition this container from the vehicle to the stack<br/>
    /// If the stack is full, return false and abort the operation, otherwise reture true
    /// </summary>
    /// <param name="monoContainer">Arrived container</param>
    /// <returns>Whether the operation is successful</returns>
    protected override bool AddContainer(MonoContainer monoContainer)
    {
        return crane.Reposition(current, monoContainer, this);
    }

    /// <summary>
    /// As for a stack, RemoveContainer means a container is mounted onto a vehicle<br/>
    /// Call the crane to reposition this container from the stack to the vehicle<br/>
    /// If the vehicle is full, return false and abort the operation, otehrwise return true
    /// </summary>
    /// <param name="monoContainer">Target container</param>
    /// <returns>Whether the operation is successful</returns>
    protected override bool RemoveContainer(MonoContainer monoContainer)
    {
        return crane.Reposition(this, monoContainer, current);
    }


    private void Start()
    {
        waiting = new Queue<Vehicle>();
        current = null;
        game.manager.stacks.Add(this);
    }

    /// <summary>
    /// Serving the current vehicle
    /// </summary>
    private void Update()
    {
        if (current == null)
        {
            if (waiting.Count == 0) return;
            current = waiting.Dequeue();
        }
        for (var i = current.containers.Count - 1; i >= 0; i--)
        {
            if (current.containers[i].movement.targetArea != this) continue;
            if (!AddContainer(current.containers[i])) break;
            //todo Should let the player know that this stack is full, need to build another stack to unload containers
            //todo And have to set all the un-unloaded container to the new stack
        }
        for (var i = current.request.Count - 1; i >= 0; i--)
        {
            var index = Contains(current.request[i].container);
            if (index < 0) continue;
            containers[index].movement = current.request[i].movement;
            if (RemoveContainer(containers[index])) continue;
            current.LeaveArea();
            do current = SummonIdleVehicle(); while (current == null);
            RemoveContainer(containers[index]);
        }
        OnVehicleLeft();
    }
}
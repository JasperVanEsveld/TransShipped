using System;
using UnityEngine;

public class Crane : MonoBehaviour
{
    public float baseTime;
    private DateTime startTime;
    public MonoContainer container { private get; set; }
    public CraneArea craneArea;
    private Game game;

    private void Awake()
    {
        container = null;
        game = (Game) FindObjectOfType(typeof(Game));
    }

    /// <summary>
    /// Check if the crane is available<br/>
    /// Return true if it is available<br/>
    /// Otherwise return false
    /// </summary>
    /// <returns>Available or not</returns>
    public bool IsAvailable()
    {
        return container == null;
    }

    public bool AddContainer(MonoContainer monoContainer)
    {
        if (!IsAvailable())
        {
            if (container.movement.originArea != monoContainer.movement.originArea
                && monoContainer.movement.originArea is Road)
            {
//                    print("Holding container from: " + container.movement.originArea);
//                    print("New container from: " + monoContainer.movement.originArea);
                Area previous = container.movement.originArea;
                container.movement.originArea = craneArea;
                if (!previous.AddContainer(container)) return false;
                craneArea.RemoveContainer(container);
            }
            else return false;
        }

//            print("Received container from: " + monoContainer.movement.originArea);
        container = monoContainer;
        container.transform.SetParent(transform);
        container.transform.position = transform.position;
        startTime = DateTime.Now;
        return true;
    }

    private void Update()
    {
        if (!(game.currentState is OperationState)) return;
        if (!IsAvailable() && DateTime.Now.Subtract(startTime).TotalSeconds >= baseTime)
            craneArea.MoveToNext(container);
        else if (IsAvailable())
            craneArea.AreaAvailable();
    }
}
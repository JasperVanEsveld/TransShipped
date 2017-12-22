using System;
using UnityEngine;

public class Crane : MonoBehaviour
{
    private Game game;
    public CraneArea craneArea;
    public float bastTime;
    public MonoContainer container { private get; set; }

    public bool isAvailable { get; private set; }
    private DateTime startTime;

    private void Awake()
    {
        game = (Game) FindObjectOfType(typeof(Game));
        isAvailable = true;

        container = null;
    }

    public bool AddContainer(MonoContainer monoContainer)
    {
        if (!isAvailable) return false;
        isAvailable = false;
        container = monoContainer;
        startTime = DateTime.Now;
        monoContainer.transform.SetParent(transform);
        return true;
    }

    private void Update()
    {
        if (!(game.currentState is OperationState)) return;
        if (!isAvailable)
        {
            if (!(DateTime.Now.Subtract(startTime).Seconds >= bastTime)) return;
            if (craneArea.MoveToNext(container))
            {
                isAvailable = true;
            }
        }
        else
        {
            craneArea.AreaAvailable();

            print(craneArea + " now available");
        }
    }
}
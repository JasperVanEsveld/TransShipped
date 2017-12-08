using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameState currentState;
    public Stage currentStage;
    public List<Stage> stagesList;
    public Queue<Stage> stages;
    public int movements;
    public double money;
    private readonly List<Area> areas = new List<Area>();
    public bool operating;

    public void Start()
    {
        stages = new Queue<Stage>(stagesList);
        if(stages.Count > 0){
            currentStage = stages.Dequeue();
        } else {
            currentState = new LevelEndState(this);
        }
        currentState = new UpgradeState(this);
    }

    public void Update()
    {
        currentState.Update();
        if (operating && !(currentState is OperationState))
        {
            currentState = new OperationState(this);
        }
        else if (!operating && !(currentState is UpgradeState))
        {
            currentState = new UpgradeState(this);
        }
    }

    public void RegisterArea(Area area)
    {
        if (!areas.Contains(area))
        {
            areas.Add(area);
        }
    }

    public List<T> GetAreasOfType<T>() where T : Area
    {
        return areas.OfType<T>().Select(a => a).ToList();
    }
}
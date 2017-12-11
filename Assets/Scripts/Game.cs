using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void OnStateChanged(GameState newState);

public class Game : MonoBehaviour
{
    public GameState currentState {get;private set;}
    public Stage currentStage;
    public List<Stage> stagesList;
    public Queue<Stage> stages;
    public int movements;
    public double money;
    public List<OptionalArea> optionalAreas = new List<OptionalArea>();
    public event OnStateChanged stateChangeEvent;
    private readonly List<Area> areas = new List<Area>();

    public void Start()
    {
        stages = new Queue<Stage>(stagesList);
        if(stages.Count > 0){
            currentStage = stages.Dequeue();
        } else {
            ChangeState(new LevelEndState(this));
        }
        ChangeState(new UpgradeState(this));
    }

    public void Update()
    {
        currentState.Update();
    }

    public void RegisterArea(Area area)
    {
        if (!areas.Contains(area))
        {
            areas.Add(area);
        }
    }

    public void RegisterArea(OptionalArea area)
    {
        if (!optionalAreas.Contains(area))
        {
            optionalAreas.Add(area);
        }
    }

    public List<T> GetAreasOfType<T>() where T : Area
    {
        return areas.OfType<T>().Select(a => a).ToList();
    }

    public VehicleGenerator GetGenerator(){
        if(currentState is OperationState) {
            return ((OperationState) currentState).generator;
        }
        return null;
    }

    public ContainerManager GetManager(){
        if(currentState is OperationState) {
            return ((OperationState) currentState).manager;
        }
        return null;
    }

    public void ChangeState(GameState newState){
        if(stateChangeEvent != null){
            stateChangeEvent.Invoke(newState);
        }
        currentState = newState;
        
    }
}
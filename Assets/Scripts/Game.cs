using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void OnStateChanged(GameState newState);

public delegate void OnStageChanged(Stage newStage);

public delegate void OnMoneyChanged(double newValue);

public class Game : MonoBehaviour
{
    public static Game instance;
    public static GameState currentState { get; private set; }
    public static Stage currentStage;
    public List<Stage> stagesList;
    public Queue<Stage> stages;
    public int movements;

    private static double moneyValue = 300;

    public static double money
    {
        get { return moneyValue; }
        set
        {
            moneyValue = value;
            if (instance.moneyChangeEvent != null)
                instance.moneyChangeEvent(money);
        }
    }

    public List<DeliveryVehicle> vehicles = new List<DeliveryVehicle>();
    private readonly List<OptionalArea> optionalAreas = new List<OptionalArea>();
    private readonly List<Area> areas = new List<Area>();
    public event OnStateChanged stateChangeEvent;
    public event OnStageChanged stageChangeEvent;
    public event OnMoneyChanged moneyChangeEvent;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void Start()
    {
        stages = new Queue<Stage>(stagesList);
        if (stages.Count > 0)
            SetStage(stages.Dequeue());
        else
            ChangeState(new LevelEndState());

        ChangeState(new UpgradeState());
    }

    public void Update()
    {
        currentState.Update();
    }

    public static void RegisterArea(Area area)
    {
        if (!instance.areas.Contains(area))
            instance.areas.Add(area);
    }

    public static void RegisterArea(OptionalArea area)
    {
        if (!instance.optionalAreas.Contains(area))
            instance.optionalAreas.Add(area);
    }

    public static void DeregisterArea(OptionalArea area)
    {
        if (instance.optionalAreas.Contains(area))
            instance.optionalAreas.Remove(area);
    }

    public void RegisterWaiting(DeliveryVehicle vehicle)
    {
        if (!vehicles.Contains(vehicle))
            vehicles.Add(vehicle);
    }

    public static List<T> GetAreasOfType<T>() where T : Area
    {
        return instance.areas.OfType<T>().Select(a => a).ToList();
    }

    public static List<T> OnlyHighlight<T>() where T : Area
    {
        foreach(Area currentArea in instance.areas){
            if( currentArea is T) {
                currentArea.Highlight(true);
            } else if(!(currentArea is T)){
                currentArea.Highlight(false);
            }
        }
        return GetAreasOfType<T>();
    }

    public static void RemoveHighlights()
    {
        foreach(Area currentArea in instance.areas){
            currentArea.Highlight(false);
        }
    }

    public static VehicleGenerator GetGenerator()
    {
        var state = currentState as OperationState;
        return state != null ? state.generator : null;
    }

    public static ContainerManager GetManager()
    {
        var state = currentState as OperationState;
        return state != null ? state.manager : null;
    }

    public void ChangeState(GameState newState)
    {
        if (stateChangeEvent != null)
            stateChangeEvent.Invoke(newState);

        currentState = newState;
    }

    public void SetStage(Stage newStage)
    {
        currentStage = newStage;
        if (stageChangeEvent != null)
            stageChangeEvent(newStage);
    }
}
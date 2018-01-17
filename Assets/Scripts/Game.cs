using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void OnStateChanged(GameState newState);

public delegate void OnStageChanged(Stage newStage);

public delegate void OnMoneyChanged(double oldValue, double newValue);

public class Game : MonoBehaviour
{
    public static Game instance;
    public GameState currentState { get; private set; }
    public Stage currentStage;
    public List<Stage> stagesList;
    public List<HighlightAble> highlights = new List<HighlightAble>();
    public Queue<Stage> stages;
    public int movements;
    public double startMoney;

    private static double moneyValue = 0;

    public static double money
    {
        get { return moneyValue; }
        set
        {
            if (instance.moneyChangeEvent != null && moneyValue != value) {
                instance.moneyChangeEvent(moneyValue, value);
            }
            moneyValue = value;
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
        money = startMoney;
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

    public static void RegisterHighlight(HighlightAble highlight)
    {
        if (!instance.highlights.Contains(highlight))
            instance.highlights.Add(highlight);
    }

    public static void DeregisterHighlight(HighlightAble highlight)
    {
        if (instance.highlights.Contains(highlight))
            instance.highlights.Remove(highlight);
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
        foreach(HighlightAble currentArea in instance.highlights){
            if( currentArea is T) {
                currentArea.Highlight(true);
            } else if(!(currentArea is T)){
                currentArea.Highlight(false);
            }
        }
        return GetAreasOfType<T>();
    }

    public static void ForceRemoveHighlights()
    {
        foreach(HighlightAble currentArea in instance.highlights){
            currentArea.ForceHighlight(false);
        }
    }

    public static void RemoveHighlights()
    {
        foreach(HighlightAble currentArea in instance.highlights){
            currentArea.Highlight(false);
        }
    }

    public static VehicleGenerator GetGenerator()
    {
        var state = instance.currentState as OperationState;
        return state != null ? state.generator : null;
    }

    public static ContainerManager GetManager()
    {
        var state = instance.currentState as OperationState;
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
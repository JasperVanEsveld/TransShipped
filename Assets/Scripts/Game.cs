﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void OnStateChanged(GameState newState);

public delegate void OnStageChanged(Stage newStage);

public delegate void OnMoneyChanged(double newValue);

public class Game : MonoBehaviour
{
    public GameState currentState { get; private set; }
    public Stage currentStage;
    public List<Stage> stagesList;
    public Queue<Stage> stages;
    public int movements;
    private double moneyValue = 100;
    public double money {
        get{
            return this.moneyValue;
        }
        set{
            this.moneyValue = value;
            if(moneyChangeEvent != null){
                moneyChangeEvent(money);
            }
        }
    }
    public List<DeliveryVehicle> vehicles = new List<DeliveryVehicle>();
    public List<OptionalArea> optionalAreas = new List<OptionalArea>();
    private readonly List<Area> areas = new List<Area>();
    public event OnStateChanged stateChangeEvent;
    public event OnStageChanged stageChangeEvent;
    public event OnMoneyChanged moneyChangeEvent;

    public void Start()
    {
        stages = new Queue<Stage>(stagesList);
        if (stages.Count > 0)
            SetStage(stages.Dequeue());
        else
            ChangeState(new LevelEndState(this));

        ChangeState(new UpgradeState(this));
    }

    public void Update()
    {
        currentState.Update();
    }

    public void RegisterArea(Area area)
    {
        if (!areas.Contains(area))
            areas.Add(area);
    }

    public void RegisterArea(OptionalArea area)
    {
        if (!optionalAreas.Contains(area))
            optionalAreas.Add(area);
    }

    public void RegisterWaiting(DeliveryVehicle vehicle)
    {
        if (!vehicles.Contains(vehicle))
            vehicles.Add(vehicle);
    }

    public List<T> GetAreasOfType<T>() where T : Area
    {
        return areas.OfType<T>().Select(a => a).ToList();
    }

    public VehicleGenerator GetGenerator()
    {
        return currentState is OperationState ? ((OperationState) currentState).generator : null;
    }

    public ContainerManager GetManager()
    {
        return currentState is OperationState ? ((OperationState) currentState).manager : null;
    }

    public void ChangeState(GameState newState)
    {
        if (stateChangeEvent != null)
            stateChangeEvent.Invoke(newState);

        currentState = newState;
    }

    public void SetMoney(double money)
    {
        this.money = money;
    }

    public void SetStage(Stage newStage)
    {
        currentStage = newStage;
        if (stageChangeEvent != null)
            stageChangeEvent(newStage);
    }
}
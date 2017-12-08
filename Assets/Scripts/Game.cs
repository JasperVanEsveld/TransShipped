using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameState currentState;
    public Stage stage;
    public int movements;
    public double money;
    public List<OptionalArea> optionalAreas = new List<OptionalArea>();
    private readonly List<Area> areas = new List<Area>();
    public bool operating;

    public void Start()
    {
        if(operating){
            currentState = new OperationState(this);
        } else{
            currentState = new UpgradeState(this);
        }
    }

    public void Update()
    {
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
}
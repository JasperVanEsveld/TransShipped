using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    public GameState currentState;
	public Stage stage;
	public int movements = 0;
	public double money = 0;
    List<Area> areas = new List<Area>();
    public bool operating = false;

    public void Start(){
        currentState = new UpgradeState(this);
    }

    public void Update(){
        if(operating && !(currentState is OperationState)){
            currentState = new OperationState(this);
        } else if(!operating &&  !(currentState is UpgradeState)){
            currentState = new UpgradeState(this);
        }
    }

    public void RegisterArea(Area area){
        if(!areas.Contains(area)){
            areas.Add(area);
        }
    }

    public List<T> GetAreasOfType<T>() where T : Area{
        List<T> result = new List<T>();
        foreach(Area a in areas){
            if(a is T){
                result.Add(a as T);
            }
        }
        return result;
    }
}
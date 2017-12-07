using System.Collections.Generic;
using UnityEngine;

public abstract class Area : MonoBehaviour
{
    public Game game;
    public List<Area> connected;

    public void Start(){
        game.RegisterArea(this);
    }

    public abstract bool AddContainer(MonoContainer monoContainer);
    protected abstract void RemoveContainer(MonoContainer monoCont);

    protected bool MoveToNext(MonoContainer monoCont){
        if (monoCont.movement != null && game.currentState is OperationState) {
            Area nextArea = (game.currentState as OperationState).manager.GetNextArea(this, monoCont.movement);
            if(nextArea.AddContainer(monoCont)){
                RemoveContainer(monoCont);
                return true;
            }
            return false;
        }
        return false;
    }

    public bool Equals(object o)
    {
        if (o is Area){
            Area that = (Area)o;
            return this.gameObject.Equals(that.gameObject);
        }
        return false;
    }
}
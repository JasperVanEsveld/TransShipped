using System.Collections.Generic;
using UnityEngine;

public abstract class Area : MonoBehaviour
{
    public float time = 0f;
    public Game game;
    public ContainerManager manager;
    public List<Area> connected;

    public void Start(){
        manager = game.manager;
        print(manager);
    }

    public abstract bool AddContainer(MonoContainer monoContainer);
    protected abstract void RemoveContainer(MonoContainer monoCont);

    protected bool MoveToNext(MonoContainer monoCont){
        if (monoCont.movement != null) {
            Area nextArea = manager.GetNextArea(this, monoCont.movement);
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
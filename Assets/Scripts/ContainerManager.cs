using System.Collections.Generic;
using UnityEngine;

public class ContainerManager
{
    public List<Stack> stacks = new List<Stack>();

    void OnMovementComplete(Movement movement) {
    }

    public bool Store( Area start, MonoContainer container) {
        Stack target = LeastFilledStack();
        container.movement = new Movement(container,start,target);
        return true;
    }

    public bool Request(Area target, Container container) {
        Stack stackContaining = FindStackContaining(container);
        if(stackContaining == null){
            return false;
        }
        int i = stackContaining.Contains(container);
        MonoContainer monoCont = stackContaining.containers[i];
        monoCont.movement = new Movement(monoCont,stackContaining,target);
        return true;
    }

    public Area GetNextArea(Area area, Movement movement) {
        Dictionary<Area,Area> nextArea = new Dictionary<Area,Area>();
        Dictionary<Area,int> hops = new Dictionary<Area,int>();
        if ( movement.TargetArea == area){
            MonoBehaviour.print("HEllo");
            return null;
        }
        List<Area> areas = new List<Area>();
        areas.Add(area);
        Pair<Area,int> next = FirstArea(movement.TargetArea, area, areas);
        MonoBehaviour.print("Next object: " + next.First + "Distance: " + next.Second );
        return next.First;
    }

    private Pair<Area,int> FirstArea(Area current, Area target, List<Area> visited) {
        Pair<Area,int> result = null;
        int closest = int.MaxValue;
        foreach(Area area in current.connected) {
            if(area == target){
                return new Pair<Area,int>(current,1);
            }
            if (!visited.Contains(area) ) {
                visited.Add(area);
                Pair<Area,int> areaDistance = FirstArea(area, target, visited);
                if(areaDistance != null && areaDistance.Second < closest){
                    areaDistance.Second += 1;
                    result = areaDistance;
                }
            }
        }
        return result;
    }

    private Stack FindStackContaining(Container container){
        Stack result = null;
        foreach(Stack stack in stacks){
            if(stack.Contains(container) >= 0){
                result = stack;
            }
        }
        return result;
    }

    private Stack LeastFilledStack(){
        Game.print("Known stacks: " + stacks.Count);
        Stack result = null;
        int leastAmount = int.MaxValue;
        foreach(Stack stack in stacks){
            if(leastAmount > stack.containers.Count){
                result = stack;
                leastAmount = stack.containers.Count;
            }
        }
        return result;
    }
}
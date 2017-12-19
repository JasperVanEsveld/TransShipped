﻿using System.Collections.Generic;
using System.Linq;

public class ContainerManager
{
    private OperationState origin;
    private List<Stack> stacks;

    public ContainerManager(List<Stack> stacks, OperationState origin)
    {
        this.stacks = stacks;
        this.origin = origin;
    }

    void OnMovementComplete(Movement movement)
    {
        origin.OnMovementComplete();
    }

    public bool Store(Area start, MonoContainer container)
    {
        var target = LeastFilledStack();
        if (target == null) return false;
        container.movement = new Movement(target);
        return true;
    }

    public bool Request(Area target, Container container)
    {
        var stackContaining = FindStackContaining(container);
        if (stackContaining == null) {
            Game.print("Request failed, no stack containing" + container.transType);
            return false;
        }
        var i = stackContaining.Contains(container);
        var monoCont = stackContaining.containers[i];
        monoCont.movement = new Movement(target);
        return true;
    }

    public Area GetNextArea(Area area, Movement movement)
    {
        if (movement.TargetArea == area)
        {
            return null;
        }
        var visited = new List<Area> {area};
        Pair<Area, int> next = FirstArea(movement.TargetArea, area, visited);
        return next.First;
    }

    private static Pair<Area, int> FirstArea(Area current, Area target, List<Area> visited)
    {
        Pair<Area, int> result = null;
        const int closest = int.MaxValue;
        foreach (var area in current.connected)
        {
            if (area == target)
            {
                return new Pair<Area, int>(current, 1);
            }
            if (visited.Contains(area)) continue;
            visited.Add(area);
            var areaDistance = FirstArea(area, target, visited);
            if (areaDistance == null || areaDistance.Second >= closest) continue;
            areaDistance.Second += 1;
            result = areaDistance;
        }
        return result;
    }

    private Stack FindStackContaining(Container container)
    {
        Stack result = null;
        foreach (Stack stack in stacks)
        {
            if (stack.Contains(container) >= 0)
            {
                result = stack;
            }
        }
        return result;
    }

    private Stack LeastFilledStack()
    {
        Stack result = null;
        var leastAmount = int.MaxValue;
        foreach (var stack in stacks)
        {
            if (leastAmount <= stack.containers.Count) continue;
            result = stack;
            leastAmount = stack.containers.Count;
        }
        return result;
    }

    
}
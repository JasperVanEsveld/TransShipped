using System.Collections.Generic;

public class ContainerManager
{
    List<DeliveryArea<DeliveryVehicle>> _deliver;
    List<Stack> _stacks;

    void OnMovementComplete(Movement movement) {
    }

    void Store(MonoContainer container, Area start) {
        Stack target = LeastFilledStack();
        container._movement = new Movement(container,start,target);
    }

    void Request(Container container, Area target) {
        Stack stackContaining = FindStackContaining(container);

        //container._movement = new Movement(container,start,target);
    }

    Area GetNextArea(Area area, Movement movement) {
        return null;
    }
    private Stack FindStackContaining(Container container){
        Stack result = null;
        foreach(Stack stack in _stacks){
            if(stack.Contains(container) >= 0){
                result = stack;
            }
        }
        return result;
    }

    private Stack LeastFilledStack(){
        Stack result = null;
        int leastAmount = int.MaxValue;
        foreach(Stack stack in _stacks){
            if(leastAmount > stack._containers.Count){
                result = stack;
                leastAmount = stack._containers.Count;
            }
        }
        return result;
    }
}
using System.Collections.Generic;
using System;

public class DeliveryArea<T> : Area where T : DeliveryVehicle
{
    private DateTime start;
    private List<Container> remainingRequests;
    private Queue<T> waiting = new Queue<T>();
    private T current;
    private bool loading;

    public void OnVehicleEnter(T vehicle) {
        if(vehicle == current || waiting.Contains(vehicle)) return;
        if (!(game.currentState is OperationState)) return;
        if (current == null) {
            current = vehicle;
            Service(current);
        }
        else
        {
            waiting.Enqueue(vehicle);
        }
    }

    private void Service(T vehicle) {
        start = DateTime.Now;
        loading = false;
        remainingRequests = new List<Container>(vehicle.outgoing);
        foreach (var container in vehicle.carrying)
        {
            ((OperationState) game.currentState).manager.Store(this, container);
        }
        for (var i = vehicle.carrying.Count - 1; i >= 0; i--) {
            if (!MoveToNext(vehicle.carrying[i])) {
                AddToQueue(vehicle.carrying[i]);
            }
        }
    }

    public void requestOutgoing(){
        for(int i = remainingRequests.Count - 1; i >= 0; i--){
            Container c = remainingRequests[i];
            ContainerManager manager = game.GetManager();
            if(manager != null){
                if(manager.Request(this, c)){
                    remainingRequests.RemoveAt(i);
                }
            }
        }
    }

    private void OnVehicleLeaves()
    {
        current.LeaveTerminal();
        current = null;
        if (waiting.Count != 0) {
            current = waiting.Dequeue();
            Service(current);
        }
    }

    private void Update() {
        if(current != null ){
            if(start.Subtract(DateTime.Now).Seconds >= current.timeOutTime){
                OnVehicleLeaves();
            }
            if(current.carrying.Count == 0 && !loading){
                loading = true;
            } else if(loading && remainingRequests.Count > 0){
                requestOutgoing();
            } else if(loading && current.outgoing.Count == current.carrying.Count){
                game.SetMoney(game.money + current.reward);
                OnVehicleLeaves();
            }
        }
    }

    public override bool AddContainer(MonoContainer monoContainer)
    {
        current.carrying.Add(monoContainer);
        monoContainer.transform.SetParent(current.transform);
        monoContainer.movement = null;
        return true;
    }

    protected override void RemoveContainer(MonoContainer monoContainer)
    {
        current.carrying.Remove(monoContainer);
    }
}
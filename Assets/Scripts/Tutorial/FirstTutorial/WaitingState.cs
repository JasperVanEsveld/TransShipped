using UnityEngine;

public class WaitingState : TutorialState {
    public WaitingState(Tutorial tutorial) : base(tutorial) {
        this.text = "There is a ship waiting let it in by first clicking the button then the prefered birth. \n After that press the stack to store the containers in that area. ";
    }

    private void OnVehcicleGen(){
    }

    public override void BindAll(){
        OperationState.vehicleGeneratedEvent += OnVehcicleGen;
    }

    public override void UnBindAll(){
        OperationState.vehicleGeneratedEvent -= OnVehcicleGen;
    }
}
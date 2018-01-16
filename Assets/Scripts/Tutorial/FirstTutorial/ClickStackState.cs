using UnityEngine;

public class ClickStackState : TutorialState {
    public ClickStackState(Tutorial tutorial) : base(tutorial) {
        this.text = "The main purpose of the container terminal is to handle containers \n so the first thing the user needs is a stacking area where containers can be stored. \n You can buy a new stacking area by selecting one of the green areas.";
    }

    public void OptionalAreaClicked(OptionalArea area) {
        this.tut.currentState = new BuyStackState(this.tut);
    }

    public override void BindAll(){
        OptionalArea.MouseDownEvent += OptionalAreaClicked;
    }

    public override void UnBindAll(){
        OptionalArea.MouseDownEvent -= OptionalAreaClicked;
    }
}
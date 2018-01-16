using UnityEngine;

public class BuyStackState : TutorialState {
    public BuyStackState(Tutorial tutorial) : base(tutorial) {
        this.text = "To now buy this area you click the purchase button";
    }

    public void AreaBought(OptionalArea area) {
        tut.currentState = new ClickCraneState(tut);
    }

    public override void BindAll(){
        OptionalArea.AreaBought += AreaBought;
    }

    public override void UnBindAll(){
        OptionalArea.MouseDownEvent -= AreaBought;
    }
}
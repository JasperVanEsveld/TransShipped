using UnityEngine;

public class BuyCraneState : TutorialState {
    public BuyCraneState(Tutorial tutorial) : base(tutorial) {
        this.text = "To now buy this area you click the purchase button";
    }

    public void CraneBought(CraneArea area) {
        tut.currentState = new ReadyState(tut);
    }

    public override void BindAll(){
        CraneArea.CraneBought += CraneBought;
    }

    public override void UnBindAll(){
        CraneArea.CraneBought -= CraneBought;
    }
}
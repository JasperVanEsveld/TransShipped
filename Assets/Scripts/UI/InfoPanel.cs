using System;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    private Game game;
    public Text targetText;
    public Text moneyText;
    public Text timeRemainingText;

	// Use this for initialization
	private void Start () {
        game = FindObjectOfType<Game>();
        game.moneyChangeEvent += SetMoney;
        game.stateChangeEvent += StateChanged;
        game.stageChangeEvent += StageChanged;
        moneyText.text = "Money :" + game.money;

    }
	
	// Update is called once per frame
	private void Update () {
	    if (!timeRemainingText.IsActive()) return;
	    DateTime start = ((OperationState) game.currentState).startTime;
	    int difference = (int) DateTime.Now.Subtract(start).TotalSeconds;
	    timeRemainingText.text = "Time left :" + (int)(game.currentStage.duration - difference);
	}

    public void SetMoney(double newMoney) {
        moneyText.text = "Money :" + newMoney;
    }

    public void StageChanged(Stage newStage) {
       targetText.text = "Target :" + newStage.moneyRequired;
    }

    public void StateChanged(GameState newState)
    {
        timeRemainingText.gameObject.SetActive(newState is OperationState);
    }
}

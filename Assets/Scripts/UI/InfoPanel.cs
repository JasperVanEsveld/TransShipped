using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    private Game game;
    public Text targetText;
    public Text moneyText;
    public Text timeRemainingText;

	// Use this for initialization
	void Start () {
        game = FindObjectOfType<Game>();
        game.moneyChangeEvent += new OnMoneyChanged(SetMoney);
        game.stateChangeEvent += new OnStateChanged(StateChanged);
        game.stageChangeEvent += new OnStageChanged(StageChanged);
        moneyText.text = "Money :" + game.money;

    }
	
	// Update is called once per frame
	void Update () {
        if(timeRemainingText.IsActive()){
            DateTime start = ((OperationState) game.currentState).startTime;
            int difference = DateTime.Now.Subtract(start).Seconds;
            timeRemainingText.text = "Time left :" + (int)(game.currentStage.duration - difference);
        }
    }

    public void SetMoney(double newMoney) {
        moneyText.text = "Money :" + newMoney;
    }

    public void StageChanged(Stage newStage) {
       targetText.text = "Target :" + newStage.moneyRequired;
    }

    public void StateChanged(GameState newState) {
        if(newState is OperationState){
            timeRemainingText.gameObject.SetActive(true);
        } else{
            timeRemainingText.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StatePanelManager : MonoBehaviour {

	public GameObject commandPanel;
	public GameObject buildingPanel;
	public GameObject StagePassedPanel;
	public GameObject StageFailedPanel;
	public GameObject LevelCompletedPanel;

	private delegate void StateChangeAction();
	private Dictionary<Type,StateChangeAction> changeActions = new Dictionary<Type, StateChangeAction>();
	// Use this for initialization
	void Start () {
		Game.instance.stateChangeEvent += new OnStateChanged(stateChanged);
		changeActions.Add(typeof(OperationState),OnOperationStart);
		changeActions.Add(typeof(UpgradeState),OnUpgradeStart);
		changeActions.Add(typeof(StageEndState),OnStageEnd);
		changeActions.Add(typeof(LevelEndState),OnLevelEnd);	
	}

	public void OnOperationStart(){
		commandPanel.SetActive(true);
        buildingPanel.SetActive(false);
	}

	public void OnUpgradeStart(){
		StagePassedPanel.SetActive(false);
		StageFailedPanel.SetActive(false);
		commandPanel.SetActive(false);
        buildingPanel.SetActive(true);
	}

	public void OnStageEnd(){
		if(Game.instance.currentStage.IsSuccess(Game.instance.currentState)){
			StagePassedPanel.SetActive(true);
			commandPanel.SetActive(false);
		} else{
			StageFailedPanel.SetActive(true);
			commandPanel.SetActive(false);
		}
	}

	public void OnLevelEnd(){
		LevelCompletedPanel.SetActive(true);
		StagePassedPanel.SetActive(false);
	}

	public void stateChanged(GameState newState) {
		if(changeActions[newState.GetType()] != null){
			changeActions[newState.GetType()].Invoke();
		}
	}
}

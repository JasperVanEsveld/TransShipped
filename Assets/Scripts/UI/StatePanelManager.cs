using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StatePanelManager : MonoBehaviour {

	public GameObject commandPanel;
	public GameObject buildingPanel;

	private delegate void StateChangeAction();
	private Game game;
	private Dictionary<Type,StateChangeAction> changeActions = new Dictionary<Type, StateChangeAction>();
	// Use this for initialization
	void Start () {
		game = FindObjectOfType<Game>();
		game.stateChangeEvent += new OnStateChanged(stateChanged);
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
		commandPanel.SetActive(false);
        buildingPanel.SetActive(true);
	}

	public void OnStageEnd(){
		
	}

	public void OnLevelEnd(){
		
	}

	public void stateChanged(GameState newState) {
		if(changeActions[newState.GetType()] != null){
			changeActions[newState.GetType()].Invoke();
		}
	}
}

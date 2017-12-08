using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePanelManager : MonoBehaviour {

	private Game game;
	// Use this for initialization
	void Start () {
		game = FindObjectOfType<Game>();
		game.stateChangeEvent += new OnStateChanged(stateChanged);
	}

	public void stateChanged(GameState newState) {

	}
}

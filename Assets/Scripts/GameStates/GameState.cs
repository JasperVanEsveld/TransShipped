using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState {
	public Game game;

	public GameState( Game game){
		this.game = game;
	}
}

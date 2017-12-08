using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPanel : MonoBehaviour {
    private Game game;
	// Use this for initialization
	void Start () {
		game = FindObjectOfType<Game>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void beginGame()
    {
        game.ChangeState(new OperationState(game));
    }

    public void toggleStack(int i)
    {
        print(game.optionalAreas.Count);
        game.optionalAreas[i].BuyArea();
    }
}

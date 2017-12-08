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
        game.currentState = new OperationState(game);
        GameObject.Find("Canvas/CommandPanel").GetComponent<RectTransform>().anchorMin = new Vector2(0.05f, 0.0f);
        GameObject.Find("Canvas/CommandPanel").GetComponent<RectTransform>().anchorMax = new Vector2(0.95f, 0.32f);

        GameObject.Find("Canvas/BuildingPanel").SetActive(false);
    }

    public void toggleStack(int i)
    {
        print(game.optionalAreas.Count);
        game.optionalAreas[i].BuyArea();
    }
}

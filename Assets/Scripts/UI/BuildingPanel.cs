using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void beginGame()
    {
        GameObject.Find("Game").GetComponent<Game>().operating = true;
        GameObject.Find("Canvas/CommandPanel").GetComponent<RectTransform>().anchorMin = new Vector2(0.05f, 0.0f);
        GameObject.Find("Canvas/CommandPanel").GetComponent<RectTransform>().anchorMax = new Vector2(0.95f, 0.32f);

        GameObject.Find("Canvas/BuildingPanel").SetActive(false);
    }

    public void toggleStack(int i)
    {
        GameObject.Find("OptStack_" + i).GetComponent<OptionalArea>().BuyArea();
    }
}

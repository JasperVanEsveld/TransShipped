using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPanel : MonoBehaviour {
    private Game game;
    public Transform prefab;
    int buttonCount = 0;
    List<Transform> buttons = new List<Transform>();
	// Use this for initialization
	void Awake () {
		game = FindObjectOfType<Game>();
        CreateButtons();
	}
	
	// Update is called once per frame
	void Update () {
        /**
		if(buttonCount != game.optionalAreas.Count){
            CreateButtons();
        }
         */
	}

    public void CreateButtons(){
        foreach(Transform button in buttons){
            Destroy(button.gameObject);
        }
        buttons.Clear();
        float x = 85f;
        int i = 0;
        buttonCount = game.optionalAreas.Count;
        foreach(OptionalArea area in game.optionalAreas){
            Transform obj = Instantiate(prefab);
            obj.SetParent(this.transform, false);
            buttons.Add(obj);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x,0);
            x += 170f;
            obj.GetComponent<Button>().onClick.AddListener(area.BuyArea);
            obj.GetChild(0).GetComponent<Text>().text = "Buy " + area.areaName;
            i++;
        }
    }

    public void beginGame()
    {
        game.ChangeState(new OperationState(game));
    }
}

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
		if(buttonCount != game.optionalAreas.Count){
            CreateButtons();
        }
	}

    public void CreateButtons(){
        foreach(Transform button in buttons){
            Destroy(button.gameObject);
        }
        buttons.Clear();
        float min = 0.02f;
        float max = 0.08f;
        int i = 0;
        buttonCount = game.optionalAreas.Count;
        foreach(OptionalArea area in game.optionalAreas){
            Transform obj = Instantiate(prefab);
            obj.SetParent(this.transform.parent, false);
            buttons.Add(obj);
            obj.GetComponent<RectTransform>().anchorMin = new Vector2(min,0.05f);
            obj.GetComponent<RectTransform>().anchorMax = new Vector2(max,0.95f);
            min += 0.2f;
            max += 0.2f;
            obj.GetComponent<Button>().onClick.AddListener(area.BuyArea);
            i++;
        }
    }

    public void beginGame()
    {
        game.ChangeState(new OperationState(game));
    }
}

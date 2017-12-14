using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandPanel : MonoBehaviour {
    private Game game;
    public Transform prefab;
    int buttonCount = 0;
    List<Transform> buttons = new List<Transform>();

	void Awake () {
		game = FindObjectOfType<Game>();
        CreateButtons();
	}
	
	void Update () {
		if(buttonCount != game.vehicles.Count){
            CreateButtons();
        }
	}

    public void CreateButtons(){
        foreach(Transform button in buttons){
            Destroy(button.gameObject);
        }
        buttons.Clear();
        float x = 85f;
        int i = 0;
        buttonCount = game.vehicles.Count;
        foreach(DeliveryVehicle vehicle  in game.vehicles){
            Transform obj = Instantiate(prefab);
            obj.SetParent(this.transform, false);
            buttons.Add(obj);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x,0);
            x += 170f;
            obj.GetComponent<Button>().onClick.AddListener(vehicle.EnterTerminal);
            obj.GetChild(0).GetComponent<Text>().text = vehicle.GetType().ToString() + "\n Containers: " + vehicle.carrying.Count;
            i++;
        }
    }
}

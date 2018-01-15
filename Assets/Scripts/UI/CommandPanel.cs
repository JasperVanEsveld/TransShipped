using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandPanel : MonoBehaviour {
    public Transform prefab;
    int buttonCount;
    readonly List<Transform> buttons = new List<Transform>();
    Transform ShipTab;
    Transform TruckTab;
    Transform TrainTab;
    private Area selectedArea;
    private Stack selectedStack;
    private DeliveryVehicle currentVehicle;

    public void SetVehicle(DeliveryVehicle vehicle) {
        currentVehicle = vehicle;
    }

    public void SetDeliveryArea(Area area) {
        selectedArea = area;
        Game.OnlyHighlight<Stack>();
    }

    public void SetStackArea(Stack area) {
        selectedStack = area;
        Game.RemoveHighlights();
        SendVehicleIn();
    }

    private void SendVehicleIn() {
        currentVehicle.targetStack = selectedStack;
        if (currentVehicle is Ship && selectedArea is ShipArea) {
            ((Ship) currentVehicle).area = (ShipArea) selectedArea;
            ((Ship) currentVehicle).areaPos = selectedArea.transform.position;
        }
        else if (currentVehicle is Truck && selectedArea is TruckArea) {
            ((Truck) currentVehicle).area = (TruckArea) selectedArea;
            ((Truck) currentVehicle).areaPos = selectedArea.transform.position;
        }
        else if (currentVehicle is Train && selectedArea is TrainArea) {
            ((Train) currentVehicle).area = (TrainArea) selectedArea;
            ((Train) currentVehicle).areaPos = selectedArea.transform.position;
        }

        currentVehicle.EnterTerminal();
    }

    void Awake() {
        CreateButtons();
        ShipTab = transform.GetChild(5);
        TruckTab = transform.GetChild(4);
        TrainTab = transform.GetChild(3);
    }

    void Update() {
        if (buttonCount != Game.instance.vehicles.Count)
            CreateButtons();
    }

    private void CreateButtons() {
        foreach (Transform button in buttons)
            Destroy(button.gameObject);

        buttons.Clear();
        float x1 = 85;
        float x2 = 85;
        float x3 = 85;
        buttonCount = Game.instance.vehicles.Count;


        foreach (DeliveryVehicle vehicle in Game.instance.vehicles) {
            Transform obj = Instantiate(prefab);

            if (vehicle.GetType() == typeof(Ship)) {
                obj.SetParent(ShipTab, false);
                buttons.Add(obj);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x1, 0);
                x1 += 170;
            }
            else if (vehicle.GetType() == typeof(Train)) {
                obj.SetParent(TrainTab, false);
                buttons.Add(obj);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x2, 0);
                x2 += 170;
            }
            else {
                obj.SetParent(TruckTab, false);
                buttons.Add(obj);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x3, 0);
                x3 += 170;
            }

            obj.GetComponent<Button>().onClick.AddListener(vehicle.OnSelected);
            var vehicle1 = vehicle;
            obj.GetComponent<Button>().onClick.AddListener(() => SetVehicle(vehicle1));
            obj.GetChild(0).GetComponent<Text>().text = vehicle.GetType() + "\n Containers: " + vehicle.carrying.Count;
        }
    }
}
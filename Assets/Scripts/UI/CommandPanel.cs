using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandPanel : MonoBehaviour {
    public Transform prefab;
    int buttonCount;
    readonly List<Transform> buttons = new List<Transform>();
    GameObject ShipTab;
    GameObject TruckTab;
    GameObject TrainTab;
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
            ((ShipArea) selectedArea).occupied = true;
        }
        else if (currentVehicle is Truck && selectedArea is TruckArea) {
            ((Truck) currentVehicle).area = (TruckArea) selectedArea;
            ((Truck) currentVehicle).areaPos = selectedArea.transform.position;
            ((TruckArea) selectedArea).occupied = true;
        }
        else if (currentVehicle is Train && selectedArea is TrainArea) {
            ((Train) currentVehicle).area = (TrainArea) selectedArea;
            ((Train) currentVehicle).areaPos = selectedArea.transform.position;
            ((TrainArea) selectedArea).occupied = true;
        }

        currentVehicle.EnterTerminal();
    }

    void Awake() {
        CreateButtons();
        ShipTab = GameObject.Find("ShipCommand");
        TruckTab = GameObject.Find("TruckCommand");
        TrainTab = GameObject.Find("TrainCommand");
    }

    void Update() {
        if (buttonCount != Game.instance.vehicles.Count)
            CreateButtons();
    }

    private void CreateButtons() {
        foreach (Transform button in buttons)
            Destroy(button.gameObject);

        buttons.Clear();
        float x_max1 = 0;
        float x_max2 = 0;
        float x_max3 = 0;
        buttonCount = Game.instance.vehicles.Count;


        foreach (DeliveryVehicle vehicle in Game.instance.vehicles) {
            Transform obj = Instantiate(prefab);

            if (vehicle.GetType() == typeof(Ship) && ShipTab != null) {
                obj.SetParent(ShipTab.transform, false);
                buttons.Add(obj);
                RectTransform rectTrans = obj.GetComponent<RectTransform>();
                rectTrans.anchorMin = new Vector2(x_max1 + 0.01f, 0.1f);
                x_max1 += 0.2f;
                rectTrans.anchorMax = new Vector2(x_max1 - 0.01f, 0.9f);
            }
            else if (vehicle.GetType() == typeof(Train)) {
                obj.SetParent(TrainTab.transform, false);
                buttons.Add(obj);
                RectTransform rectTrans = obj.GetComponent<RectTransform>();
                rectTrans.anchorMin = new Vector2(x_max2 + 0.01f, 0.1f);
                x_max2 += 0.2f;
                rectTrans.anchorMax = new Vector2(x_max2 - 0.01f, 0.9f);
            }
            else {
                obj.SetParent(TruckTab.transform, false);
                buttons.Add(obj);
                RectTransform rectTrans = obj.GetComponent<RectTransform>();
                rectTrans.anchorMin = new Vector2(x_max3 + 0.01f, 0.1f);
                x_max3 += 0.2f;
                rectTrans.anchorMax = new Vector2(x_max3 - 0.01f, 0.9f);
            }

            obj.GetComponent<Button>().onClick.AddListener(vehicle.OnSelected);
            var vehicle1 = vehicle;
            obj.GetComponent<Button>().onClick.AddListener(() => SetVehicle(vehicle1));
            obj.GetChild(0).GetComponent<Text>().text = vehicle.GetType() + "\n Containers: " + vehicle.carrying.Count;
        }
    }
}
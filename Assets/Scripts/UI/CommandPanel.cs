using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandPanel : MonoBehaviour {
    private Transform buttonPrefab;
    private int shipCount, truckCount, trainCount;
    private GameObject ShipTab;
    private GameObject TruckTab;
    private GameObject TrainTab;
    private GameObject shipIcon;
    private GameObject truckIcon;
    private GameObject trainIcon;
    private Text shipText;
    private Text truckText;
    private Text trainText;
    private Area selectedArea;
    private Stack selectedStack;
    private DeliveryVehicle currentVehicle;
    private readonly List<Transform> shipButtons = new List<Transform>();
    private readonly List<Transform> truckButtons = new List<Transform>();
    private readonly List<Transform> trainButtons = new List<Transform>();

    private void SetVehicle(DeliveryVehicle vehicle) {
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
        } else if (currentVehicle is Truck && selectedArea is TruckArea) {
            ((Truck) currentVehicle).area = (TruckArea) selectedArea;
            ((Truck) currentVehicle).areaPos = selectedArea.transform.position;
            ((TruckArea) selectedArea).occupied = true;
        } else if (currentVehicle is Train && selectedArea is TrainArea) {
            ((Train) currentVehicle).area = (TrainArea) selectedArea;
            ((Train) currentVehicle).areaPos = selectedArea.transform.position;
            ((TrainArea) selectedArea).occupied = true;
        }

        currentVehicle.EnterTerminal();
    }

    private void Awake() {
        buttonPrefab = ((GameObject) Resources.Load("UI/button")).transform;
        ShipTab = GameObject.Find("ShipCommand");
        TruckTab = GameObject.Find("TruckCommand");
        TrainTab = GameObject.Find("TrainCommand");
        shipIcon = GameObject.Find("ShipCount");
        if (shipIcon != null) shipIcon.SetActive(false);

        truckIcon = GameObject.Find("TruckCount");
        if (truckIcon != null) truckIcon.SetActive(false);

        trainIcon = GameObject.Find("TrainCount");
        if (trainIcon != null) trainIcon.SetActive(false);

        shipCount = 0;
        trainCount = 0;
        truckCount = 0;
    }

    private void Update() {
        if (shipCount != Game.ships.Count)
            CreateShipButtons();
        else if (trainCount != Game.trains.Count)
            CreateTrainButtons();
        else if (truckCount != Game.trucks.Count)
            CreateTruckButtons();
    }

    private void CreateShipButtons() {
        foreach (Transform button in shipButtons) Destroy(button.gameObject);

        shipButtons.Clear();
        float xMax = 0;
        shipCount = Game.ships.Count;
        if (shipCount == 0) {
            shipIcon.SetActive(false);
            return;
        }

        foreach (Ship ship in Game.ships) {
            Transform button = Instantiate(buttonPrefab);

            button.SetParent(ShipTab.transform, false);
            shipButtons.Add(button);
            RectTransform rectTransform = button.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(xMax + 0.01f, 0.1f);
            xMax += 0.2f;
            rectTransform.anchorMax = new Vector2(xMax - 0.01f, 0.9f);

            button.GetComponent<Button>().onClick.AddListener(ship.OnSelected);
            Ship ship1 = ship;
            button.GetComponent<Button>().onClick.AddListener(() => SetVehicle(ship1));
            button.GetChild(0).GetComponent<Text>().text = ship.GetType() + "\n Containers: " + ship.carrying.Count;
        }

        shipIcon.SetActive(true);
        shipIcon.GetComponentInChildren<Text>().text = "" + shipCount;
    }

    private void CreateTruckButtons() {
        foreach (Transform button in truckButtons) { Destroy(button.gameObject); }

        truckButtons.Clear();
        float xMax = 0;
        truckCount = Game.trucks.Count;
        if (truckCount == 0) {
            truckIcon.SetActive(false);
            return;
        }

        foreach (Truck truck in Game.trucks) {
            Transform button = Instantiate(buttonPrefab);

            button.SetParent(TruckTab.transform, false);
            truckButtons.Add(button);
            RectTransform rectTransform = button.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(xMax + 0.01f, 0.1f);
            xMax += 0.2f;
            rectTransform.anchorMax = new Vector2(xMax - 0.01f, 0.9f);

            button.GetComponent<Button>().onClick.AddListener(truck.OnSelected);
            Truck truck1 = truck;
            button.GetComponent<Button>().onClick.AddListener(() => SetVehicle(truck1));
            button.GetChild(0).GetComponent<Text>().text = truck.GetType() + "\n Containers: " + truck.carrying.Count;
        }

        truckIcon.SetActive(true);
        truckIcon.GetComponentInChildren<Text>().text = "" + truckCount;
    }

    private void CreateTrainButtons() {
        foreach (Transform button in trainButtons) { Destroy(button.gameObject); }

        trainButtons.Clear();
        float xMax = 0;
        trainCount = Game.trains.Count;
        if (trainCount == 0) {
            trainIcon.SetActive(false);
            return;
        }

        foreach (Train train in Game.trains) {
            Transform button = Instantiate(buttonPrefab);

            button.SetParent(TrainTab.transform, false);
            trainButtons.Add(button);
            RectTransform rectTransform = button.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(xMax + 0.01f, 0.1f);
            xMax += 0.2f;
            rectTransform.anchorMax = new Vector2(xMax - 0.01f, 0.9f);

            button.GetComponent<Button>().onClick.AddListener(train.OnSelected);
            Train train1 = train;
            button.GetComponent<Button>().onClick.AddListener(() => SetVehicle(train1));
            button.GetChild(0).GetComponent<Text>().text = train.GetType() + "\n Containers: " + train.carrying.Count;
        }

        trainIcon.SetActive(true);
        trainIcon.GetComponentInChildren<Text>().text = "" + trainCount;
    }
}
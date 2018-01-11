using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandPanel : MonoBehaviour
{
    public Transform prefab;
    int buttonCount;
    readonly List<Transform> buttons = new List<Transform>();
    Transform ShipTab;
    Transform TruckTab;
    Transform TrainTab;


    void Awake()
    {
        CreateButtons();
        ShipTab = transform.GetChild(5);
        TruckTab = transform.GetChild(4);
        TrainTab = transform.GetChild(3);
    }

    void Update()
    {
        if (buttonCount != Game.instance.vehicles.Count)
            CreateButtons();
    }

    private void CreateButtons()
    {
        foreach (Transform button in buttons)
            Destroy(button.gameObject);

        buttons.Clear();
        float x1 = 85;
        float x2 = 85;
        float x3 = 85;
        buttonCount = Game.instance.vehicles.Count;


        foreach (DeliveryVehicle vehicle in Game.instance.vehicles)
        {
            Transform obj = Instantiate(prefab);

            if (vehicle.GetType() == typeof(Ship))
            {
                vehicle.areaPos = Game.GetAreasOfType<DeliveryArea<Ship>>()[0].transform.position;
                obj.SetParent(ShipTab, false);
                buttons.Add(obj);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x1, 0);
                x1 += 170;
            }
            else if (vehicle.GetType() == typeof(Train))
            {
                vehicle.areaPos = Game.GetAreasOfType<DeliveryArea<Train>>()[0].transform.position;
                obj.SetParent(TrainTab, false);
                buttons.Add(obj);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x2, 0);
                x2 += 170;
            }
            else
            {
                obj.SetParent(TruckTab, false);
                buttons.Add(obj);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x3, 0);
                x3 += 170;
                vehicle.areaPos = Game.GetAreasOfType<DeliveryArea<Truck>>()[0].transform.position;
            }

            obj.GetComponent<Button>().onClick.AddListener(vehicle.EnterTerminal);
            obj.GetChild(0).GetComponent<Text>().text = vehicle.GetType() + "\n Containers: " + vehicle.carrying.Count;
        }
    }
}
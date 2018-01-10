using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandPanel : MonoBehaviour
{
    public Transform prefab;
    int buttonCount;
    List<Transform> buttons = new List<Transform>();

    void Awake()
    {
        CreateButtons();
    }

    void Update()
    {
        if (buttonCount != Game.instance.vehicles.Count)
            CreateButtons();
    }

    public void CreateButtons()
    {
        foreach (Transform button in buttons)
            Destroy(button.gameObject);

        buttons.Clear();
        float x = 85;
        buttonCount = Game.instance.vehicles.Count;
        foreach (DeliveryVehicle vehicle in Game.instance.vehicles)
        {
            Transform obj = Instantiate(prefab);
            obj.SetParent(transform, false);
            buttons.Add(obj);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, 0);
            x += 170;
            
            if (vehicle.GetType() == typeof(Ship))
                vehicle.areaPos = Game.instance.GetAreasOfType<DeliveryArea<Ship>>()[0].transform.position;
            else if (vehicle.GetType() == typeof(Train))
                vehicle.areaPos = Game.instance.GetAreasOfType<DeliveryArea<Train>>()[0].transform.position;
            else
                vehicle.areaPos = Game.instance.GetAreasOfType<DeliveryArea<Truck>>()[0].transform.position;
            
            obj.GetComponent<Button>().onClick.AddListener(vehicle.EnterTerminal);
            obj.GetChild(0).GetComponent<Text>().text = vehicle.GetType() + "\n Containers: " + vehicle.carrying.Count;
        }
    }
}
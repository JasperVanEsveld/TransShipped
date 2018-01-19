using UnityEngine;
using UnityEngine.UI;

public class AGVButtons : MonoBehaviour {
    private Button BuyAGVButton, UpgradeAGVButton;
    private int buyPrice, upgradePrice;
    private int i;
    private Road road;

    private void Start() {
        road = FindObjectOfType<Road>();
        BuyAGVButton = GameObject.Find("BuyAGVButton").GetComponent<Button>();
        UpgradeAGVButton = GameObject.Find("UpgradeAGVButton").GetComponent<Button>();
    }

    private void Update() {
        if (buyPrice != Vehicle.buyPrice) {
            buyPrice = Vehicle.buyPrice;
            BuyAGVButton.GetComponentInChildren<Text>().text = "Buy AGV\n$" + buyPrice + " Each";
        }

        if (Game.instance.money < buyPrice)
            BuyAGVButton.gameObject.SetActive(false);

        if (upgradePrice != Vehicle.upgradePrice) {
            upgradePrice = Vehicle.upgradePrice;
            if (upgradePrice == -1) {
                UpgradeAGVButton.gameObject.SetActive(false);
                return;
            }

            UpgradeAGVButton.GetComponentInChildren<Text>().text = "Upgrade AGV\n$" + upgradePrice;
        }

        if (Game.instance.money < upgradePrice)
            UpgradeAGVButton.gameObject.SetActive(false);
    }

    public void AddVehicle() {
        if (UpgradeState.Buy(Vehicle.buyPrice)) {
            GameObject v = Instantiate(GameObject.Find("Vehicle"));
            road.vehicles.Add(v.GetComponent(typeof(Vehicle)) as Vehicle);
        }
        else if (i == 0)
            print("You don't have enough money");

        i++;
    }

    public void UpgradeAGV() {
        int ret = Vehicle.Upgrade();
        switch (ret) {
            case -1:
                print("You have already reached the maximum level");
                break;
            case 0:
                print("You don't have enough money");
                break;
            default:
                print("All AGVs now have been upgraded into level " + ret);
                break;
        }
    }
}
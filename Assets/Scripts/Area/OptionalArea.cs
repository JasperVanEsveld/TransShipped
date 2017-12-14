using System.Collections.Generic;
using UnityEngine;

public class OptionalArea : MonoBehaviour
{
    private Game game { get; set; }

    public List<Area> connected;
    public string areaName;
    public Transform prefab;

    public double price = 10;

    private bool locked;

    private int i = 0;

    private void Start() {
        game = (Game) FindObjectOfType(typeof(Game));
        game.RegisterArea(this);
    }

    public void BuyArea()
    {
        if (!(game.currentState is UpgradeState)) return;
        if (((UpgradeState) game.currentState).Buy(price))
        {
            game.optionalAreas.Remove(this);
            var stack = Instantiate(prefab, transform.position, transform.rotation).GetComponent<Stack>();
            stack.max = 5 * (int) ((transform.lossyScale.x - 2) / 2) * (int) (transform.lossyScale.z - 2);
            foreach (var connectArea in connected)
            {
                stack.Connect(connectArea);
            }
            Destroy(gameObject);
        }
        else if (i == 0)
        {
            print("You don't have enough money");
        }
        i++;
    }
}
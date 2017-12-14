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


//---- Implement this so that we can get rid of the clunky buy menu?   
    private Color startingColor_;
    void OnMouseEnter()
    {
        // if (in BUILDING stage)
        //{ 
            startingColor_ = GetComponent<Renderer>().material.color; // Move this to Start() so that it only needs to be exec once.
            GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        //}
    }
    void OnMouseExit()
    {
        // if (in BUILDING stage)
        //{
            GetComponent<Renderer>().material.color = startingColor_;
        //}
    }
    void OnMouseDown()
    {
        // if (in BUILDING stage && have enough money) 
        //{
            Debug.Log(name + "selected.");
        
       // }
    }
    // Called when entering operation stage. If not bought.
    public void HideAreaNotBought()
    {
        //if (not bought)
        //{
            GetComponent<MeshRenderer>().enabled = false;
        //}
    }
    // Called when entering another buy stage, if previously hidden.
    public void ShowAreaNotBought()
    {
        //if (not bought)
        //{
            //GetComponent<Renderer>().material.color = startingColor_;
            GetComponent<MeshRenderer>().enabled = true;
        //}
    }
    //---- Implement the above section so that we can get rid of the clunky buy menu?   ---------/
}
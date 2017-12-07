using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleShip : DeliveryVehicle
{
    public override void EnterTerminal()
    {
        targetPos_ = destPos_;
    }

    public override void LeaveTerminal()
    {
        targetPos_ = spawnPos_;
    }

    // Use this for initialization
    void Start ()
    {
        transform.position = spawnPos_;
        transform.localScale = spawnScale_;
        targetPos_ = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(targetPos_, transform.position) >= 0.1f)
        {
            float step = speed_ * Time.deltaTime;
            
        }
        
    }

}

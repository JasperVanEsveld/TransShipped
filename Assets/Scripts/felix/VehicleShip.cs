using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleShip : DeliveryVehicle
{


    public override void EnterTerminal()
    {
        moveAxisOrder = true;
        targetPos_ = destPos_;
    }

    public override void LeaveTerminal()
    {
        moveAxisOrder = false;
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
        if (moveAxisOrder)
        {
            if (transform.position.x - targetPos_.x > 0.1f)
            {
                float step = forwardSpeed_ * Time.deltaTime;
                transform.position += new Vector3(-step, 0.0f, 0.0f);
            }
            else if (transform.position.z - targetPos_.z > 0.1f)
            {
                float step = sidewaySpeed_ * Time.deltaTime;
                transform.position += new Vector3(0.0f, 0.0f, -step);
            }
        }
        else
        {
            if (transform.position.z - targetPos_.z < -0.1f)
            {
                float step = sidewaySpeed_ * Time.deltaTime;
                transform.position += new Vector3(0.0f, 0.0f, step);
            }
            else if (transform.position.x - targetPos_.x < -0.1f)
            {
                float step = forwardSpeed_ * Time.deltaTime;
                transform.position += new Vector3(step, 0.0f, 0.0f);
            }
        }

    }

}

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
        carrying = new List<MonoContainer>();
        System.Random rnd = new System.Random();
        // TODO: This probably will relate to size
        int conCount = rnd.Next(10, 30);

        for (int i = 0; i < conCount; ++i)
            carrying.Add(new MonoContainer(new Container(true, 1), null));

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

        // If at port
        if (isAtPort())
        {
            // TODO exchange cargo with terminal
        }
    }
    private bool isAtPort()
    {
        if (Vector3.Distance(destPos_, transform.position) < 0.1f)
            return true;
        else
            return false;
    }
}

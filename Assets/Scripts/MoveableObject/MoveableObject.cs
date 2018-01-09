using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveableObject : MonoBehaviour
{
    // HOW TO USE:
    // for all vehicles (child class of this class), call Init() in their Start();
    // for all vehicles (child class of this class), call MovementUpdate() in their Update(); It will take care of all the movement thinggy
    // For ship, invoke MOEnterTerminal(Vector3 i_dest) and MOLeaveTerminal() to... have it enter and leave. 
    // The i_dest should be the position of a parking spot for the ship, the east one of the west one.
    // You can add a sth in the end of MOLeaveTerminal() to have the script destroy the GameObject after leaving the screen.
    // For ground vehicle, just invoke PushNewDest(Vector3 i_dest) to have it move to that position. (the position has to on the ground, obviously)
    // You can utilize MOIsObjectMoving() and MOIsAtTheThisPos(Vector3 i_pos) to help you with debugging, but ideally you should not need to use them anymore.


    // CALL THIS IN Update() IN WHICHEVER CHILD CLASS YOU ARE USING
    // It will takes care of all the movement stuff so you would not need to worry about them in child class anymore.
    protected void MOMovementUpdate()
    {
        transform.position = CalcPosForNextFrame_();
    }

    // CALL THIS IN Start() FIRST IN WHICHEVER CHILD CLASS YOU ARE USING
    protected void MOInit(Vector3 i_initPos, float i_speed, bool i_isAtSeaLevel, Vector3 i_scale)
    {
        movementQueue_ = new Queue<Vector3>();
        lastPos_ = transform.position;
        transform.position = new Vector3(i_initPos.x, height_, i_initPos.z);
        speed_ = i_speed;
        isSeaVehicle_ = i_isAtSeaLevel;
        if (isSeaVehicle_)
            height_ = -2.0f;
        else
            height_ = 0.0f;
        transform.localScale = i_scale;
    }

    public void UpdateSpeed(float i_newSpeed)
    {
        speed_ = i_newSpeed;
    }

    // or this if you dont want to modify the scale
    protected void MOInit(Vector3 i_initPos, float i_speed, bool i_isAtSeaLevel)
    {
        movementQueue_ = new Queue<Vector3>();
        lastPos_ = transform.position;
        transform.position = new Vector3(i_initPos.x, height_, i_initPos.z);
        speed_ = i_speed;
        isSeaVehicle_ = i_isAtSeaLevel;
        if (isSeaVehicle_)
            height_ = -2.0f;
        else
            height_ = 0.0f;
    }

    // If the object is not moving/ does not have anywhere to go, return true
    public bool MOIsObjectMoving()
    {
        return movementQueue_.Count != 0;
    }

    // For ground vehicles, tell to object to move to this place when it's done with wtever it is doing
    public void MOPushNewDest(Vector3 i_dest)
    {
        if (MOIsAtTheThisPos(i_dest))
        {
            return;
        }
        int desRegion = GetRegion_(i_dest);
        int curRegion = GetRegion_(lastPos_);
//        Debug.Log(i_dest);
//        
//        Debug.Log(lastPos_);
        //Debug.Log("desRegion: " + desRegion + " curRegion: " + curRegion);
        if (curRegion == desRegion)
        {
            movementQueue_.Enqueue(new Vector3(lastPos_.x, height_, i_dest.z));
            movementQueue_.Enqueue(i_dest);
            lastPos_ = i_dest;
        }
        else
        {
            if (desRegion == 1)
            {
                movementQueue_.Enqueue(new Vector3(lastPos_.x, height_, i_dest.z));
                movementQueue_.Enqueue(i_dest);
                lastPos_ = i_dest;
            }
            else if ((desRegion == 0) && (curRegion == 2) || (desRegion == 2) && (curRegion == 0))
            {
                movementQueue_.Enqueue(new Vector3(lastPos_.x, height_, i_dest.z));
                movementQueue_.Enqueue(i_dest);
                lastPos_ = i_dest;
            }
            else if (desRegion == 4)
            {
                movementQueue_.Enqueue(new Vector3(lastPos_.x, height_, i_dest.z));
                movementQueue_.Enqueue(i_dest);
                lastPos_ = i_dest;
            }
            else
            {
                movementQueue_.Enqueue(new Vector3(i_dest.x, height_, lastPos_.z));
                movementQueue_.Enqueue(i_dest);
                lastPos_ = i_dest;
            }
        }
    }

    // Check if the object is at this place
    public bool MOIsAtTheThisPos(Vector3 i_pos)
    {
        Vector3 cur = transform.position;
        cur.y = height_;
        Vector3 des = i_pos;
        des.y = height_;

        float dis = Vector3.Distance(cur, des);
        if (dis <= speed_ * Time.deltaTime)
            return true;
        else
            return false;
    }

    // For ships, call this to have it enter terminal.
    // args: the position of the terminal area
    public void MOEnterTerminal(Vector3 i_dest)
    {
        int desRegion = GetRegion_(i_dest);
        if (desRegion == 5)
        {
            Vector3 intPoint2 = new Vector3(i_dest.x, height_, 36.0f);
            Vector3 intPoint1 = new Vector3(80.0f, height_, 36.0f);
            movementQueue_.Enqueue(intPoint1);
            movementQueue_.Enqueue(intPoint2);
            movementQueue_.Enqueue(i_dest);
        }
        else if (desRegion == 3)
        {
            Vector3 intPoint2 = new Vector3(i_dest.x, height_, -36.0f);
            Vector3 intPoint1 = new Vector3(80.0f, height_, -36.0f);
            movementQueue_.Enqueue(intPoint1);
            movementQueue_.Enqueue(intPoint2);
            movementQueue_.Enqueue(i_dest);
        }
    }

    // For ships, call this to have it leave terminal.
    public void MOLeaveTerminal()
    {
        int curRegion = GetRegion_(transform.position);
        if (curRegion == 5)
        {
            Vector3 intPoint1 = new Vector3(transform.position.x, height_, 36.0f);
            Vector3 intPoint2 = new Vector3(80.0f, height_, 36.0f);
            movementQueue_.Enqueue(intPoint1);
            movementQueue_.Enqueue(intPoint2);
        }
        else if (curRegion == 3)
        {
            Vector3 intPoint1 = new Vector3(transform.position.x, height_, -36.0f);
            Vector3 intPoint2 = new Vector3(80.0f, height_, -36.0f);
            movementQueue_.Enqueue(intPoint1);
            movementQueue_.Enqueue(intPoint2);
        }
        // TODO: Delete the parent GameObject
    }


    // ------------------- You should not need to worry about these below -----------------------//
    private Queue<Vector3> movementQueue_;

    private Vector3 lastPos_;
    private float speed_;
    private float height_;
    private bool isSeaVehicle_;

    // Check if the object has finished the current movement task
    private bool IsAtTheCurrentDest_()
    {
        if (!MOIsObjectMoving()) return true;
        else
        {
            Vector3 cur = transform.position;
            cur.y = height_;
            Vector3 des = movementQueue_.Peek();
            des.y = height_;

            float dis = Vector3.Distance(cur, des);
            if (dis <= speed_ * Time.deltaTime)
                return true;
            else
                return false;
        }
    }

    // Return a pos that the object is supposed to be for the next frame
    private Vector3 CalcPosForNextFrame_()
    {
        if ((!MOIsObjectMoving()))
        {
            return transform.position;
        }
        else
        {
            if (IsAtTheCurrentDest_())
            {
                movementQueue_.Dequeue();
                if (!MOIsObjectMoving())
                {
                    return transform.position;
                }
            }
            float step = speed_ * Time.deltaTime;
            Vector3 cur = transform.position;
            cur.y = height_;
            Vector3 des = movementQueue_.Peek();
            des.y = height_;
            return Vector3.MoveTowards(cur, des, step);
        }
    }


    private int GetRegion_(Vector3 i_vec)
    {
        float zp = 12.0f;
        float zn = -12.0f;
        float xs = 30.0f;
        float xc = 1.0f;
        float x = i_vec.x;
        float z = i_vec.z;

        if (z <= zn)
        {
            if (x <= xc)
            {
                return 0;
            }
            else if (x >= xs)
            {
                return 6;
            }
            else
            {
                return 3;
            }
        }
        else if (z >= zp)
        {
            if (x <= xc)
            {
                return 2;
            }
            else if (x >= xs)
            {
                return 8;
            }
            else
            {
                return 5;
            }
        }
        else
        {
            if (x <= xc)
            {
                return 1;
            }
            else if (x >= xs)
            {
                return 7;
            }
            else
            {
                return 4;
            }
        }
    }
}
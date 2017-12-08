using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GenerateShip : MonoBehaviour
{
    public void generateShip(int i)
    {
        Debug.Log("generateShip(" + i + ")");
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = "Ship_" + i;
        obj.AddComponent<Ship>();
        //return obj;
    }
}

using UnityEngine;
using System.Collections.Generic;

public enum VehicleType{Ship = 0, Truck = 1, Train = 2};

[CreateAssetMenu(menuName = "VehicleTemplate")]
public class VehicleTemplate : ScriptableObject
{
    public VehicleType type;
    public Transform prefab;
    public List<containerType> containerTypes;
    public int carryMin;
    public int carryMax;
    public int requestMin;
    public int requestMax;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;
}
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MoveableObject
{
    // Upgrade related stuff
    // They are static because I don't think we want user to upgrade individual AGV. Rather, upgrade all AGVs at once.
    private static int countAGV_ = 0;
    private static int level_ = 0;
    private const int maxLevel_ = 4;
    private static int[] costOfUpgrade_ = new int[maxLevel_ + 1] {0, 10, 20, 40, 80 };
    private static float[] bonusOfUpgrade_ = new float[maxLevel_ + 1] {1.0f, 1.1f, 1.2f, 1.4f, 1.8f};
    public static bool IsFullyUpgraded() { return ( level_ >= maxLevel_ ? true : false ); }
    public static int PeekNextUpgradeCost() { return ( IsFullyUpgraded() ? -1 : costOfUpgrade_[level_ + 1] ); } // Return -1 if already maxed out
    public static float PeekNextUpgradeBonus() { return (IsFullyUpgraded() ? -1 : bonusOfUpgrade_[level_ + 1]); } // Return -1 if already maxed out
    public static bool CanAffordNextUpgrade() { return ( GetMoney() >= PeekNextUpgradeCost() * countAGV_ + 0.01 ? true : false ); } // Note: Do we really need money to be double? I think we should make it int.
    public static bool CanUpgrade() { return ((IsFullyUpgraded() || !CanAffordNextUpgrade()) ? false : true); }
    public static bool Upgrade()
    {
        if (CanUpgrade())
        {
            Debug.Log("\nMoney: " + GetMoney() + "\nVehicleCount: " + countAGV_ + "\nCurrentSpeed: " + initSpeed_ + "\nUnitUpgradeCost:" + PeekNextUpgradeCost() + "\nTotalUpgradeCost" + PeekNextUpgradeCost() * countAGV_);
            SubtractMoney(PeekNextUpgradeCost() * countAGV_);
            initSpeed_ *= PeekNextUpgradeBonus();
            ++level_;
            Debug.Log("\nMoney: " + GetMoney() + "\nVehicleCount: " + countAGV_ + "\nCurrentSpeed: " + initSpeed_);
            return true;
        }
        else return false;
    }
    private void Start()
    {
        ++countAGV_;
        //Debug.Log("AGV Count: " + countAGV_);
    }
    private static float initSpeed_ = 5.0f;
    // TODO: Impl these
    private static double GetMoney() { return 999.0; }
    private static void SubtractMoney(int i_amount) { int a = i_amount; }
    // End of upgrade related stuff

    private Game game;
    public Road road;
    public int capacity;
    public List<MonoContainer> containers = new List<MonoContainer>();
    private Area targetArea;
    public Queue<Area> request = new Queue<Area>();

    private void Awake() {
        request = new Queue<Area>();
        game = (Game) FindObjectOfType(typeof(Game));
        MOInit(transform.position, 5.0f, false);
    }

    public bool AddContainer(MonoContainer monoContainer)
    {
        if (containers.Count >= capacity) return false;
        containers.Add(monoContainer);
        monoContainer.transform.SetParent(transform);
        return true;
    }

    public bool IsFull()
    {
        return containers.Count >= capacity;
    }

    public void GoTo(Area i_targetArea)
    {
        MOPushNewDest(i_targetArea.transform.position);
        this.targetArea = i_targetArea;
    }


    private void Update()
    {
        if (!(game.currentState is OperationState))
        {
            return;
        }
        for (var i = 0; i < containers.Count; i++)
            containers[i].transform.position = new Vector3(transform.position.x,
                transform.position.y + transform.lossyScale.y / 2 + i, transform.position.z);
        if (!MOIsObjectMoving())
        {
            if (containers.Count != 0)
            {
                targetArea = ((OperationState) game.currentState).manager.GetNextArea(road, containers[0].movement);
                GoTo(targetArea);
                if (MOIsAtTheThisPos(targetArea.transform.position))
                    road.MoveToNext(containers[0]);
            }
            else
            {
                //Debug.Log("Update: Branch 3");
                if (request.Count == 0) return;
                GoTo(request.Dequeue());
                targetArea.AreaAvailable(road);
            }
        }
        MOMovementUpdate();
    }
}
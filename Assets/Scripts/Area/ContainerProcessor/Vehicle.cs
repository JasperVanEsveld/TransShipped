using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MoveableObject
{
    // Upgrade related stuff
    // They are static because I don't think we want user to upgrade individual AGV. Rather, upgrade all AGVs at once.
    private static int countAGV_ = 0;
    private static int level_ = 0;
    private const int maxLevel_ = 4;
    private static int[] costOfPurchase_ = new int[maxLevel_ + 1] { 2, 3, 5, 7, 10 };
    private static int[] costOfUpgrade_ = new int[maxLevel_] { 1, 2, 2, 3 };
    private static float[] speedAtEachLevel_ = new float[maxLevel_ + 1] { 5.0f, 6.0f, 7.5f, 8.0f, 30.0f };
    public static bool IsFullyUpgraded() { return (level_ >= maxLevel_ ? true : false); }
    public static int UpgradeCost() { return (IsFullyUpgraded() ? -1 : costOfUpgrade_[level_]); } // Return -1 if already maxed out
    public static int PurchaseCost() { return costOfPurchase_[level_]; } // TODO: Invoke this after pressing buy AGV button.
    public static bool CanAffordNextUpgrade() { return (game.money >= UpgradeCost() * countAGV_ + 0.01 ? true : false); } // Note: Do we really need money to be double? I think we should make it int.
    public static bool CanUpgrade() { return ((IsFullyUpgraded() || !CanAffordNextUpgrade()) ? false : true); }
    public static bool Upgrade()
    {
        if (CanUpgrade())
        {
            game.money -= UpgradeCost() * countAGV_; // TODO: Currently the info panel doesnt update after upgrade
            speed_ = speedAtEachLevel_[level_ + 1];
            ++level_;
            Debug.Log("New Money: " + game.money + " AGV Level: "+ level_);
            return true;
        }
        else return false;
    }
    private void Start()
    {
        ++countAGV_;
    }
    private static float speed_ = speedAtEachLevel_[0];
    // End of upgrade related stuff
    static private Game game;
    public Road road;
    public int capacity;
    public List<MonoContainer> containers = new List<MonoContainer>();
    private Area targetArea;
    public Queue<Area> request = new Queue<Area>();

    private void Awake() {
        request = new Queue<Area>();
        game = (Game) FindObjectOfType(typeof(Game));
        MOInit(transform.position, speed_, false);
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
        UpdateSpeed(speed_);
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
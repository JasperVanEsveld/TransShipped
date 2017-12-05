using UnityEngine;

public class Game : MonoBehaviour
{
    GameState currentState;
    public ContainerManager manager = new ContainerManager();

    void OnContainerProcessed(Container container)
    {
    }
}

class GameState
{
}
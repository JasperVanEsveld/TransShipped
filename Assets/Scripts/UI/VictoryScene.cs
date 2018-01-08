using UnityEngine;

public class VictoryScene : MonoBehaviour
{
    private static void ShowVictoryScreen()
    {
        GameObject.Find("Canvas/VictoryPanel").GetComponent<RectTransform>().anchorMin = new Vector2(0.1f, 0.4f);
        GameObject.Find("Canvas/VictoryPanel").GetComponent<RectTransform>().anchorMax = new Vector2(0.9f, 0.9f);
    }

    //Placeholder. used to trigger ShowVictoryScreen after a certain time
    private int count;

    private void Update()
    {
        // check if victory
        //Placeholder. trigger after a certain time
        ++count;
        Debug.Log(count);
        if (count == 1000)
            ShowVictoryScreen();
    }
}
using UnityEngine;

public class MovePanel : MonoBehaviour
{
    private int state;

    public void HidePanel()
    {
        if (state == 0)
        {
            var rt = transform.parent.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.05f, -0.3f);
            rt.anchorMax = new Vector2(0.95f, 0.0f);
            state = 1;
        }
        else
        {
            var rt = transform.parent.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.05f, 0.0f);
            rt.anchorMax = new Vector2(0.95f, 0.3f);
            state = 0;
        }
    }
}
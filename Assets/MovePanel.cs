using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePanel : MonoBehaviour {
    private int state = 0;
    public void HidePanel()
    {
        if (state == 0)
        {
            RectTransform rt = transform.parent.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.05f, -0.3f);
            rt.anchorMax = new Vector2(0.95f, 0.0f);
            state = 1;
        }
        else
        {
            RectTransform rt = transform.parent.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.05f, 0.0f);
            rt.anchorMax = new Vector2(0.95f, 0.3f);
            state = 0;
        }
    }
}

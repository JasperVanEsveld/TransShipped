using UnityEngine;
using UnityEngine.UI;

public class ui_mouseover : MonoBehaviour
{
    // Use this for initialization
    private void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        //Debug.Log("Mouse is over GameObject.");
        var rt = GameObject.Find("Canvas/MouseoverTooltipPanel").GetComponent<RectTransform>();


        rt.anchorMin = new Vector2(0.00f, 0.4f);
        rt.anchorMax = new Vector2(0.1f, 0.8f);
        var txt = GameObject.Find("Canvas/MouseoverTooltipPanel/Text").GetComponent<Text>();
        txt.text = transform.name;
    }

    private void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        // Debug.Log("Mouse is no longer on GameObject.");
        var rt = GameObject.Find("Canvas/MouseoverTooltipPanel").GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.42f, -0.7f);
        rt.anchorMax = new Vector2(0.58f, -0.3f);
    }
}
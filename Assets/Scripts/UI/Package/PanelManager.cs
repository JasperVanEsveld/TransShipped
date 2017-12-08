using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public Animator initiallyOpen;

    private int m_OpenParameterId;
    private Animator m_Open;
    private GameObject m_PreviouslySelected;

    const string KOpenTransitionName = "Open";
    const string KClosedStateName = "Closed";

    public void OnEnable()
    {
        m_OpenParameterId = Animator.StringToHash(KOpenTransitionName);

        if (initiallyOpen == null)
            return;

        OpenPanel(initiallyOpen);
    }

    private void OpenPanel(Animator anim)
    {
        if (m_Open == anim)
            return;

        anim.gameObject.SetActive(true);
        var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;

        anim.transform.SetAsLastSibling();

        CloseCurrent();

        m_PreviouslySelected = newPreviouslySelected;

        m_Open = anim;
        m_Open.SetBool(m_OpenParameterId, true);

        GameObject go = FindFirstEnabledSelectable(anim.gameObject);

        SetSelected(go);
    }

    static GameObject FindFirstEnabledSelectable(GameObject gameObject)
    {
        var selectables = gameObject.GetComponentsInChildren<Selectable>(true);
        return (from selectable in selectables
            where selectable.IsActive() && selectable.IsInteractable()
            select selectable.gameObject).FirstOrDefault();
    }

    private void CloseCurrent()
    {
        if (m_Open == null)
            return;

        m_Open.SetBool(m_OpenParameterId, false);
        SetSelected(m_PreviouslySelected);
        StartCoroutine(DisablePanelDeleyed(m_Open));
        m_Open = null;
    }

    private IEnumerator DisablePanelDeleyed(Animator anim)
    {
        var closedStateReached = false;
        var wantToClose = true;
        while (!closedStateReached && wantToClose)
        {
            if (!anim.IsInTransition(0))
                closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName(KClosedStateName);

            wantToClose = !anim.GetBool(m_OpenParameterId);

            yield return new WaitForEndOfFrame();
        }

        if (wantToClose)
            anim.gameObject.SetActive(false);
    }

    private static void SetSelected(GameObject go)
    {
        EventSystem.current.SetSelectedGameObject(go);
    }
}
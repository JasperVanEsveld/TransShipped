using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
#else
		Application.Quit();
		#endif
    }
}
using UnityEngine;

public class UIScript : MonoBehaviour
{
    // Macht das UI unsichtbar, wenn hidePlayerUI() aufgerufen wird
    public void hidePlayerUI()
    {
        gameObject.SetActive(false);
    }

    public void showPlayerUI()
    {
        gameObject.SetActive(true);
    }
}

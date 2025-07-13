using UnityEngine;

public class UIScript : MonoBehaviour
{

    public GameObject playerUI; // Reference to the player UI GameObject
    // Macht das UI unsichtbar, wenn hidePlayerUI() aufgerufen wird
    public void hidePlayerUI()
    {
        playerUI.SetActive(false);
    }

    public void showPlayerUI()
    {
        playerUI.SetActive(true);
    }
}

using UnityEngine;

public class UIScript : MonoBehaviour
{

    public GameObject playerUI; // Reference to the player UI GameObject
    // method to hide the player UI, can be called when the player dies or during certain game events
    public void hidePlayerUI()
    {
        playerUI.SetActive(false);
    }

    public void showPlayerUI()
    {
        playerUI.SetActive(true);
    }
}

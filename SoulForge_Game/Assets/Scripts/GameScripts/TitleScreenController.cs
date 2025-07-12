using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
    private TitleScreen titleScreen;
    private GameObject player;
    private UIScript uiScript;

    void Start()
    {
        // Find TitleScreen automatically
        titleScreen = FindFirstObjectByType<TitleScreen>();
        // Find Player automatically (by tag)
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj;
        // Find UIScript automatically
        uiScript = FindFirstObjectByType<UIScript>();
        // Disable PlayerMovement at start
        if (player != null)
        {
            var movement = player.GetComponent<PlayerMovement>();
            if (movement != null)
                movement.enabled = false;
        }
        // Disable UI at start
        if (uiScript != null)
            uiScript.hidePlayerUI();
    }

    // Call this from the Start button on the title screen
    public void OnButtonPress()
    {
        // Hide the title screen
        if (titleScreen != null)
            titleScreen.HideTitleScreen();

        // Enable player movement
        if (player != null)
        {
            var movement = player.GetComponent<PlayerMovement>();
            if (movement != null)
                movement.enabled = true;
        }
        // Enable UI
        if (uiScript != null)
            uiScript.gameObject.SetActive(true);
    }
}

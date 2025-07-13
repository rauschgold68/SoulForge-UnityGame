using UnityEngine;

public class ResetUIHandler : MonoBehaviour
{
    public void OnResetButtonClick()
    {
        var handler = FindFirstObjectByType<HandleGameReset>();
        if (handler != null)
        {
            var uiScript = FindFirstObjectByType<UIScript>();
            if (uiScript != null)
            {
                // Hide the player UI before resetting
                uiScript.showPlayerUI();
            }
            handler.CommenceGameReset();
        }
        else
        {
            Debug.LogWarning("HandleGameReset nicht gefunden.");
        }
    }
}


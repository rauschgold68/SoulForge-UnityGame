using UnityEngine;

public class ResetUIHandler : MonoBehaviour
{
    public void OnResetButtonClick()
    {
        var handler = FindFirstObjectByType<HandleGameReset>();
        if (handler != null)
        {
            handler.CommenceGameReset();
        }
        else
        {
            Debug.LogWarning("HandleGameReset nicht gefunden.");
        }
    }
}


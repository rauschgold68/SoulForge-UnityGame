using UnityEngine;
using System.Collections;

public class GameQuit : MonoBehaviour
{
    public void OnQuitButtonClick()
    {
        Debug.Log("Spiel wird in 3 Sekunden beendet...");
        StartCoroutine(QuitAfterDelay(3f)); // 3 Sekunden warten
    }

    private IEnumerator QuitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Beende Spiel jetzt.");
        Application.Quit();
    }
}



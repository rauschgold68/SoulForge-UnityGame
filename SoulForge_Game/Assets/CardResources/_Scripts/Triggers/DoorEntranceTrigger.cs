using UnityEngine;

public class RoomEntranceTrigger : MonoBehaviour
{
    [SerializeField] private GameObject doorToClose; // Zuweisung im Inspector: Door1, Door2, Door3

    private bool alreadyTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            alreadyTriggered = true;

            if (doorToClose != null)
            {
                doorToClose.SetActive(true); // Tür schließen
                Debug.Log($"{doorToClose.name} geschlossen!");
            }
        }
    }

    // Wird vom GameReset aufgerufen
    public void ResetTrigger()
    {
        alreadyTriggered = false;

        // Nur Tür mit Name "Door1" wird geöffnet
        if (doorToClose != null && doorToClose.name == "Door1")
        {
            doorToClose.SetActive(false); // Tür öffnen
            Debug.Log("Door1 geöffnet (Reset)!");
        }
    }
}

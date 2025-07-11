using UnityEngine;

public class RoomEntranceTrigger : MonoBehaviour
{
    [SerializeField] private GameObject doorToClose; // Die Tür, die geschlossen werden soll

    private bool alreadyTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            alreadyTriggered = true;

            if (doorToClose != null)
            {
                doorToClose.SetActive(true); // Tür wird aktiv → geschlossen
                Debug.Log("Tür geschlossen!");
            }

            // Optional: Trigger-Objekt entfernen
            Destroy(gameObject);
        }
    }
}



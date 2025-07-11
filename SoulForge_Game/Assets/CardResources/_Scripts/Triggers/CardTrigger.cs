using UnityEngine;

public class CardTrigger : MonoBehaviour
{
[SerializeField] private CardGameManager cardGameManager;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            var cardGameManager = FindFirstObjectByType<CardGameManager>();
            var playerController = other.GetComponent<PlayerMovement>(); // Passe ggf. deinen Movement-Skriptnamen an

            if (playerController != null)
                playerController.SetMovementEnabled(false); // Methode im Movement-Skript, um Bewegung zu sperren

            cardGameManager.TriggerCardChoice(playerController);
        }
    }
}


using UnityEngine;

public class CardTrigger : MonoBehaviour
{
    [SerializeField] private CardGameManager cardGameManager;
    [SerializeField] private GameObject doorToClose;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;

            var playerController = other.GetComponent<PlayerMovement>();
            if (playerController != null)
            {
                playerController.SetMovementEnabled(false);
                cardGameManager.TriggerCardChoice(playerController, this);
            }
        }
    }
    
    public void CloseDoor()
{
    if (doorToClose != null)
    {
        doorToClose.SetActive(true); // Collider aktivieren = Tür zu
    }
}

}

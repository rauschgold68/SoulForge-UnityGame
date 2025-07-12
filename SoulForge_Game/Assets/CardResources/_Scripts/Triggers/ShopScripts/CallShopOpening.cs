using UnityEngine;

public class ShopToggle : MonoBehaviour
{
    [Header("Shop UI")]
    public GameObject shopUI;
    public GameObject interactiveButton;

    private bool isShopOpen = false;
    private bool playerInside = false;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            ToggleShop();
        }
    }

    void ToggleShop()
    {
        isShopOpen = !isShopOpen;

        if (shopUI != null)
            shopUI.SetActive(isShopOpen);

        if (interactiveButton != null)
            interactiveButton.SetActive(!isShopOpen && playerInside);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            if (!isShopOpen && interactiveButton != null)
                interactiveButton.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            if (interactiveButton != null)
                interactiveButton.SetActive(false);

            // Optional: Shop automatisch schließen, wenn Spieler weggeht
            if (shopUI != null)
                shopUI.SetActive(false);
            isShopOpen = false;
        }
    }
}

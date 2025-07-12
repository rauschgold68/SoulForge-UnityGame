using UnityEngine;

public class ShopToggle : MonoBehaviour
{
    [Header("Shop UI")]
    public GameObject shopUI;

    private bool isShopOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleShop();
        }
    }

    void ToggleShop()
    {
        isShopOpen = !isShopOpen;
        if (shopUI != null)
        {
            shopUI.SetActive(isShopOpen);
        }

        // Optional: Spiel pausieren oder Spielerbewegung deaktivieren
        // Time.timeScale = isShopOpen ? 0 : 1;
    }
}


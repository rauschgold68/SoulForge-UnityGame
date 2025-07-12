using UnityEngine;

public class CloseShop : MonoBehaviour
{
    public GameObject shopInterfaceToClose;         // Ziehe das Shop-UI GameObject hier rein (z. B. ein Canvas oder Panel)

    public void ShopClosingCall()
    {

        // Shop-UI schließen
        if (shopInterfaceToClose != null)
        {
            shopInterfaceToClose.SetActive(false);
            Debug.Log("Shop wurde geschlossen.");
        }
        else
        {
            Debug.LogWarning("Shop Interface wurde nicht zugewiesen!");
        }
    }
}

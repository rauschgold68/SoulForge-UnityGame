using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class BuyRightStack : MonoBehaviour
{
    public CardGameManager cardGameManager;

    public GameObject soldOutGO;  // ← Setze in Inspector auf dein "Sold Out"-Objekt
    public GameObject pricesGO;   // ← Setze in Inspector auf dein Preise-Objekt

    public GameObject soldOutGO2;  // ← Setze in Inspector auf dein "Sold Out"-Objekt
    public GameObject pricesGO2;   // ← Setze in Inspector auf dein Preise-Objekt

    public Button thisButton;     // ← Setze in Inspector auf diesen Button (oder hole automatisch)

    public Button thisButton2; 

    void Start()
    {
        // Falls du vergessen hast, im Inspector den Button zu setzen
        if (thisButton == null)
            thisButton = GetComponent<Button>();

        thisButton.onClick.AddListener(OnBuy);
    }

    private void OnBuy()
    {
        if (cardGameManager == null)
        {
            Debug.LogWarning("CardGameManager nicht zugewiesen.");
            return;
        }

        // Nur Epic Cards erlauben
        List<Card> epicCards = new List<Card>(Resources.LoadAll<Card>("Cards/Epic"));
        cardGameManager.OverrideCommonCardsWith(epicCards);

        // UI aktualisieren
        if (soldOutGO != null)
            soldOutGO.SetActive(true);

        if (pricesGO != null)
            pricesGO.SetActive(false);

            if (soldOutGO2 != null)
            soldOutGO2.SetActive(true);

        if (pricesGO2 != null)
            pricesGO2.SetActive(false);

        if (thisButton != null)
            thisButton.interactable = false;

        if (thisButton2 != null)
            thisButton2.interactable = false;

        // Optional: Wenn du den ganzen Button ausblenden willst:
        // thisButton.gameObject.SetActive(false);
    }
}

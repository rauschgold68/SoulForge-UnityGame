using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class BuyLeftStack : MonoBehaviour
{
    public CardGameManager cardGameManager;

    public GameObject soldOutGO;  // ← Setze in Inspector auf dein "Sold Out"-Objekt
    public GameObject pricesGO;   // ← Setze in Inspector auf dein Preise-Objekt

    public Button thisButton;     // ← Setze in Inspector auf diesen Button (oder hole automatisch)

    public SoulStore soulStore;  // ← Im Inspector zuweisen
    public int stackCost = 50;   // ← Preis des Stacks festlegen

    public GameObject ErrorText;


    void Start()
    {
        // Falls du vergessen hast, im Inspector den Button zu setzen
        if (thisButton == null)
            thisButton = GetComponent<Button>();

        thisButton.onClick.AddListener(OnBuy);
    }

    private void OnBuy()
    {
        if (cardGameManager == null || soulStore == null)
        {
            Debug.LogWarning("CardGameManager oder SoulStore nicht zugewiesen.");
            return;
        }

        if (!soulStore.CanAfford(stackCost))
        {
            if (ErrorText != null)
                StartCoroutine(ShowErrorText());

            Debug.Log("Nicht genug Souls für diesen Stack.");
            return;
        }

        // Kaufe Stack (Souls abziehen)
        soulStore.SpendSouls(stackCost);

        // Nur Rare Cards erlauben
        List<Card> rareCards = new List<Card>(Resources.LoadAll<Card>("Cards/Rare"));
        cardGameManager.OverrideCommonCardsWith(rareCards);

        // UI aktualisieren
        if (soldOutGO != null)
            soldOutGO.SetActive(true);

        if (pricesGO != null)
            pricesGO.SetActive(false);

        if (thisButton != null)
            thisButton.interactable = false;
    }

private System.Collections.IEnumerator ShowErrorText()
{
    ErrorText.SetActive(true);
    yield return new WaitForSeconds(0.5f);
    ErrorText.SetActive(false);
}

}

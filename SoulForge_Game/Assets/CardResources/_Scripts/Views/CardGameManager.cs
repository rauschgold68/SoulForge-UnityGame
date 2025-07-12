using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : MonoBehaviour
{
    [SerializeField] private HandView handView;
    [SerializeField] private CardView cardPrefab;
    [SerializeField] private Transform cardSpawnPoint;

    private List<Card> commonCards;
    private List<Card> rareCards;
    private List<Card> epicCards;

    // Diese Liste ist der Pool aus dem gezogen wird, hält alle verfügbaren Karten
    private List<Card> availableCards;

    private void Awake()
    {
        // Karten aus den jeweiligen Ordnern laden
        commonCards = new List<Card>(Resources.LoadAll<Card>("Cards/Common"));
        rareCards = new List<Card>(Resources.LoadAll<Card>("Cards/Rare"));
        epicCards = new List<Card>(Resources.LoadAll<Card>("Cards/Epic"));

        // Alle Karten in den verfügbaren Pool packen
        availableCards = new List<Card>();
        availableCards.AddRange(commonCards);
        availableCards.AddRange(rareCards);
        availableCards.AddRange(epicCards);

        Debug.Log($"Common: {commonCards.Count}, Rare: {rareCards.Count}, Epic: {epicCards.Count}");
    }

    private void Update()
    {
        // Entferne oder kommentiere diese Zeile:
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     handView.ClearHand();
        //     StartCoroutine(SpawnThreeRandomCardsWithLock());
        // }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null)
            {
                var cardView = hit.collider.GetComponent<CardView>();
                if (cardView != null)
                {
                    cardView.OnClickedByManager();
                }
            }
        }
    }


    public void TriggerCardChoice(PlayerMovement player, System.Action onFinish = null)
    {
        StartCoroutine(SpawnThreeRandomCardsWithLock(player, onFinish));
    }


    private IEnumerator SpawnThreeRandomCardsWithLock(PlayerMovement player, System.Action onFinish = null)
    {
        player.SetMovementEnabled(false);
        handView.ClearHand();

        int cardsToDraw = Mathf.Min(3, availableCards.Count);
        float spacing = 4f;
        Vector3 cameraCenter = Camera.main.transform.position;
        Vector3 spawnCenter = new Vector3(cameraCenter.x, cameraCenter.y, 0f);
        float totalWidth = spacing * (cardsToDraw - 1);
        float startX = spawnCenter.x - totalWidth / 2;

        for (int i = 0; i < cardsToDraw; i++)
        {
            Card randomCard = GetRandomCardByRarity();
            if (randomCard == null) yield break;

            availableCards.Remove(randomCard);

            Vector3 spawnPos = new Vector3(startX + i * spacing, spawnCenter.y, -1f);
            CardView cardView = Instantiate(cardPrefab, spawnPos, Quaternion.identity);

            cardView.Setup(randomCard, player);
            cardView.HandView = handView;
            cardView.GameManager = this;
            cardView.OnCardChosen = onFinish; // Callback nach Auswahl

            yield return StartCoroutine(handView.AddCard(cardView));
            yield return new WaitForSeconds(0.1f);
        }
    }





    private Card GetRandomCardByRarity()
    {
        float roll = Random.Range(0f, 1f);

        if (roll < 0.80f && HasCards(commonCards))
        {
            return GetRandomAvailableCardFromList(commonCards);
        }
        else if (roll < 0.95f && HasCards(rareCards))
        {
            return GetRandomAvailableCardFromList(rareCards);
        }
        else if (HasCards(epicCards))
        {
            return GetRandomAvailableCardFromList(epicCards);
        }

        // Falls ausgewählte Kategorie leer, versuche andere Kategorien (Fallback)
        if (HasCards(commonCards)) return GetRandomAvailableCardFromList(commonCards);
        if (HasCards(rareCards)) return GetRandomAvailableCardFromList(rareCards);
        if (HasCards(epicCards)) return GetRandomAvailableCardFromList(epicCards);

        return null; // Keine Karten mehr verfügbar
    }

    private bool HasCards(List<Card> list)
    {
        // Prüfen ob in der Liste Karten sind, die noch in availableCards sind
        foreach (var card in list)
        {
            if (availableCards.Contains(card))
                return true;
        }
        return false;
    }

    private Card GetRandomAvailableCardFromList(List<Card> list)
    {
        // Alle Karten aus der Liste filtern, die noch verfügbar sind
        List<Card> filtered = list.FindAll(card => availableCards.Contains(card));
        if (filtered.Count == 0)
            return null;

        return filtered[Random.Range(0, filtered.Count)];
    }

    public void RemoveCardFromStack(Card card)
    {
        if (availableCards.Contains(card))
        {
            availableCards.Remove(card);
            Debug.Log("Karte entfernt: " + card.Title);
        }
    }

    public void ReturnCardToStack(Card card)
    {
        if (!availableCards.Contains(card))
        {
            availableCards.Add(card);
            Debug.Log("Karte zurückgegeben: " + card.Title);
        }
    }
    
    public void ResetDeck()
{
    availableCards.Clear();

    // Optional: Lade Karten neu aus Resources, falls sich Dateien im Laufzeitverzeichnis ändern könnten
    commonCards = new List<Card>(Resources.LoadAll<Card>("Cards/Common"));
    rareCards = new List<Card>(Resources.LoadAll<Card>("Cards/Rare"));
    epicCards = new List<Card>(Resources.LoadAll<Card>("Cards/Epic"));

    availableCards.AddRange(commonCards);
    availableCards.AddRange(rareCards);
    availableCards.AddRange(epicCards);

    Debug.Log("Kartendeck wurde zurückgesetzt.");
}

}

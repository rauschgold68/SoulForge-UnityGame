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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            handView.ClearHand();
            StartCoroutine(SpawnThreeRandomCards());
        }

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

    private IEnumerator SpawnThreeRandomCards()
    {
        int cardsToDraw = Mathf.Min(3, availableCards.Count);

        for (int i = 0; i < cardsToDraw; i++)
        {
            Card randomCard = GetRandomCardByRarity();
            if (randomCard == null)
            {
                Debug.LogWarning("Keine Karten zum Ziehen verfügbar!");
                yield break;
            }

            availableCards.Remove(randomCard);

            CardView cardView = Instantiate(cardPrefab, cardSpawnPoint.position, Quaternion.identity);
            cardView.Setup(randomCard);
            cardView.HandView = handView;
            cardView.GameManager = this;

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
}

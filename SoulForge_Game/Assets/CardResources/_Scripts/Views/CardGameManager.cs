using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : MonoBehaviour
{
    [SerializeField] private HandView handView;
    [SerializeField] private CardView cardPrefab;
    [SerializeField] private Transform cardSpawnPoint;

    private List<Card> allCards;
    private List<Card> availableCards;

    private void Awake()
    {
        allCards = new List<Card>(Resources.LoadAll<Card>("Cards"));
        availableCards = new List<Card>(allCards);

        Debug.Log($"Geladene Karten insgesamt: {allCards.Count}");
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
            int randomIndex = Random.Range(0, availableCards.Count);
            Card randomCard = availableCards[randomIndex];
            availableCards.RemoveAt(randomIndex);

            CardView cardView = Instantiate(cardPrefab, cardSpawnPoint.position, Quaternion.identity);
            cardView.Setup(randomCard);
            cardView.HandView = handView;
            cardView.GameManager = this;

            yield return StartCoroutine(handView.AddCard(cardView));
            yield return new WaitForSeconds(0.1f);
        }
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

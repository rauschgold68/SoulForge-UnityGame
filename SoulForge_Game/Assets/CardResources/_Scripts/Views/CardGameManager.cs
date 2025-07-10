using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardGameManager : MonoBehaviour
{
    [SerializeField] private HandView handView;
    [SerializeField] private CardView cardPrefab;
    [SerializeField] private Transform cardSpawnPoint;

    private List<Card> availableCards;

    private void Awake()
    {
        // Alle Cards aus dem Resources/Cards Ordner laden
        availableCards = new List<Card>(Resources.LoadAll<Card>("Cards"));

        Debug.Log($"Loaded {availableCards.Count} cards from Resources/Cards.");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            handView.ClearHand();
            StartCoroutine(SpawnThreeRandomCards());
        }
    }

    private IEnumerator SpawnThreeRandomCards()
    {
        for (int i = 0; i < 3; i++)
        {
            if (availableCards.Count == 0) yield break;

            Card randomCard = availableCards[Random.Range(0, availableCards.Count)];

            CardView cardView = Instantiate(cardPrefab, cardSpawnPoint.position, Quaternion.identity);
            cardView.Setup(randomCard);
            yield return StartCoroutine(handView.AddCard(cardView));
            yield return new WaitForSeconds(0.1f);
        }
    }
}




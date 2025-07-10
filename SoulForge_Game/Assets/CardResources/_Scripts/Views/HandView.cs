using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandView : MonoBehaviour
{
    [SerializeField] private Transform[] cardSlots = new Transform[3];
    [SerializeField] private CardView cardPrefab;

    private readonly List<CardView> cards = new();

    public IEnumerator AddCard(CardView cardView)
    {
        if (cards.Count >= cardSlots.Length)
        {
            Debug.LogWarning("Hand is full!");
            yield break;
        }

        cards.Add(cardView);
        yield return UpdateCardPosition(0.15f);
    }

    private IEnumerator UpdateCardPosition(float duration)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.DOMove(cardSlots[i].position, duration);
            cards[i].transform.DORotate(cardSlots[i].rotation.eulerAngles, duration);
        }
        yield return new WaitForSeconds(duration);
    }

    public void ClearHand()
    {
        foreach (var card in cards)
        {
            Destroy(card.gameObject);
        }
        cards.Clear();
    }

    public void RemoveCard(CardView cardView)
    {
        if (cards.Contains(cardView))
        {
            cards.Remove(cardView);
            StartCoroutine(UpdateCardPosition(0.15f));
        }
    }

    public void AnimateAndHideOtherCards(CardView selectedCard, CardGameManager gameManager)
    {
        for (int i = cards.Count - 1; i >= 0; i--)
        {
            var card = cards[i];
            if (card != selectedCard)
            {
                // Karte zurückgeben
                gameManager.ReturnCardToStack(card.Card);

                // Karte ausblenden mit Animation
                card.transform.DOMove(card.transform.position + Vector3.left * 2f, 0.3f);
                card.transform.DOScale(0f, 0.3f).OnComplete(() =>
                {
                    card.gameObject.SetActive(false);
                });

                cards.RemoveAt(i);
            }
            // Für ausgewählte Karte keine Skalierung mehr hier, das macht CardView selbst
        }
    }
}

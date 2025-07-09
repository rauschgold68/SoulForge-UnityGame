using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

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
}



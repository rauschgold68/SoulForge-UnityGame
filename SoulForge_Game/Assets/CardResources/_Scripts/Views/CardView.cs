using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text description;

    [SerializeField] private TMP_Text title;

    [SerializeField] private SpriteRenderer imageSR;

    [SerializeField] private GameObject wrapper;

    public Card Card { get; private set; }
    public void Setup(Card card)
    {
        Card = card;
        title.text = card.Title;
        description.text = card.Description;
        imageSR.sprite = card.Image;
    }
}

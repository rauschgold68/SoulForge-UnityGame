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

        float targetHeight = 1.25f;

    
        float spriteHeight = card.Image.bounds.size.y;

    
        float scale = targetHeight / spriteHeight;
        imageSR.transform.localScale = new Vector3(scale, scale, 1f);
    }
}

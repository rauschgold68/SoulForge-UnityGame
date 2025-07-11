using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text title;
    [SerializeField] private SpriteRenderer imageSR;

    public Card Card { get; private set; }
    public HandView HandView { get; set; }
    public CardGameManager GameManager { get; set; }

    private Vector3 originalScale;

    private PlayerMovement playerController;


    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void Setup(Card card, PlayerMovement player = null)
{
    Card = card;
    title.text = card.Title;
    description.text = card.Description;
    imageSR.sprite = card.Image;

    float targetHeight = 1.25f;
    float spriteHeight = card.Image.bounds.size.y;
    float scale = targetHeight / spriteHeight;
    imageSR.transform.localScale = new Vector3(scale, scale, 1f);

    playerController = player; // <- damit `ScaleUpAndDisappear()` den Spieler wieder freigeben kann
}



    public void OnClickedByManager()
{
    // Genau das gleiche wie OnMouseDown bisher
    if (gameObject.activeInHierarchy)
    {
        Debug.Log("Card clicked: " + Card.Title);

        GameManager.RemoveCardFromStack(Card);

        HandView.AnimateAndHideOtherCards(this, GameManager);
        HandView.RemoveCard(this);
        StartCoroutine(ScaleUpAndDisappear());
    }
}


    private IEnumerator ScaleUpAndDisappear()
    {
        yield return transform.DOScale(1.5f, 0.3f).WaitForCompletion();
        yield return new WaitForSeconds(0.5f);
        yield return transform.DOScale(0f, 0.5f).WaitForCompletion();
        gameObject.SetActive(false);
        if (playerController != null)
{
    playerController.SetMovementEnabled(true); // <-- funktioniert nur, wenn es diese Methode gibt
}


    }
}

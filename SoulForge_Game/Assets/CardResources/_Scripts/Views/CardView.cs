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

public System.Action OnCardChosen; // <-- Wird von GameManager gesetzt

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
        if (gameObject.activeInHierarchy)
        {
            Debug.Log("Card clicked: " + Card.Title);

            GameManager.RemoveCardFromStack(Card);

            ApplyCardEffect(Card.Title);

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
if (playerController != null)
{
    playerController.SetMovementEnabled(true);
}

if (OnCardChosen != null)
{
    OnCardChosen.Invoke(); // z. B. Tür öffnen
}


    }
    
    private void ApplyCardEffect(string cardName)
{
    if (playerController == null)
    {
        Debug.LogWarning("Kein PlayerController gefunden!");
        return;
    }

    var cardController = playerController.GetComponent<CardController>();
    if (cardController == null)
    {
        Debug.LogWarning("CardController nicht am Spieler gefunden!");
        return;
    }

        switch (cardName)
        {
            // Health Upgrades
            case "HealthCard1":
                cardController.AddHealthUpgrade("FirstUpgrade");
                break;
            case "HealthCard2":
                cardController.AddHealthUpgrade("SecondUpgrade");
                break;
            case "HealthCard3":
                cardController.AddHealthUpgrade("ThirdUpgrade");
                break;

            // Speed Upgrades
            case "SpeedCard1":
                cardController.AddSpeedUpgrade("FirstUpgrade");
                break;
            case "SpeedCard2":
                cardController.AddSpeedUpgrade("SecondUpgrade");
                break;
            case "SpeedCard3":
                cardController.AddSpeedUpgrade("ThirdUpgrade");
                break;

            // Damage Upgrades
            case "DamageCard1":
                cardController.AddDamageUpgrade("FirstUpgrade");
                break;
            case "DamageCard2":
                cardController.AddDamageUpgrade("SecondUpgrade");
                break;
            case "DamageCard3":
                cardController.AddDamageUpgrade("ThirdUpgrade");
                break;

            // Attack Range Upgrades
            case "RangeCard1":
                cardController.AddAttackRangeUpgrade("FirstUpgrade");
                break;
            case "RangeCard2":
                cardController.AddAttackRangeUpgrade("SecondUpgrade");
                break;
            case "RangeCard3":
                cardController.AddAttackRangeUpgrade("ThirdUpgrade");
                break;

            // Attack Speed Upgrades
            case "AttSpeedCard1":
                cardController.AddAttackSpeedUpgrade("FirstUpgrade");
                break;
            case "AttSpeedCard2":
                cardController.AddAttackSpeedUpgrade("SecondUpgrade");
                break;
            case "AttSpeedCard3":
                cardController.AddAttackSpeedUpgrade("ThirdUpgrade");
                break;

                // Life Steal Upgrades
        case "LifeStealCard1":
            cardController.AddLifeStealUpgrade("FirstUpgrade");
            break;
        case "LifeStealCard2":
            cardController.AddLifeStealUpgrade("SecondUpgrade");
            break;
        case "LifeStealCard3":
            cardController.AddLifeStealUpgrade("ThirdUpgrade");
            break;

            default:
                Debug.LogWarning("Kein Effekt für Karte: " + cardName);
                break;
            
        
    }
}

}

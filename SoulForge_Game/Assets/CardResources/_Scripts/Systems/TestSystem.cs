using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private HandView handView;

    [SerializeField] private CardData cardData;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Card card = new(cardData);
            CardView cardView = CardViewCreators.Instance.CreateCardView(card, transform.position, Quaternion.identity);
            StartCoroutine(handView.AddCard(cardView));
        }
    }
}

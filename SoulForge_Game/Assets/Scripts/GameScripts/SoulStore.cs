using UnityEngine;

public class SoulStore : MonoBehaviour
{
    public int cardDeckUpgrade1Cost = 50;
    public int cardDeckUpgrade2Cost = 120;

    private StatController statController;

    void Awake()
    {
        statController = FindFirstObjectByType<StatController>();
    }

    private int GetSouls()
    {
        return statController != null ? statController.SoulAmount : 0;
    }

    public bool CanAfford(int cost)
    {
        return GetSouls() >= cost;
    }

    public bool BuyCardDeckUpgrade1()
    {
        if (CanAfford(cardDeckUpgrade1Cost))
        {
            statController.SoulAmount -= cardDeckUpgrade1Cost;
            Debug.Log("Card Deck Upgrade 1 purchased.");
            return true;
        }
        return false;
    }

    public bool BuyCardDeckUpgrade2()
    {
        if (CanAfford(cardDeckUpgrade2Cost))
        {
            statController.SoulAmount -= cardDeckUpgrade2Cost;
            Debug.Log("Card Deck Upgrade 2 purchased.");
            return true;
        }
        return false;
    }

    public void SpendSouls(int amount)
{
    if (statController != null)
        statController.SoulAmount -= amount;
}

}

using UnityEngine;

[CreateAssetMenu(menuName = "Card")]
public class Card : ScriptableObject
{
    [SerializeField] private CardData data;

    public string Title => data != null ? data.name : "";
    public string Description => data != null ? data.Description : "";
    public Sprite Image => data != null ? data.Image : null;
}


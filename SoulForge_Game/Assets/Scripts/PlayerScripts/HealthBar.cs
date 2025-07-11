using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public RectTransform barRectTransform; // Assign in Inspector
    public int defaultMaxHealth = 120;
    public float defaultWidth = 200f; // Set this to your default bar width in the Inspector

    public void setMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health; // Set initial value to max
        UpdateBarWidth(health);
    }

    public void setCurrentHealth(int health)
    {
        healthSlider.value = health;
    }

    private void UpdateBarWidth(int maxHealth)
    {
        if (barRectTransform == null) return;
        float width = defaultWidth;
        if (maxHealth > defaultMaxHealth)
        {
            width = defaultWidth * ((float)maxHealth / defaultMaxHealth);
        }
        barRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        // Set pivot to left so bar grows to the right
        barRectTransform.pivot = new Vector2(0f, barRectTransform.pivot.y);
        // Optionally, set anchoredPosition.x to 0 to keep left edge fixed
        barRectTransform.anchoredPosition = new Vector2(80f, barRectTransform.anchoredPosition.y);
    }
}

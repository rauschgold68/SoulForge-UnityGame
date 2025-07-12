using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SoulDisplay : MonoBehaviour
{
    public enum DisplayMode { Whole, Current }
    public DisplayMode displayMode = DisplayMode.Whole;

    public TextMeshProUGUI soulText;
    private StatController statController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        statController = FindFirstObjectByType<StatController>();
        UpdateSoulDisplay();
    }
    private void UpdateSoulDisplay()
    {
        if (soulText != null && statController != null)
        {
            if (displayMode == DisplayMode.Whole)
                soulText.text = statController.SoulAmount.ToString();
            else
                soulText.text = statController.SoulsThisRound.ToString();
        }
        else if (soulText == null)
        {
            Debug.LogWarning("SoulDisplay: soulText is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSoulDisplay();
    }
}

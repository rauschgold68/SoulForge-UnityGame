using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public void HideTitleScreen()
    {
        gameObject.SetActive(false);
    }

    public void ShowTitleScreen()
    {
        gameObject.SetActive(true);
    }
}


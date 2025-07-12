using UnityEngine;

public class UIScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ...existing code...
    }

    // Update is called once per frame
    void Update()
    {
        // ...existing code...
    }

    // Macht das UI unsichtbar, wenn hidePlayerUI() aufgerufen wird
    public void hidePlayerUI()
    {
        gameObject.SetActive(false);
    }
}

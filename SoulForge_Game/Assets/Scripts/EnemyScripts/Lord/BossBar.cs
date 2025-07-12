using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    public Slider bossSlider;
    public GameObject barRoot; // Parent GameObject of the bar, set active/inactive
    public Lords_Behaviour lord; // Assign in Inspector
    public float showDistance = 8f; // Attack range or trigger distance
    public Transform player; // Assign in Inspector or find at runtime

    private bool barShown = false;

    public static BossBar Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (barRoot != null) barRoot.SetActive(false);
        if (lord != null && bossSlider != null)
        {
            bossSlider.maxValue = lord.MaxHealth;
            bossSlider.value = lord.currentHealth;
        }
        if (player == null)
        {
            var go = GameObject.FindGameObjectWithTag("Player");
            if (go != null) player = go.transform;
        }
    }

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!barShown && player != null && lord != null)
        {
            float dist = Mathf.Abs(player.position.x - lord.transform.position.x);
            if (dist <= showDistance)
            {
                if (barRoot != null) barRoot.SetActive(true);
                barShown = true;
            }
        }
        if (barShown && bossSlider != null && lord != null)
        {
            bossSlider.value = lord.currentHealth;
        }
    }

    public void HideBar()
    {
        if (barRoot != null) barRoot.SetActive(false);
        barShown = false;
    }
    
    public void ShowBar()
    {
        if (barRoot != null) barRoot.SetActive(true);
        barShown = true;
    }
}

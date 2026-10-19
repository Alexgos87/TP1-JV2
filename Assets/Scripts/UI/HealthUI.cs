using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        Finder.EventChannels.OnPlayerHealthChanged += UpdateHealth;
    }
    
    private void OnDisable()
    {
        var eventChannels = Finder.EventChannels;
        if (eventChannels != null)
            eventChannels.OnPlayerHealthChanged -= UpdateHealth;
    }

    private void UpdateHealth(int health)
    {
        text.text = health.ToString();
    }
}
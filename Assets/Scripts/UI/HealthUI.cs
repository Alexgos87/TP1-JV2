using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private int digits = 7;

    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        Finder.EventChannels.OnPlayerHealthChanged += UpdateScore;
    }
    
    private void OnDisable()
    {
        var eventChannels = Finder.EventChannels;
        if (eventChannels != null)
            eventChannels.OnPlayerHealthChanged -= UpdateScore;
    }

    private void UpdateScore(int score)
    {
        text.text = score.ToString().PadLeft(digits, '0');
    }
}
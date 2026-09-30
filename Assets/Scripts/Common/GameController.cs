using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int playerHealth = 50;

    private void OnEnable()
    {
        Finder.EventChannels.OnPlayerHealthChanged += OnPlayerHealthChanged;
    }

    private void OnDisable()
    {
        var eventChannels = Finder.EventChannels;
        if (eventChannels != null)
            eventChannels.OnPlayerHealthChanged -= OnPlayerHealthChanged;
    }

    private void OnPlayerHealthChanged(int _playerHealth)
    {
        this.playerHealth += _playerHealth;
        Finder.EventChannels.PublishPlayerHealthChanged(this.playerHealth);
    }
}
using System;
using UnityEngine;
using UnityEngine.Events;

// TODO : Ajouter tous vos canaux événementiels ici.
//        Consultez les notes de cours si vous avez oublié comment faire.
public class EventChannels : MonoBehaviour
{
    [SerializeField] private UnityEvent<int> onPlayerHealthChanged = new();
    [Header("projectile events")]
    [SerializeField] private UnityEvent<int> onNumberOfMisslesChanged = new();
    [Header("Game events")]
    [SerializeField] private UnityEvent<bool> onGameEnded = new();
    public event UnityAction<int> OnPlayerHealthChanged
    {
        add => onPlayerHealthChanged.AddListener(value);
        remove => onPlayerHealthChanged.RemoveListener(value);
    }

    public event UnityAction<int> OnNumberOfMisslesChanged
    {
        add => onNumberOfMisslesChanged.AddListener(value);
        remove => onNumberOfMisslesChanged.RemoveListener(value);
    }
    
    public event UnityAction<bool> OnGameEnded
    {
        add => onGameEnded.AddListener(value);
        remove => onGameEnded.RemoveListener(value);
    }

    public void PublishPlayerHealthChanged(int playerHealth)
    {
        onPlayerHealthChanged.Invoke(playerHealth);
    }

    public void PublishMissilesChanged(int missilesNumber)
    {
        onNumberOfMisslesChanged.Invoke(missilesNumber);
    }
    
    public void PublishGameEnded(bool isVictory)
    {
        onGameEnded.Invoke(isVictory);
    }
}
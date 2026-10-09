using UnityEngine;
using UnityEngine.Events;

// TODO : Ajouter tous vos canaux événementiels ici.
//        Consultez les notes de cours si vous avez oublié comment faire.
public class EventChannels : MonoBehaviour
{
    [SerializeField] private UnityEvent<int> onPlayerHealthChanged = new();

    public event UnityAction<int> OnPlayerHealthChanged
    {
        add => onPlayerHealthChanged.AddListener(value);
        remove => onPlayerHealthChanged.RemoveListener(value);
    }

    public void PublishPlayerHealthChanged(int playerHealth)
    {
        onPlayerHealthChanged.Invoke(playerHealth);
    }
    [Header("projectile events")] private UnityEvent<Bullet> onCollision = new();

    public event UnityAction<Bullet> OnCollision
    {
        add => onCollision.AddListener(value);
        remove => onCollision.RemoveListener(value);
    }

    public void PublishBulletCollision(Bullet bullet)
    {
        onCollision.Invoke(bullet);
    }
}
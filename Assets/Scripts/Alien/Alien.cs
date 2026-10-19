using UnityEngine;

public class Alien : MonoBehaviour, IHurtable
{
    [SerializeField] private int damageToPlayer = 10;

    private void OnEnable()
    {
        Finder.EventChannels.OnGameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        var eventChannels = Finder.EventChannels;
        if (eventChannels != null)
            eventChannels.OnGameEnded -= OnGameEnded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (collision.gameObject.TryGetComponent(out IHurtable player))
            player.TakeDamage(damageToPlayer);

        Die();
    }
    public void TakeDamage(int damage)
    {
        Die();
    }

    private void Die()
    {
        if (!gameObject.activeSelf) return;
        Finder.ObjectPools.AlienExplosion.Place(transform.position);
        Finder.ObjectPools.Alien.Release(gameObject);
    }
    private void OnGameEnded(bool isVictory)
    {
        Die();
    }
}
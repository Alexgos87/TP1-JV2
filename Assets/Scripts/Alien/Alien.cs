using UnityEngine;

public class Alien : MonoBehaviour, IHurtable
{
    [SerializeField] private int damageToPlayer = 10;

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
        Finder.ObjectPools.Alien.Release(gameObject);
        gameObject.SetActive(false);
    }
}

using UnityEngine;

public class Alien : MonoBehaviour, IHurtable
{
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

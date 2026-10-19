using UnityEngine;

public class SpaceMarineHealth : MonoBehaviour, IHurtable
{
    [SerializeField] private int startHealth = 50;
    [SerializeField] private int maxHealth = 100;

    private int health;

    public bool IsDead => health <= 0;

    private void Awake()
    {
        health = startHealth;
    }

    private void Start()
    {
        Finder.EventChannels.PublishPlayerHealthChanged(health);
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(health - damage, 0);
        Finder.EventChannels.PublishPlayerHealthChanged(health);

        if (IsDead)
        {
            // TODO : game over
        }
    }

    public void Heal(int amount)
    {
        if (IsDead) return;

        health = Mathf.Min(health + amount, maxHealth);
        Finder.EventChannels.PublishPlayerHealthChanged(health);
    }
}
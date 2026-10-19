
using Assets.Scripts.Common.Collectables;
using UnityEngine;

public class Portal : MonoBehaviour, IHurtable
{
    [Header("Portal Settings")]
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int maxAlien = 20;

    [Header("Collectables")]
    [SerializeField] private CollectablesSpawner collectablesSpawner;

    private ObjectPool alienPool;
    private int health;

    public int Health
    {
        get => health;
        set => health = value;
    }

    private void Awake()
    {
        health = maxHealth;

        if (Finder.ObjectPools == null)
        {
            Debug.LogError("Finder.ObjectPools est null.");
            return;
        }

        alienPool = Finder.ObjectPools.Alien;

        if (alienPool == null)
        {
            Debug.LogError("La pool Alien est null.");
        }
    }

    private void OnEnable()
    {
        health = maxHealth;

        var gameController = Finder.GameController;

        if (gameController != null)
            gameController.RegisterPortal(this);
    }

    private void OnDisable()
    {
        var gameController = Finder.GameController;

        if (gameController != null)
            gameController.UnRegisterPortal(this);
    }

    public void TakeDamage(int amount)
    {
        ApplyDamage(amount);
    }

    public void TakeDamage(float damage)
    {
        ApplyDamage(Mathf.RoundToInt(damage));
    }

    private void ApplyDamage(int amount)
    {
        if (health <= 0)
            return;

        health -= amount;

        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Portal mort");

        collectablesSpawner.SpawnRandomCollectable(transform.position);

        Debug.Log("Spawn du collectible demandé");

        gameObject.SetActive(false);
    }

    public void SpawnAlien()
    {
        alienPool ??= Finder.ObjectPools?.Alien;

        if (alienPool == null)
            return;

        if (alienPool.ActiveCount < maxAlien)
        {
            alienPool.Place(transform.position);
        }
    }
}

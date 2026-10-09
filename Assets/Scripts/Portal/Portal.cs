using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;

    private int health;
    private ObjectPool alienPool;

    private void Awake()
    {
        health = maxHealth;
    }

    private void OnEnable()
    {
        health = maxHealth;

        var gameController = Finder.GameController;
        if (gameController != null)
            gameController.RegisterPortal(this);
        else
            Debug.LogWarning("GameController introuvable dans OnEnable : portail non enregistr�.", this);
    }

    private void OnDisable()
    {
        var gameController = Finder.GameController;
        if (gameController != null)
            gameController.UnRegisterPortal(this);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            gameObject.SetActive(false);
    }

    public void SpawnAlien()
    {
        alienPool ??= Finder.ObjectPools?.AlienObjectPool;
        if (alienPool == null) return;

        alienPool.Place(transform.position);
    }
}
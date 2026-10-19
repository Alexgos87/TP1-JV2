using UnityEngine;

public class Portal : MonoBehaviour, IHurtable
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int maxAlien = 20;          // Nombre max d'aliens actifs
    private ObjectPool alienPool;
    private int health;

    private void Awake()
    {
        health = maxHealth;

        if (Finder.ObjectPools == null)
        {
            return;
        }

        alienPool = Finder.ObjectPools.Alien;
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
        // Le GameController peut deja etre detruit (fin du Play mode / changement de scene)
        var gameController = Finder.GameController;
        if (gameController != null)
            gameController.UnRegisterPortal(this);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Finder.ObjectPools.PortalExplosion.Place(transform.position);
            gameObject.SetActive(false);
        }
            
    }

    public void SpawnAlien()
    {
        if (alienPool == null) return; // Defensive : évite les appels si le pool est manquant
        if (alienPool.ActiveCount < maxAlien)
        {
            alienPool.Place(transform.position);
        }
    }
}
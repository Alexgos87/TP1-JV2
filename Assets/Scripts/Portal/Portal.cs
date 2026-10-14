using UnityEngine;

public class Portal : MonoBehaviour, IHurtable
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int maxAlien = 20;          // Nombre max d'aliens actifs
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
            Debug.LogError("Finder.ObjectPools est null (GameController introuvable, composant absent, ou Finder pas encore initialisé).");
            return;
        }

        alienPool = Finder.ObjectPools.Alien;
        if (alienPool == null)
        {
            Debug.LogError("ObjectPools existe, mais le champ 'Alien' est null (non assigné ou pool pas encore créé).");
        }
    }

    private void Update()
    {
        if (Health <= 0)
        {
            this.gameObject.SetActive(false);
        }
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
        // Le GameController peut deja etre detruit (fin du Play mode / changement de scene)
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
        alienPool ??= Finder.ObjectPools?.Alien;
        if (alienPool == null) return; // Defensive : évite les appels si le pool est manquant
        if (alienPool.ActiveCount < maxAlien)
        {
            alienPool.Place(transform.position);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= Mathf.RoundToInt(damage);
    }
}
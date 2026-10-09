using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int maxAlien = 20;          // Nombre max d'aliens actifs

    private int health;

    public int Health
    {
        get => health;
        set => health = value;
    }

    private ObjectPool alienPool;

    private int health;
    private ObjectPool alienPool;

    private void Awake()
    {
    private void Awake()
    {
        health = maxHealth;
        alienPool = Finder.ObjectPools?.Alien;
        if (alienPool == null)
        {
            Debug.LogError("Alien ObjectPool not found! Assurez-vous que l'objet 'GameController' possède le composant ObjectPools et que le champ 'Alien' est assigné.");
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
        alienPool ??= Finder.ObjectPools?.AlienObjectPool;
        if (alienPool == null) return; // Defensive : évite les appels si le pool est manquant
        if (alienPool.ActiveCount < maxAlien)
        {
            alienPool.Place(transform.position);
        }
    }
}
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private int maxAlien = 20;          // Nombre max d'aliens actifs
    [SerializeField] private int Health = 10;            // Santé du portail

    private ObjectPool alienPool;

    private void Awake()
    {
        // Sécurise la récupération : si ObjectPools ou Alien est null, alienPool sera null
        alienPool = Finder.ObjectPools.Alien;
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
        Finder.GameController.RegisterPortal(this);
    }

    private void OnDisable()
    {
        Finder.GameController.UnRegisterPortal(this);
    }

    public void SpawnAlien()
    {
        if (alienPool == null) return; // Défensive : évite les appels si le pool est manquant
        if (alienPool.ActiveCount < maxAlien)
        {
            alienPool.Place(transform.position);
        }
    }
}
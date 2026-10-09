using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private int maxAlien = 20;          // Nombre max d'aliens actifs
    [SerializeField] private int Health = 10;            // Sant� du portail

    private ObjectPool alienPool;

    private void Awake()
    {
        // S�curise la r�cup�ration : si ObjectPools ou Alien est null, alienPool sera null
        alienPool = Finder.ObjectPools.Alien;
        if (alienPool == null)
        {
            Debug.LogError("Alien ObjectPool not found! Assurez-vous que l'objet 'GameController' poss�de le composant ObjectPools et que le champ 'Alien' est assign�.");
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
        // Le GameController peut deja etre detruit (fin du Play mode / changement de scene)
        var gameController = Finder.GameController;
        if (gameController != null)
            gameController.UnRegisterPortal(this);
    }

    public void SpawnAlien()
    {
        if (alienPool == null) return; // D�fensive : �vite les appels si le pool est manquant
        if (alienPool.ActiveCount < maxAlien)
        {
            alienPool.Place(transform.position);
        }
    }
}
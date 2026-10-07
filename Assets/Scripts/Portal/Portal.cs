using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private int maxAlien = 20;          // Nombre max d'aliens actifs
    [SerializeField] private int Health = 10;            // Santé du portail


    private ObjectPool alienPool;

    private void Awake()
    {
        alienPool = Finder.ObjectPools.Alien;
        if (alienPool == null)
        {
            Debug.LogError("Alien ObjectPool not found!");
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
        if (alienPool.ActiveCount<maxAlien)
        {
            alienPool.Place(transform.position);
        }
    }
 
}
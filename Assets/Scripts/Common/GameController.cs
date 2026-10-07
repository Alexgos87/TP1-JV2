using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class GameController : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 0.5f; // Intervalle entre chaque spawn
    private List<Portal> activePortals = new();


    /// <summary>
    /// trouve tous les portails dans la scène et les stocke dans un tableau si il sont OnEnable
    /// </summary>
    public void RegisterPortal(Portal portal)
    {
        if (!activePortals.Contains(portal))
        {
            activePortals.Add(portal);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnAliensRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void UnRegisterPortal(Portal portal)
    {
        if (activePortals.Contains(portal))
        {
            activePortals.Remove(portal);
        }
    }

    /// <summary>
    /// Toutes les 0.5 secondes, 1 alien sort du portail (en coroutine).
    /// </summary>
    private IEnumerator SpawnAliensRoutine()
    {
        var waitForSeconds = new WaitForSeconds(spawnInterval);

        while (true)
        {
            if (activePortals.Count > 0)
            {
                var selectedPortal = activePortals[Random.Range(0, activePortals.Count)];
                selectedPortal.SpawnAlien();
            }

            yield return waitForSeconds;
        }
    }
}


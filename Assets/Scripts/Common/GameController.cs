using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    [SerializeField] private int playerHealth = 50;
    [SerializeField] private float spawnInterval = 0.5f; // Intervalle entre chaque spawn
    private List<Portal> activePortals = new();
    
    private void OnEnable()
    {
        StartCoroutine(SpawnAliensRoutine());
        Finder.EventChannels.OnPlayerHealthChanged += OnPlayerHealthChanged;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        var eventChannels = Finder.EventChannels;
        if (eventChannels != null)
            eventChannels.OnPlayerHealthChanged -= OnPlayerHealthChanged;
    }

    private void OnPlayerHealthChanged(int _playerHealth)
    {
        this.playerHealth += _playerHealth;
        Finder.EventChannels.PublishPlayerHealthChanged(this.playerHealth);
    }
    
    public void RegisterPortal(Portal portal)
    {
        if (!activePortals.Contains(portal))
        {
            activePortals.Add(portal);
        }
    }
    public void UnRegisterPortal(Portal portal)
    {
        if (activePortals.Contains(portal))
        {
            activePortals.Remove(portal);
        }
    }
    
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


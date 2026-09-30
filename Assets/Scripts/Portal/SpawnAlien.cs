using System.Collections;
using UnityEngine;

public class SpawnAlien : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 0.5f; // Time interval between spawns
    [SerializeField] private float HealthPoint = 10f; // Health points for the alien
    [SerializeField] private float MaxAlien = 20f; // Maximum number of aliens allowed in the scene

    private ObjectPool alienList; // Reference to the portal from which aliens will spawn

    private ObjectPool Portal;

    

    private void Awake()
    {
        // Get the reference to the portal from which aliens will spawn
        Portal = Finder.ObjectPools.Portal;
        alienList = Finder.ObjectPools.Alien;
    }

    private void OnEnable()
    {
        SpawnAliensRoutine(); // Start the coroutine to spawn aliens
    }


    private void Update()
    {

    }

    /// <summary>
    /// À toutes les 0.5 secondes, 1 alien sort de 1 portail choisi aléatoirement parmi ceux restants. Dans une méthode en coroutine
    /// </summary>
    private void SpawnAliensRoutine()
    {
        var waitForSeconds = new WaitForSeconds(spawnInterval);
        var waitUntil = new WaitUntil(() => Input.GetKeyDown(KeyCode.E));

        IEnumerator Routine()
        {
            while (true)
            {
                //temporary solution to wait for the 'E' key to be pressed before starting the spawning process
                yield return waitUntil;
                var alien = Finder.ObjectPools.Alien;

                if (alienList.ActiveCount < MaxAlien)
                {
                    var spawnPosition = Portal.GetRandomPortalPosition(); // Get a random position from the portal
                    var spawnedAlien = alien.Place(spawnPosition);
                    spawnedAlien.GetComponent<Alien>().HealthPoint = HealthPoint; // Set the health points for the alien
                }





                yield return waitForSeconds; // Wait for the specified interval before spawning the next alien
            }
        }


    }

}


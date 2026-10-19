
using UnityEngine;

namespace Assets.Scripts.Common.Collectables
{
    public class CollectablesSpawner : MonoBehaviour
    {
        public void SpawnRandomCollectable(Vector3 position)
        {
            Debug.Log("CollectablesSpawner appelé");


            //  On génère un nombre aléatoire pour selectionner un collectible random
            int randomNumber = UnityEngine.Random.Range(1, 4);

            switch (randomNumber)
            {
                case 1:
                    Debug.Log("Spawn Health");
                    Finder.ObjectPools.HealthCollectPool.Place(position);
                    break;

                case 2:
                    Debug.Log("Spawn Ammo");
                    Finder.ObjectPools.AmmoCollectPool.Place(position);
                    break;

                case 3:
                    Debug.Log("Spawn Shield");
                    Finder.ObjectPools.ShieldCollectPool.Place(position);
                    break;
            }
        }
    }
}

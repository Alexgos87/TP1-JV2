using UnityEngine;

namespace Assets.Scripts.Common.Collectables
{
    public class CollectablesSpawner : MonoBehaviour
    {
        public void SpawnRandomCollectable(Vector3 position)
        {
            //Debug.Log("CollectablesSpawner appelé");
            //Debug.Log("ObjectPools trouvé : " + Finder.ObjectPools);

            //if (Finder.ObjectPools != null)
            //{
            //    Debug.Log("Health : " + Finder.ObjectPools.HealthCollectPool);
            //    Debug.Log("Ammo : " + Finder.ObjectPools.AmmoCollectPool);
            //    Debug.Log("Shield : " + Finder.ObjectPools.ShieldCollectPool);
            //}
            //if (Finder.ObjectPools == null)
            //{
            //    Debug.LogError("ObjectPools est null !");
            //    return;
            //}

            int randomNumber = Random.Range(1, 4);

            switch (randomNumber)
            {
                case 1:
                    if (Finder.ObjectPools.HealthCollectPool == null)
                    {
                        Debug.LogError("HealthCollectPool est null !");
                        return;
                    }

                    Finder.ObjectPools.HealthCollectPool.Place(position);
                    break;

                case 2:
                    if (Finder.ObjectPools.AmmoCollectPool == null)
                    {
                        Debug.LogError("AmmoCollectPool est null !");
                        return;
                    }

                    Finder.ObjectPools.AmmoCollectPool.Place(position);
                    break;

                case 3:
                    if (Finder.ObjectPools.ShieldCollectPool == null)
                    {
                        Debug.LogError("ShieldCollectPool est null !");
                        return;
                    }

                    Finder.ObjectPools.ShieldCollectPool.Place(position);
                    break;
            }
        }
    }
}

using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    [Header("Entities")]
    [SerializeField] private ObjectPool alien;

    [Header("Fx")]
    [SerializeField] private ObjectPool alienExplosion;

    [Header("Projectiles")]
    [SerializeField] private ObjectPool MissileObjectPools;

    [Header("Alien")]
    [SerializeField] private ObjectPool AlienObjectPools;

    [Header("Collectables")]
    [SerializeField] private ObjectPool healthCollectPool;
    [SerializeField] private ObjectPool ammoCollectPool;
    [SerializeField] private ObjectPool shieldCollectPool;

    private static ObjectPools instance;

    private static ObjectPools Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindWithTag("ObjectPools")
                    .GetComponent<ObjectPools>();
            }

            return instance;
        }
    }

    // Entities
    public ObjectPool Alien => alien != null ? alien : AlienObjectPools;

    // Fx
    public ObjectPool AlienExplosion => alienExplosion;

    // Projectiles
    public ObjectPool MissileObjectPool => MissileObjectPools;

    // Alien pools
    public ObjectPool AlienObjectPool => AlienObjectPools;

    // Collectables
    public ObjectPool HealthCollectPool => healthCollectPool;
    public ObjectPool AmmoCollectPool => ammoCollectPool;
    public ObjectPool ShieldCollectPool => shieldCollectPool;
}

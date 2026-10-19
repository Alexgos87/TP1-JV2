using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    [Header("Entities")]
    [SerializeField] private ObjectPool alien;

    [Header("Fx")]
    [SerializeField] private ObjectPool alienExplosion;
    [SerializeField] private ObjectPool missileExplosion;
    [SerializeField] private ObjectPool portalExplosion;

    [Header("Projectiles")]
    [SerializeField] private ObjectPool missileObjectPools;
    [SerializeField] private ObjectPool bulletObjectPools;

    [Header("Collectables")]
    [SerializeField] private ObjectPool healthCollectPool;
    [SerializeField] private ObjectPool ammoCollectPool;
    [SerializeField] private ObjectPool shieldCollectPool;

    // Entities
    public ObjectPool Alien => alien;

    // Projectiles
    public ObjectPool MissileObjectPool => missileObjectPools;
    public ObjectPool BulletObjectPools => bulletObjectPools;

    // Fx
    public ObjectPool AlienExplosion => alienExplosion;
    public ObjectPool MissileExplosion => missileExplosion;
    public ObjectPool PortalExplosion => portalExplosion;

    // Collectables
    public ObjectPool HealthCollectPool => healthCollectPool;
    public ObjectPool AmmoCollectPool => ammoCollectPool;
    public ObjectPool ShieldCollectPool => shieldCollectPool;

    [ContextMenu("Debug Collectable Pools")]
    private void DebugCollectablePools()
    {
        Debug.Log($"ObjectPools : {gameObject.name}", this);
        Debug.Log($"Health : {healthCollectPool}", this);
        Debug.Log($"Ammo : {ammoCollectPool}", this);
        Debug.Log($"Shield : {shieldCollectPool}", this);
    }
}
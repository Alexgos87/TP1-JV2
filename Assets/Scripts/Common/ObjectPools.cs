using UnityEngine;

// TODO : Ajoutez toutes les références à vos ObjectPools ici.
//        Basez-vous sur le code existant.
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
    
    // Entities
    public ObjectPool Alien => alien;

    public ObjectPool MissileObjectPool => missileObjectPools;
    public ObjectPool BulletObjectPools => bulletObjectPools;
    
    // Fx
    public ObjectPool AlienExplosion => alienExplosion;
    public ObjectPool MissileExplosion => missileExplosion;
    public ObjectPool PortalExplosion => portalExplosion;
    
}
using UnityEngine;

// TODO : Ajoutez toutes les références à vos ObjectPools ici.
//        Basez-vous sur le code existant.
public class ObjectPools : MonoBehaviour
{
    [Header("Entities")]
    [SerializeField] private ObjectPool alien;
    [SerializeField] private ObjectPool bullet; // ajouter ceci

    [Header("Fx")]
    [SerializeField] private ObjectPool alienExplosion;

    [Header("Projectiles")]
    [SerializeField] private ObjectPool MissileObjectPools;

    [Header("Alien")]
    [SerializeField] private ObjectPool AlienObjectPools;

    
    private static ObjectPools instance;
    private static ObjectPools Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindWithTag("ObjectPools").GetComponent<ObjectPools>();
            return instance;
        }
    }
    
    // Entities
    public ObjectPool Alien => alien != null ? alien : AlienObjectPools;
    public ObjectPool BulletObjectPool => bullet; // exposer le pool de balles

    // Fx
    public ObjectPool AlienExplosion => alienExplosion;

    public ObjectPool MissileObjectPool => MissileObjectPools;

    // AlienPools
    public ObjectPool AlienObjectPool => AlienObjectPools;
}
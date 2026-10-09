using UnityEngine;

// TODO : Ajoutez toutes les références à vos ObjectPools ici.
//        Basez-vous sur le code existant.
public class ObjectPools : MonoBehaviour
{
    [Header("Entities")]
    [SerializeField] private ObjectPool alien;

    [Header("Fx")]
    [SerializeField] private ObjectPool alienExplosion;

    [Header("Projectiles")]
    [SerializeField] private ObjectPool bulletObjectPools;

    [Header("Alien")]
    [SerializeField] private ObjectPool AlienObjectPools;

    // Entities
    public ObjectPool Alien => alien;

    // Fx
    public ObjectPool AlienExplosion => alienExplosion;


    // AlienPools
    public ObjectPool AlienObjectPool => AlienObjectPools;
}
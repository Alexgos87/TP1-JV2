using UnityEngine;

// TODO : Ajoutez toutes les références à vos ObjectPools ici.
//        Basez-vous sur le code existant.
public class ObjectPools : MonoBehaviour
{
    [Header("Entities")]
    [SerializeField] private ObjectPool alien;

    [Header("Portals")]
    [SerializeField] private ObjectPool portal;

    [Header("Fx")]
    [SerializeField] private ObjectPool alienExplosion;

    // Entities
    public ObjectPool Alien => alien;

    // Portals
    public ObjectPool Portal => portal;

    // Fx
    public ObjectPool AlienExplosion => alienExplosion;
}
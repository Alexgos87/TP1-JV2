using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///  bouge a 200 de vitesse et fait un de degat, est un trigger pour les collisions. 
/// </summary>
public class Missile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float speed = 200; // Speed of the projectile
    [SerializeField] private int damage = 10; // Damage of the projectile
    [SerializeField] private float explosionRadius = 20f; // Radius of the explosion

    private Collider[] results = new Collider[32];
    private HashSet<IHurtable> damaged = new HashSet<IHurtable>();
    
    private void FixedUpdate()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// D�clanche un �v�nement lorsqu'un projectile entre en collision avec un ennemi. Applique des d�g�ts � l'ennemi et d�truit le projectile apr�s la collision.
    /// </summary>
    /// <param name="other">objet avec lequel le projectile entre en collision</param>
    private void OnTriggerEnter(Collider other)
    {
        // Correction : OverlapSphereNonAlloc n�cessite un tableau de r�sultats en argument
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, results);
        
        damaged.Clear();
        for (int i = 0; i < hitCount; i++)
        {
            Collider hit = results[i];
            // Utiliser GetComponentInParent pour que les colliders enfants trouvent le script sur la racine
            if (hit.GetComponentInParent<IHurtable>() is IHurtable hurtable && damaged.Add(hurtable))
            {
                hurtable.TakeDamage(damage);
            }
        }
        Finder.ObjectPools.MissileExplosion.Place(transform.position);
        Finder.ObjectPools.MissileObjectPool.Release(gameObject);
    }
}







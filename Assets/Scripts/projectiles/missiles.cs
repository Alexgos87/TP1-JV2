using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///  bouge a 200 de vitesse et fait 1 de degat, est un trigger pour les collisions. 
/// </summary>
public class Missiles : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float speed = 200; // Speed of the projectile
    [SerializeField] private int damage = 10; // Damage of the projectile
    [SerializeField] private float ExplosionRadius = 5f; // Radius of the explosion


    private void FixedUpdate()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Déclanche un événement lorsqu'un projectile entre en collision avec un ennemi. Applique des dégâts à l'ennemi et détruit le projectile après la collision.
    /// </summary>
    /// <param name="other">objet avec lequel le projectile entre en collision</param>
    private void OnTriggerEnter(Collider other)
    {
        // Correction : OverlapSphereNonAlloc nécessite un tableau de résultats en argument
        Collider[] results = new Collider[32]; // Taille arbitraire, à ajuster selon le besoin
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, ExplosionRadius, results);

        // Éviter de toucher deux fois le même objet s'il a plusieurs colliders
        var damaged = new HashSet<IHurtable>();

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







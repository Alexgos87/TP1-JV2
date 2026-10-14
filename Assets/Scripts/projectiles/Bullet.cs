using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///  bouge a 200 de vitesse et fait 1 de degat, est un trigger pour les collisions. 
/// </summary>
public class Bullet : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float speed = 200; // Speed of the projectile
    [SerializeField] private float damage = 1; // Damage of the projectile


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
        // if collide with a IHurtable object, apply damage and destroy the projectile
        if (other.TryGetComponent<IHurtable>(out IHurtable hurtable))
        {
            hurtable.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            // If it doesn't, destroy the projectile
            Destroy(gameObject);
        }
    }


}







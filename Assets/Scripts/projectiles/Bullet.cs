using UnityEngine;

/// <summary>
///  bouge a 200 de vitesse et fait un de degat, est un trigger pour les collisions. 
/// </summary>
public class Bullet : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float speed = 200; // Speed of the projectile
    [SerializeField] private int damage = 1; // Damage of the projectile


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
        // if collide with a IHurtable object, apply damage and destroy the projectile
        if (other.TryGetComponent<IHurtable>(out var hurtable))
            hurtable.TakeDamage(damage);

        Finder.ObjectPools.BulletObjectPools.Release(gameObject);
    }


}







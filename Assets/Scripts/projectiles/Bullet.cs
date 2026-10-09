using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float speed = 200; // Speed of the projectile
    [SerializeField] private float damage = 1; // Damage of the projectile
    [SerializeField] private float Rayon_Collider = 0.5f; // Radius of the collider

    [Header("Input Settings")]
    [SerializeField] private InputActionReference moveAction;


    private void FixedUpdate()
    {
        // Move the projectile forward
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);

    }


    
}



using UnityEngine;

public class PhysicsSettings : MonoBehaviour
{
    [SerializeField] private Vector3 gravity = new(x: 0, y: -9.81f, z: 0);

#if !UNITY_EDITOR

    private void Awake()
    {
        Physics.gravity = gravity;
    }
    
#else
    
    private void Update()
    {
        Physics.gravity = gravity;
    }
    
#endif
}
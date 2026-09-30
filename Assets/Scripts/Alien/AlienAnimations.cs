using UnityEngine;
using UnityEngine.AI;

public class AlienAnimations : MonoBehaviour
{
    private static readonly int Walk = Animator.StringToHash("Walk");
    
    [Header("Movement")]
    [SerializeField] private float maximumSpeed = 25;
    [SerializeField, Range(0, 1)] private float walkThreshold = 0.1f;
    [SerializeField, Range(0, 1)] private float walkStartDamping = 0.3f;
    [SerializeField, Range(0, 1)] private float walkStopDamping = 0.15f;
    
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        // Get components.
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Prevent script from running if no Animator or NavMeshAgent is found.
        enabled = animator != null && navMeshAgent != null;
    }

    private void Update()
    {
        UpdateMouvementAnimations();
    }
    
    private void UpdateMouvementAnimations()
    {
        var normalizedSpeed = Mathf.InverseLerp(0, maximumSpeed, navMeshAgent.velocity.magnitude);

        if (normalizedSpeed > walkThreshold)
        {
            animator.SetFloat(Walk, normalizedSpeed, walkStartDamping, Time.deltaTime);
        }
        else if (normalizedSpeed < walkThreshold)
        {
            animator.SetFloat(Walk, normalizedSpeed, walkStopDamping, Time.deltaTime);
        }
    }
}
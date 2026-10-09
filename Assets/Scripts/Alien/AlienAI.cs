using UnityEngine;
using UnityEngine.AI;

public class AlienAI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private NavMeshAgent agent;
    
    private void Awake()
    {
        agent  = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        agent.SetDestination(player.transform.position);
    }
}

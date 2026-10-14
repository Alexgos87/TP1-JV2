using UnityEngine;
using UnityEngine.AI;

public class AlienAI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private NavMeshAgent agent;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        FindPlayer();
    }

    private void OnEnable()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        if (player == null)
            player = FindAnyObjectByType<SpaceMarinePlayer>()?.gameObject;
    }
    
    private void Update()
    {
        if (agent == null || player == null)
        {
            FindPlayer();
            return;
        }

        agent.SetDestination(player.transform.position);
    }
}

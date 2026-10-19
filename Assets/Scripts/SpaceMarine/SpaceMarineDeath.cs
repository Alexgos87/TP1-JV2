using UnityEngine;

public class SpaceMarineDeath : MonoBehaviour
{

    [SerializeField] private float pushForce = 5f;
    
    private Animator animator;
    private CharacterController characterController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        Finder.EventChannels.OnGameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        var eventChannels = Finder.EventChannels;
        if (eventChannels != null)
            eventChannels.OnGameEnded -= OnGameEnded;
    }

    private void OnGameEnded(bool isVictory)
    {
        if (isVictory) return;

        animator.enabled = false;
        characterController.enabled = false;

        gameObject.AddComponent<Rigidbody>();
    }
}
using UnityEngine;

public class SpaceMarineDeath : MonoBehaviour
{

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

        var body = gameObject.AddComponent<Rigidbody>();
    }
}
using UnityEngine;

public class SpaceMarineDeath : MonoBehaviour
{
    [SerializeField] private float pushForce = 5f;

    private Animator animator;
    private CharacterController characterController;
    private Rigidbody deathRigidbody;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        initialPosition = transform.position;
        initialRotation = transform.rotation;
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
        if (isVictory)
            return;

        if (animator != null)
            animator.enabled = false;

        if (characterController != null)
            characterController.enabled = false;

        if (deathRigidbody == null)
        {
            deathRigidbody = gameObject.AddComponent<Rigidbody>();
            deathRigidbody.useGravity = true;
        }
    }

    public void ResetPlayer()
    {
        // Supprimer le Rigidbody ajouté à la mort.
        if (deathRigidbody != null)
        {
            Destroy(deathRigidbody);
            deathRigidbody = null;
        }

        // Réinitialiser la position et la rotation.
        transform.SetPositionAndRotation(initialPosition, initialRotation);

        // Réactiver les composants du joueur.
        if (animator != null)
            animator.enabled = true;

        if (characterController != null)
            characterController.enabled = true;
    }
}
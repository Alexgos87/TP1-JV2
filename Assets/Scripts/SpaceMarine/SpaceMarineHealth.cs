using UnityEngine;

public class SpaceMarineHealth : MonoBehaviour, IHurtable
{
    [SerializeField] private int startHealth = 50;
    [SerializeField] private int maxHealth = 100;
    
    [Header("Sounds")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    private int health;

    public bool IsDead => health <= 0;

    private AudioSource audioSource;

    private void Awake()
    {
        health = startHealth;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Finder.EventChannels.PublishPlayerHealthChanged(health);
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(health - damage, 0);
        Finder.EventChannels.PublishPlayerHealthChanged(health);
        audioSource.PlayOneShot(hurtSound);

        if (IsDead)
        {
            audioSource.PlayOneShot(deathSound);
            Finder.EventChannels.PublishGameEnded(false);
        }
    }
}
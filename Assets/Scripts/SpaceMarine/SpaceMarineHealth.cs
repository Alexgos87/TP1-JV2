using System.Collections;
using UnityEngine;

public class SpaceMarineHealth : MonoBehaviour, IHurtable
{
    [SerializeField] private int startHealth = 50;
    [SerializeField] private int maxHealth = 100;
    
    [Header("Sounds")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    private int health;


    private bool isInvulnerable = false;
    private bool IsDead => health <= 0;

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
        if (IsDead || isInvulnerable) return;

        health = Mathf.Max(health - damage, 0);
        Finder.EventChannels.PublishPlayerHealthChanged(health);

        if (audioSource != null && hurtSound != null)
            audioSource.PlayOneShot(hurtSound);

        if (IsDead)
        {
            if (audioSource != null && deathSound != null)
                audioSource.PlayOneShot(deathSound);

            Finder.EventChannels.PublishGameEnded(false);

        }
    }
    public void Heal()
    {
        health = Mathf.Min(health + 25, maxHealth);
        Finder.EventChannels.PublishPlayerHealthChanged(health);
    }
    public void ActivateArmor()
    {
        StartCoroutine(InvulnerabilityCoroutine());
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;

        yield return new WaitForSeconds(5f);

        isInvulnerable = false;
    }
    public void ResetHealth()
    {
        health = startHealth;
        isInvulnerable = false;

        StopAllCoroutines();

        Finder.EventChannels.PublishPlayerHealthChanged(health);
    }
}
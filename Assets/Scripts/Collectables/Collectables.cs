using UnityEngine;
using Assets.Scripts.Common.Collectables;

namespace Assets.Scripts.Collectables
{
    public class Collectable : MonoBehaviour
    {
        private bool collected;
        private AudioSource audioSource;
        private AudioClip collectSound;

        private void Awake()
        {
            audioSource = FindFirstObjectByType<SpaceMarineHealth>()
                ?.GetComponent<AudioSource>();

            // Récupère le son assigné au collectible.
            AudioSource collectibleAudio = GetComponent<AudioSource>();

            if (collectibleAudio != null)
            {
                collectSound = collectibleAudio.clip;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (collected)
                return;

            // Vérifie que le joueur entre en contact avec le collectible.
            SpaceMarineHealth playerHealth =
                other.GetComponentInParent<SpaceMarineHealth>();

            if (playerHealth == null)
                return;

            ICollectable collectable = GetComponent<ICollectable>();

            if (collectable == null)
            {
                Debug.LogError(
                    "Aucun ICollectable trouvé sur " + gameObject.name
                );
                return;
            }

            collected = true;

            // Applique l'effet du collectible.
            collectable.UseEffect();

            // Joue le son depuis le joueur.
            if (audioSource != null && collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);
            }
            

            // Désactive le collectible.
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            collected = false;
        }
    }
}
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
    [SerializeField] private AudioClip gameEndMusic;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Finder.EventChannels.OnGameEnded += PlayEndGamMusic;
    }

    private void OnDisable()
    {
        var eventChannels = Finder.EventChannels;
        if (eventChannels != null)
            eventChannels.OnGameEnded -= PlayEndGamMusic;
    }

    private void PlayEndGamMusic(bool isVictory)
    {
        audioSource.clip = gameEndMusic;
        audioSource.loop = false;
        audioSource.Play();
    }
}
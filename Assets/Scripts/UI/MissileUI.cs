using TMPro;
using UnityEngine;

public class MissileUI : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        Finder.EventChannels.OnNumberOfMisslesChanged += UpdateMissiles;
    }

    private void OnDisable()
    {
        var eventChannels = Finder.EventChannels;
        if (eventChannels != null)
            eventChannels.OnNumberOfMisslesChanged -= UpdateMissiles;
    }

    private void UpdateMissiles(int missiles)
    {
        text.text = missiles.ToString();
    }
}
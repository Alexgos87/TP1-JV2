using UnityEngine;

public class SpaceMarineHead : MonoBehaviour
{

    private Rigidbody rb;
    private Collider headCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        headCollider = GetComponent<Collider>();
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

        transform.SetParent(null, true);

        headCollider.enabled = true;
        rb.isKinematic = false;
        
    }
}
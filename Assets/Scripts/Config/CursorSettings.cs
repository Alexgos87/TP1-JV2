using UnityEngine;

public class CursorSettings : MonoBehaviour
{
    [SerializeField] private CursorLockMode mode = CursorLockMode.Locked;

    private void Awake()
    {
        Cursor.lockState = mode;
    }
}
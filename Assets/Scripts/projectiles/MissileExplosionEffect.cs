using System.Collections;
using UnityEngine;

public class MissileExplosionEffect : MonoBehaviour
{
    [SerializeField] private float duration = 3f;

    private ObjectPool pool;

    private void Awake()
    {
        pool = GetComponentInParent<ObjectPool>();
    }

    private void OnEnable()
    {
        StartCoroutine(StopExplosion());
    }

    private IEnumerator StopExplosion()
    {
        yield return new WaitForSeconds(duration);
        pool.Release(gameObject);
    }
}

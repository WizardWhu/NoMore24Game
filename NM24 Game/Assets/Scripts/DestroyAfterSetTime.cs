using System.Collections;
using UnityEngine;

public class DestroyAfterSetTime : MonoBehaviour
{
    [SerializeField] private float Timer = 30;

    private void Awake()
    {
        StartCoroutine(TimedDestroy());
    }

    IEnumerator TimedDestroy()
    {
        yield return new WaitForSeconds(Timer);
        Destroy(gameObject);
    }
}

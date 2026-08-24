using System;
using UnityEngine;

public class DripManager : MonoBehaviour
{
    [Tooltip ("Decides the minimum amount of time in minutes a freshly poored bucket could take before it needs to be dumped again")]
    [SerializeField] private float MinMinutes = 30f;

    [Tooltip("Decides the maximum amount of time in minutes a freshly poored bucket could take before it needs to be dumped again")]
    [SerializeField] private float MaxMinutes = 60f;

    private float TotalSeconds = 0f;
    private float timePassed = 0f;

    public static event Action<float> TimePassed;
    public static event Action OnPlayerLose;

    private bool isDripping = false;

    // Update is called once per frame
    void Update()
    {
        if(isDripping) 
        {
            timePassed += Time.deltaTime;
            TimePassed?.Invoke(TotalSeconds / timePassed);
        }

        if(timePassed >= TotalSeconds && isDripping)
        {
            OnPlayerLose?.Invoke();
            Debug.Log("Lost!");
        }
    }

    public void StartTimer()
    {
        isDripping = true;
    }

    public void PauseTimer()
    {
        isDripping = false;
    }
    public void ResetTimer()
    {
        TotalSeconds = UnityEngine.Random.Range(MinMinutes * 60f, MaxMinutes * 60f);
    }
}

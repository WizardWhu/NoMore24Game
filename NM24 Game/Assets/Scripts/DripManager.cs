using UnityEngine;

public class DripManager : MonoBehaviour
{
    [Tooltip ("Decides the minimum amount of time in minutes a freshly poored bucket could take before it needs to be dumped again")]
    [SerializeField] private float MinMinutes = 30f;

    [Tooltip("Decides the maximum amount of time in minutes a freshly poored bucket could take before it needs to be dumped again")]
    [SerializeField] private float MaxMinutes = 60f;

    private float TotalSeconds = 0f;
    private float timePassed = 0f;

    private bool isDripping = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDripping = false)
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
        TotalSeconds = Random.Range(MinMinutes * 60f, MaxMinutes * 60f);
    }
}

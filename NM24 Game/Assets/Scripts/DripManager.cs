using System;
using UnityEngine;
using System.Collections.Generic;
public enum Dripstate{
    Dripping,
    NotDripping
};
public class DripManager : MonoBehaviour
{
    [Header("Bucket Settings")]
    [Tooltip ("Decides the minimum amount of time in minutes a freshly poored bucket could take before it needs to be dumped again")]
    [SerializeField] private float MinMinutes = 30f;

    [Tooltip("Decides the maximum amount of time in minutes a freshly poored bucket could take before it needs to be dumped again")]
    [SerializeField] private float MaxMinutes = 60f;

    [Header("Water Drop Settings")]
    [Tooltip("Decides the maximum amount of time to wait between waterdrops")]
    [SerializeField] private float MaxSecondsBetweenDrops;

    [Tooltip("Decides the minimum amount of time to wait between waterdrops")]
    [SerializeField] private float MinSecondsBetweenDrops;

    private float TotalSeconds = 0f;
    public static event Action<float> TimePassed;
    public static event Action OnPlayerLose;

    private List<WaterDrop> AllWaterDrops;
    private Dripstate currentDripstate = Dripstate.NotDripping;
    // Update is called once per frame
    void Update()
    {
        /*
        if(currentDripstate == Dripstate.Dripping) 
        {
            timePassed += Time.deltaTime;
            TimePassed?.Invoke(TotalSeconds / timePassed);
        }

        if(timePassed >= TotalSeconds && currentDripstate == Dripstate.Dripping)
        {
            OnPlayerLose?.Invoke();
            Debug.Log("Lost!");
        }
        */
    }

    public void StartTimer()
    {
        ResetTimer();
        PopulateDripList();
        currentDripstate = Dripstate.Dripping;
    }

    public void PauseTimer()
    {
        
    }
    public void ResetTimer()
    {
        TotalSeconds = UnityEngine.Random.Range(MinMinutes * 60f, MaxMinutes * 60f);
    }


    private void PopulateDripList()
    {
        List<float> allDripTimes = new List<float>();
        allDripTimes.Add(TotalSeconds);

        float offset = 0;
        float selectedDripTime = 0f;
        while (allDripTimes.Count > 0)
        {
            offset = 0;
            if (allDripTimes[0] > MaxSecondsBetweenDrops)
            {
                selectedDripTime = allDripTimes[0];
                offset = allDripTimes[0] / 2f;
                offset += UnityEngine.Random.Range(0f, Mathf.Floor(offset / 3f));

                allDripTimes.RemoveAt(0);
                allDripTimes.Insert(0, offset);
                allDripTimes.Insert(1, selectedDripTime - offset);
            }
        }
        Debug.Log(allDripTimes);
    }
}

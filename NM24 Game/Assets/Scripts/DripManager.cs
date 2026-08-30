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
    public static event Action<WaterDrop> WaterDropped;
    public static event Action OnPlayerLose;

    public List<WaterDrop> AllWaterDrops;
    private Dripstate currentDripstate = Dripstate.NotDripping;

    private float timePassed = 0f;
    void Start()
    {
        StartTimer();
    }
    // Update is called once per frame
    void Update()
    {
        
        if(currentDripstate == Dripstate.Dripping) 
        {
            timePassed += Time.deltaTime;
        }

        if(timePassed >= AllWaterDrops[0].GetSecondsTillDrop() && currentDripstate == Dripstate.Dripping)
        {
            WaterDropped?.Invoke(AllWaterDrops[0]);
            AllWaterDrops.RemoveAt(0);
            timePassed = 0f;
        }

        if(currentDripstate == Dripstate.Dripping && AllWaterDrops.Count <= 0)
        {
            OnPlayerLose?.Invoke();
        }
        
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

    List<float> allDripTimes = new List<float>();
    List<float> finalDripTimes = new List<float>();

    List <float> allBucketValues = new List<float>();
    List<float> LastBucketValues = new List<float>();
    List<float> NewBucketValues = new List<float>();

    private void PopulateDripList()
    {
        AllWaterDrops = new List<WaterDrop>();

        allDripTimes.Add(TotalSeconds);

        float offset = 0;
        float selectedDripTime = 0f;

        while (allDripTimes.Count > 0)
        {
            if (allDripTimes[0] <= MaxSecondsBetweenDrops && allDripTimes[0] >= MinSecondsBetweenDrops)
            {
                finalDripTimes.Add(allDripTimes[0]);
                allDripTimes.RemoveAt(0);
                continue;
            }

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

       LastBucketValues.Add(1f);
       float selectedBucketValue = 0;
       while(LastBucketValues.Count + NewBucketValues.Count < finalDripTimes.Count)
       {
            selectedBucketValue = LastBucketValues[0];

            offset = selectedBucketValue / 2f;
            offset += UnityEngine.Random.Range(0f, offset / 4f);

            NewBucketValues.Add(offset);
            NewBucketValues.Add(selectedBucketValue - offset);
            LastBucketValues.RemoveAt(0);
            
            if(LastBucketValues.Count < 1)
            {
                LastBucketValues.AddRange(NewBucketValues);
                NewBucketValues.Clear();
            }
            
       }
        allBucketValues.AddRange(LastBucketValues);
        allBucketValues.AddRange(NewBucketValues);


        for (int i = 0; i < finalDripTimes.Count; i++)
        {
            WaterDrop currentDrop = new WaterDrop();
            currentDrop.SetSecondsTillDrop(finalDripTimes[i]);
            currentDrop.SetDropletVolume(allBucketValues[i]);
            AllWaterDrops.Add(currentDrop);
        }
    }
}

using UnityEngine;
using System;
public class BucketManager : MonoBehaviour
{
    [SerializeField] private float minAmount = 0f;
    [SerializeField] private float maxAmount = 1f;

    [SerializeField] private float fillSpeed = 1f;
    public Material WaterShader;
    float AmountFilled;
    bool BucketIsClearable = false;
    public static event Action OnPlayerLose;


    void Awake()
    {
        AmountFilled = minAmount;

        OnClickEmpty.OnMouseDown += ClearBucket;
    }

    void Start()
    {
        WaterShader.SetFloat("_Fullness", Mathf.Lerp(minAmount, maxAmount, AmountFilled));
    }
    public void AddWater(float amount)
    {
        AmountFilled += amount;
        if (AmountFilled >= maxAmount)
        {
            Debug.Log("Player Lost");
            OnPlayerLose?.Invoke();
            BucketIsClearable = false;
        }
        WaterShader.SetFloat("_Fullness", Mathf.Lerp(minAmount,maxAmount,AmountFilled));

    }

    public void ClearBucket()
    {
        if (BucketIsClearable)
        {
            AmountFilled = minAmount;
            WaterShader.SetFloat("_Fullness", Mathf.Lerp(minAmount, maxAmount, AmountFilled));
        }

    }


}

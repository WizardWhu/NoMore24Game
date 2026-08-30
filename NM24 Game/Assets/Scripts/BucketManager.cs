using UnityEngine;

public class BucketManager : MonoBehaviour
{
    [SerializeField] private float minAmount = 0f;
    [SerializeField] private float maxAmount = 1f;

    [SerializeField] private float fillSpeed = 1f;
    public Material WaterShader;
    float AmountFilled;

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
        WaterShader.SetFloat("_Fullness", Mathf.Lerp(minAmount,maxAmount,AmountFilled));
    }

    public void ClearBucket()
    {
        AmountFilled = minAmount;
        WaterShader.SetFloat("_Fullness", Mathf.Lerp(minAmount, maxAmount, AmountFilled));

    }


}

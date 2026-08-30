using UnityEngine;

public class BucketManager : MonoBehaviour
{
    [SerializeField] private float minAmount = 0f;
    [SerializeField] private float maxAmount = 1f;

    [SerializeField] private float fillSpeed = 1f;
    private Material WaterShader;
    float AmountFilled;

    void Awake()
    {
        AmountFilled = minAmount;
    }

    public void AddWater(float amount)
    {
        AmountFilled += amount;
        WaterShader.SetFloat("_Fullness", amount);
    }

  
}

using UnityEngine;

public class RainSpawner : MonoBehaviour
{
    [SerializeField] private GameObject WaterDroplet;
    
    private GameObject currentWaterDrop;

    private void Awake()
    {
        DripManager.WaterDropped += CreateDrop;
    }

    public void CreateDrop(WaterDrop currentWaterDrop)
    {
        this.currentWaterDrop = Instantiate(WaterDroplet, transform.position, Quaternion.identity);
        
        this.currentWaterDrop.GetComponent<FillBucketOnCollision>().SetCurrentDrop(currentWaterDrop);
    }
}

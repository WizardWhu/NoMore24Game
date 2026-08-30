using UnityEngine;

public class FillBucketOnCollision : MonoBehaviour
{
    private WaterDrop currentDrop;

    [SerializeField] private GameObject splashPrefab;
    public void SetCurrentDrop(WaterDrop currentDrop)
    {
        this.currentDrop = currentDrop;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Water")) return;

        collision.transform.GetComponent<BucketManager>().AddWater(currentDrop.GetDropletVolume());
        Instantiate(splashPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

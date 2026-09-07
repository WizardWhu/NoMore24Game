using UnityEngine;

public class AppearOnLose : MonoBehaviour
{
    private void Awake()
    {
        BucketManager.OnPlayerLose += SetActive;
    }

    private void OnDisable()
    {
        BucketManager.OnPlayerLose -= SetActive;
    }
    public void SetActive()
    {
        gameObject.SetActive(true);
    }
}

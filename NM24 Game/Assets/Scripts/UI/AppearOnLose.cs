using UnityEngine;

public class AppearOnLose : MonoBehaviour
{
    public GameObject target;
    void Start()
    {
        BucketManager.OnPlayerLose += MakeActive;
    }

    private void OnDestroy()
    {
        BucketManager.OnPlayerLose -= MakeActive;
    }
    public void MakeActive()
    {
        Debug.Log("Appeared!");
        target.SetActive(true);
    }
}

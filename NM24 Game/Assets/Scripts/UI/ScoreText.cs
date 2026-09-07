using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public GameManager gameManager;
    void Start()
    {
        if(gameManager == null)
        {
            gameManager = FindAnyObjectByType<GameManager>();
        }

        scoreText.text = Mathf.Floor(PlayerPrefs.GetFloat("PlayerScore")).ToString();
    }
}

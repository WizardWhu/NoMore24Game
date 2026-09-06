using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState
{
    NotStarted,
    Playing,
    GameLost
};
public class GameManager : MonoBehaviour
{
    /*This is the big center script for the whole game --
     * 
     * Saves data for the gamestate to know:
     * 
     *how much to fill the bucket, 
     *how much time is left, 
     *Player settings
     *how much money the player has made
     *how much time it's been since the player was last on,
     *if the player has started the game.
     *---------------------------------------------------------------------
     *it uses this data to recongnize what screen to put the player to, if water should keep dripping, and what settings to give the player
     */


    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }




    //Data To save
    public float timeLeft = 0f;

    private DateTime timeStampWhenLastQuit;

    public GameState currentGameState;

    public static event Action StartedPlaying;
    //Player preferences
    [SerializeField] private float defaultVolume = 0f;
    private float volumeControl = 0f;
    public DripManager dripManager;

    void Awake()
    {
        //Starts Singleton stuff on game load
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }


        //connects preferences and values on game load
        ConnectRecordedValues();

        SetToCorrectScene();



    }
    private void Update()
    {
        if(currentGameState == GameState.Playing)
        {
            timeLeft = dripManager.GetTimeLeft();
        }
    }
    private void SetToCorrectScene()
    {
        if (currentGameState == GameState.NotStarted)
        {
            if (SceneManager.GetActiveScene().buildIndex != 0) SceneManager.LoadScene(0);

        }
        else if(currentGameState == GameState.Playing)
        {
            if(SceneManager.GetActiveScene().buildIndex != 1) SceneManager.LoadScene(1);

            StartedPlaying?.Invoke();
            dripManager = FindAnyObjectByType<DripManager>();

            if(timeLeft != 0f)
            {
                dripManager.SetTimer(timeLeft);
            }

        }
        else if(currentGameState == GameState.GameLost)
        {
            //SceneManager.LoadScene(2);
        }
    }
    public void StartGame()
    {
        currentGameState = GameState.Playing;
        StartedPlaying?.Invoke();
        SetToCorrectScene();
    }
    private void ConnectRecordedValues()
    {
        //If the game has started already, connect the players values (score, timeleft etc.) to the scripts it needs to.
        //If not, set the default values
        if (!PlayerPrefs.HasKey("SavedGameState") || (PlayerPrefs.GetInt("SavedGameState") == 0))
        {
            SetDefaultValues();
        }
        else
        {
            SetValuesOnGameOpen();
        }

        //Do the same for preferences, but is checked seperately in case the player sets the preferences without starting the game.

        if (!PlayerPrefs.HasKey("volumeControl"))
        {
            SetDefaultPreferences();
        }
        else
        {
            SetPreferencesOnGameOpen();
        }
    }

    private void SetDefaultValues()
    {
        Debug.Log("Setting Default Values");
        PlayerPrefs.SetFloat("timeLeft", 0f);
        PlayerPrefs.SetString("timeStampWhenLastQuit", "");
        PlayerPrefs.SetInt("SavedGameState", 0);
        PlayerPrefs.Save();
    }
    private void SetValuesOnGameOpen()
    {
        Debug.Log("Setting Values for game return");

        int SavedGameState = PlayerPrefs.GetInt("SavedGameState");

        currentGameState = GameState.NotStarted;
        if (SavedGameState == 1)
        {
            currentGameState = GameState.Playing;
        }
        else if (SavedGameState == 2)
        {
            currentGameState = GameState.GameLost;
        }

        timeStampWhenLastQuit = DateTime.Parse(PlayerPrefs.GetString("timeStampWhenLastQuit"));
        timeLeft = PlayerPrefs.GetFloat("timeLeft");

        TimeSpan timePassed = timeStampWhenLastQuit.Subtract(System.DateTime.Now);
        timeLeft = PlayerPrefs.GetFloat("timeLeft") - (float)timePassed.TotalSeconds;

        PlayerPrefs.Save();

    }




    //Preferences
    private void SetDefaultPreferences()
    {
        PlayerPrefs.SetFloat("volumeControl", defaultVolume);
        PlayerPrefs.Save();
    }

    private void SetPreferencesOnGameOpen()
    {
        volumeControl = PlayerPrefs.GetFloat("volumeControl");
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("volumeControl", volumeControl);

        if (currentGameState != GameState.NotStarted)
        {
            if(currentGameState == GameState.Playing) PlayerPrefs.SetInt("SavedGameState", 1);
            if (currentGameState == GameState.GameLost) PlayerPrefs.SetInt("SavedGameState", 0);


            PlayerPrefs.SetFloat("timeLeft", timeLeft);

            timeStampWhenLastQuit = System.DateTime.Now;
            PlayerPrefs.SetString("timeStampWhenLastQuit", timeStampWhenLastQuit.ToString());

        }


        PlayerPrefs.Save();

    }
}

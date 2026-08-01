using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

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
    private float timePassedSinceLastPour = 0f;
    private float totalTimeTillFull = 0f;

    private bool hasStartedGame = false;

    private DateTime timeStampWhenLastQuit;




    //Player preferences
    [SerializeField] private float defaultVolume = 0f;
    private float volumeControl = 0f;

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
    }

    private void ConnectRecordedValues()
    {
        //If the game has started already, connect the players values (score, timeleft etc.) to the scripts it needs to.
        //If not, set the default values
        if (!PlayerPrefs.HasKey("hasStartedGame") || (PlayerPrefs.GetInt("hasStartedGame") == 0 ? true : false))
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


    private void SetValuesOnGameOpen()
    {
        hasStartedGame = PlayerPrefs.GetInt("hasStartedGame") == 1 ? true : false;
        timeStampWhenLastQuit = DateTime.Parse(PlayerPrefs.GetString("timeStampWhenLastQuit"));
        totalTimeTillFull = PlayerPrefs.GetFloat("totalTimeTillFull");

        TimeSpan timePassed = timeStampWhenLastQuit.Subtract(System.DateTime.Now);
        timePassedSinceLastPour = PlayerPrefs.GetFloat("timePassedSinceLastPour") + (float)timePassed.TotalSeconds;


    }

    private void SetDefaultValues()
    {
        PlayerPrefs.SetFloat("timePassedSinceLastPour", 0f);
        PlayerPrefs.SetFloat("totalTimeTillFull", 0f);
        PlayerPrefs.SetString("timeStampWhenLastQuit", "");
        PlayerPrefs.SetInt("hasStartedGame", 0);
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

        if (hasStartedGame)
        {
            PlayerPrefs.SetInt("hasStartedGame", hasStartedGame ? 1 : 0);
            PlayerPrefs.SetFloat("totalTimeTillFull", totalTimeTillFull);
            PlayerPrefs.SetFloat("timePassedSinceLastPour", timePassedSinceLastPour);

            timeStampWhenLastQuit = System.DateTime.Now;
            PlayerPrefs.SetString("timeStampWhenLastQuit", timeStampWhenLastQuit.ToString());

        }


        PlayerPrefs.Save();

    }
}

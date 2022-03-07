using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int GlobalPixelsPerUnit = 16; //IMPORTANT
    public static GameManager instance;

    public int globalDiamonds = 0; //Player Diamonds
    public int playerStocks = 3; //Player stocks
    public float playerHP = 3; //Player Health
    public float playerHPMAX = 3; //Max player health

    public int playerSpawnID = 0; //0 is default spawn
    public bool playerJustDied = false;
    public string onDeathScene;
    public AudioClip onDeathSong;

    public string gameOverScene;

    public bool globalLockedBlocks = true;

    //Singleton code---------------------------------
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void playerDeath()
    {
        playerStocks--; //Lose 1-up
        playerJustDied = true;
        onDeathSong = SoundManager.instance.GetCurrentSong(); //Save the current song playing
        playerSpawnID = 0; //Reset spawn ID to avoid Wrong Warps
        //GameManager.instance.onDeathScene = <Main Scene of the Level, or one of the Checkpoints>
        
        SoundManager.instance.StopAllMusic();
        SceneManager.LoadScene(gameOverScene, LoadSceneMode.Single); //Go to Death Cutscene Room
    }

    public void playerContinue()
    {
        SoundManager.instance.PlayMusic(onDeathSong);
        playerHP = playerHPMAX;
        SceneManager.LoadScene(onDeathScene, LoadSceneMode.Single);
    }

    public void updateCustomGUI()
    {
        var myGUI = GameObject.Find("Ingame_GUI").GetComponent<customGUI>();
        myGUI.updateGUIValues();
    }

    public void resetAllValues()
    {
        globalDiamonds = 0; //Player Diamonds
        playerStocks = 3; //Player stocks
        playerHP = 5; //Player Health
        playerHPMAX = 5; //Max player health

        playerSpawnID = 0; //0 is default spawn
        playerJustDied = false;
    }
}

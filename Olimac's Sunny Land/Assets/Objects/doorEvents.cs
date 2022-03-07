using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorEvents : MonoBehaviour
{
    GameObject myPlayer, theScreenFade;

    public string sceneToLoad;
    public bool isAutomatic = false;
    public int spawnTarget; //Spawn to target after transition

    float timer = 0.5f;
    bool activated = false;
    bool awaitingPlayerInput = false; //Used for non-automatic doors

    void Start()
    {
        myPlayer = GameObject.Find("objPlayer");
        theScreenFade = GameObject.Find("ScreenFilter");
        GetComponent<Renderer>().enabled = false; //Make sprite invisible
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Autostart
        if(other.gameObject.tag == "Player" && isAutomatic)
        {
            startRoomChange();
        }
        
        //Not autostart
        if(other.gameObject.tag == "Player")
        {
            awaitingPlayerInput = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            awaitingPlayerInput = false;
        }
    }

    void Update()
    {
        //AWAITING PLAYER INTERACTION
        if(!activated && awaitingPlayerInput && Input.GetKeyDown(KeyCode.UpArrow))
        {
            startRoomChange();
        }

        //ACTIVATED
        if(activated && timer <= 0)
        {
            GameManager.instance.playerSpawnID = spawnTarget;
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
        else if(activated)
        {
            timer -= Time.deltaTime;
        }
    }

    void startRoomChange()
    {
        theScreenFade.GetComponentInChildren<Animator>().SetBool("leaveRoom", true);
        myPlayer.GetComponent<playerMovement>().stopMovement();
        activated = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleScreenMenu : MonoBehaviour
{
    int menuPhase = 0;
    /*
    0 = Start
    1 = Credits
    2 = Exit
    */
    int menuChoice = 0; //0 = Start, 1 = Credits, 2 = Exit

    int choiceCooldownMAX = 5;
    int choiceCooldown = 0;
    public GameObject creditsObject;

    public AudioClip selectionSound, acceptSound;

    public GameObject selectionArrow;
    Vector3 menubarDistance = new Vector3(0, 1, 0);

    Vector3 scaleZero = new Vector3(1, 0, 1);
    Vector3 scaleNormal = new Vector3(1, 1, 1);
    
    public string sceneToLoad;

    void Start()
    {
        creditsObject.transform.localScale = scaleZero;
    }

    // Update is called once per frame
    void Update()
    {
        //CHOICE COOLDOWN
        if(choiceCooldown > 0)
        {
            choiceCooldown--;
        }
        if(menuPhase == 0 && choiceCooldown <= 0)
        {
            //SELECTION MENU PHASE 0
            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(Input.GetKeyDown(KeyCode.UpArrow) && menuChoice > 0)
                {
                    menuChoice--;
                    SoundManager.instance.PlaySound(selectionSound);
                    selectionArrow.transform.position += menubarDistance;
                    Debug.Log("moved");
                }
                
                if(Input.GetKeyDown(KeyCode.DownArrow) && menuChoice < 2)
                {
                    menuChoice++;
                    SoundManager.instance.PlaySound(selectionSound);
                    selectionArrow.transform.position -= menubarDistance;
                    Debug.Log("moved");
                }
            }
            //ACCEPT AND PROCCEED TO PHASE 1
            if(Input.GetButtonDown("Submit"))
            {
                SoundManager.instance.PlaySound(acceptSound);
                menuPhase = 1;
                choiceCooldown = choiceCooldownMAX; //IMPORTANT ON EVERY CHOICE
            }
        }
        if(menuPhase == 1)
        {
            switch(menuChoice)
            {
                case 0:
                    Invoke("startTheGame", 2);
                    GameObject.Find("ScreenFilter").GetComponentInChildren<Animator>().SetBool("leaveRoom", true);
                    menuPhase = -1;
                break;

                case 1:
                    creditsObject.transform.localScale = scaleNormal;
                    if(Input.GetButtonDown("Submit") && choiceCooldown <= 0)
                    {
                        menuPhase = 0;
                        SoundManager.instance.PlaySound(selectionSound);
                        creditsObject.transform.localScale = scaleZero;
                    }
                break;

                case 2:
                Application.Quit();
                break;
            }
        }
    }

    void startTheGame()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}

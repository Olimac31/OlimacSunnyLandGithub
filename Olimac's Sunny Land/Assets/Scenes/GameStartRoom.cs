using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStartRoom : MonoBehaviour
{
    public float waitingTime;
    public string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(waitingTime > 0)
        {
            waitingTime -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
    }
}

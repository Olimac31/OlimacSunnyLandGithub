using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverScript : MonoBehaviour
{
    public GameObject playerDeathObject;
    playerDeathAnim playerDead;

    void Start()
    {
        playerDead = playerDeathObject.GetComponent<playerDeathAnim>();
    }

    void Update()
    {
        if(playerDead.waitingTime <= 0 && playerDead.phase == 1)
        {
            if(GameManager.instance.playerStocks <= 0)
            {
                SceneManager.LoadScene("gameStartRoom", LoadSceneMode.Single);
                GameManager.instance.resetAllValues();
            }
            else
            {
                GameManager.instance.playerContinue();
            }
        }
    }
}

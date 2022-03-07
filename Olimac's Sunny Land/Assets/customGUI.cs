using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customGUI : MonoBehaviour
{
    public GameObject myHearts;
    public GameObject myHeartsBack;

    public GameObject gemText, livesText;
    // Start is called before the first frame update

    void Start()
    {
        updateGUIValues();
    }

    public void updateGUIValues()
    {
        //Update Health
        myHearts.GetComponent<SpriteRenderer>().size = new Vector2(GameManager.instance.playerHP, 1);
        myHeartsBack.GetComponent<SpriteRenderer>().size = new Vector2(GameManager.instance.playerHPMAX, 1);

        //Update gems
        gemText.GetComponent<TextMesh>().text = GameManager.instance.globalDiamonds.ToString();

        //Update life stocks
        livesText.GetComponent<TextMesh>().text = GameManager.instance.playerStocks.ToString();
    }
}

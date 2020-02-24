using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject player, enemy;
    public static bool lose = false;
    public static bool win = false;
    public Text winLoseText,objectiveText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (PickUp.placed)
        {
            win = true;
            winLoseText.text = "You Win! Press R to Restart or Esc to Exit";
            if (Input.GetKeyDown("escape")) //exit function
            {
                Application.Quit();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                winLoseText.text = "";
                SceneManager.LoadScene("MacPuffin");
                lose = false;
                PlayerController.onHand = false;
                PickUp.placed = false;

            }
        }

        if (lose)
        {
            winLoseText.text = "You Lose! Press R to Restart";
            objectiveText.text = "";
            if (Input.GetKeyDown(KeyCode.R))
            {
                winLoseText.text = "";
                
                SceneManager.LoadScene("MacPuffin");
                lose = false;
                PlayerController.onHand = false;

            }
        }

    }




}

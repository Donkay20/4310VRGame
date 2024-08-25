using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Keyboard : MonoBehaviour
{
    [SerializeField] PlayerStats player;                //assign to call player scripts
    [SerializeField] Typing typer;                      //assign to call typing scripts
    //private readonly float STRATAGEM_COOLDOWN = 10;     //change if needed
    [SerializeField] private bool gameStarted;          //nothing works unless this is true; denotes game start
    [SerializeField] private GameObject welcomeUI;
    private bool stratagemInProgress;                   //checks to see if a stratagem is currently running
    private string stratagemCode;                       //the randomly generated code when a stratagem is called
    private string inputtedCode;                        //the code the player(s) input
    private string command;                             //the desired effect the player wants when they call on the stratagem
    //private float stratagemCooldown;                    //cooldown

    public GameObject popUp;
    public GameObject popUp1;


    /*
    * Here are the variables to set text: 
    *     stratagemHealText.text = "";
    *     stratagemRefuelText.text = "";
    *     stratagemReloadText.text = "";
    */
    public TMP_Text stratagemHealText;
    public TMP_Text stratagemRefuelText;
    public TMP_Text stratagemReloadText;

    [Space, SerializeField] private AudioSource correctSound;
    [Space, SerializeField] private AudioSource wrongSound;

    /*
     * How to use Typing:
     *     1. Use is IsReady() to check if text is available for retrieval (they hit enter, space, confirm key, etc.)
     *     2. Use RecieveWord() to get the word that was typed in (compare with word + '~' == Typer.recieveWord())
     *     3. Use ResetReady() to reset the typer
     */

    void Update()
    {
        if (!gameStarted)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            welcomeUI.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted)
        {  //start the game
            gameStarted = true;
            ResetStratagemDisplay();
            //add arduino switch
        }

        if (typer.IsReady())
        {
            inputtedCode = typer.RecieveWord();  // Retrieve the completed word
            if (stratagemInProgress && inputtedCode.Equals(stratagemCode + '~'))
            {  // Check if it matches the stratagem code
                ExecuteStratagem();  // Execute the stratagem action
            }
            else
            {
                wrongSound.Play();
            }
            typer.ResetReady();  // Reset the Typing script for new input
        }

        if (gameStarted)
        {   //all stratagem commands
            if ((Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) && !stratagemInProgress)
            {   //heal stratagem
                command = "heal";
                InitializeStratagem();
                stratagemHealText.text = stratagemCode;
                stratagemRefuelText.text = "Heal In Progress";
                stratagemReloadText.text = "Heal In Progress";
            }

            if ((Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) && !stratagemInProgress)
            {   //refuel stratagem
                command = "refuel";
                InitializeStratagem();
                stratagemRefuelText.text = stratagemCode;
                stratagemHealText.text = "Refuel In Progress";
                stratagemReloadText.text = "Refuel In Progress";
            }

            if ((Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) && !stratagemInProgress)
            {   //reload stratagem
                command = "reload";
                InitializeStratagem();
                stratagemReloadText.text = stratagemCode;
                stratagemHealText.text = "Reload In Progress";
                stratagemRefuelText.text = "Reload In Progress";
            }

            // Confirm is now binded to Typing.cs when player press Enter
            // if ((Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4)) && stratagemInProgress)
            // {    //confirm
            //     CheckStratagem();
            // }

            if ((Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5)) && stratagemInProgress)
            {    //reset (without aborting)
                ResetStratagemInput();
            }
        }
    }

    private void InitializeStratagem()
    {
        stratagemInProgress = true;
        GenerateCode();
    }

    private void GenerateCode()
    {
        stratagemCode = ""; int codeLength = Random.Range(4, 9);
        const string alphanumeric = "abcdefghijklmnopqrstuvwxyz";

        for (int i = 0; i < codeLength; i++)
        {
            stratagemCode += alphanumeric[Random.Range(0, alphanumeric.Length)];
        }

    }

    private void ExecuteStratagem()
    {
        switch (command)
        {
            case "heal":
                player.Heal(50);  // Adjust the healing amount as needed
                correctSound.Play();
                break;
            case "refuel":
                player.Refuel();
                correctSound.Play();
                break;
            case "reload":
                player.Reload();
                correctSound.Play();
                break;
        }
        if(popUp != null) popUp.SetActive(false);
        if(popUp1 != null) popUp1.SetActive(true);
        stratagemInProgress = false;
        ResetStratagemDisplay();
    }

    private void ResetStratagemInput()
    {
        inputtedCode = "";
        //update it to display here
    }
    private void ResetStratagemDisplay()
    {
        stratagemHealText.text = "Request Heal Code";
        stratagemRefuelText.text = "Request Refuel Code";
        stratagemReloadText.text = "Request Reload Code";
    }
}
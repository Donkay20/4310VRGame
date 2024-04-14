using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    [SerializeField] PlayerStats player;                //assign to call player scripts
    [SerializeField] Typing typer;                      //assign to call typing scripts
    private readonly float STRATAGEM_COOLDOWN = 10;     //change if needed
    private bool gameStarted;                           //nothing works unless this is true; denotes game start
    private bool stratagemInProgress;                   //checks to see if a stratagem is currently running
    private string stratagemCode;                       //the randomly generated code when a stratagem is called
    private string inputtedCode;                        //the code the player(s) input
    private string command;                             //the desired effect the player wants when they call on the stratagem
    private float stratagemCooldown;                    //cooldown

    /*
    * Here are the variables to set text: 
    *     stratagemHealText.text = "";
    *     stratagemRefuelText.text = "";
    *     stratagemReloadText.text = "";
    */
    public TMP_Text stratagemHealText;                 
    public TMP_Text stratagemRefuelText;                
    public TMP_Text stratagemReloadText;

    /*
     * How to use Typing:
     *     1. Use is IsReady() to check if text is available for retrieval (they hit enter, space, confirm key, etc.)
     *     2. Use RecieveWord() to get the word that was typed in (compare with word + '~' == Typer.recieveWord())
     *     3. Use ResetReady() to reset the typer
     */

    void Update() {
        if (stratagemCooldown > 0) {
            stratagemCooldown -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted) {  //start the game
            gameStarted = true;
        }

        if (stratagemCooldown <= 0) {   //all stratagem commands
            if ((Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0)) && gameStarted && !stratagemInProgress) {   //heal stratagem
                command = "heal";
                InitializeStratagem();
            }

            if ((Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) && gameStarted && !stratagemInProgress) {   //refuel stratagem
                command = "refuel";
                InitializeStratagem();
            }

            if ((Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) && gameStarted && !stratagemInProgress) {   //reload stratagem
                command = "reload";
                InitializeStratagem();
            }

            if ((Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4)) && gameStarted && stratagemInProgress) {    //confirm
                CheckStratagem();
            }

            if ((Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5)) && gameStarted && stratagemInProgress) {    //reset (without aborting)
                ResetStratagemInput();
            }
        }
    }

    private void InitializeStratagem() {
        stratagemInProgress = true;
        GenerateCode();
    }

    private void GenerateCode() {
        stratagemCode = "Decryption Code: "; int codeLength = Random.Range(4,9);
        const string alphanumeric = "abcdefghijklmnopqrstuvwxyz0123456789";

        for (int i = 0; i < codeLength; i++) {
            stratagemCode += alphanumeric[Random.Range(0, alphanumeric.Length)];
        }

        //todo: put something here to display on UI element
    }

    private void CheckStratagem() {
        if (stratagemCode.Equals(inputtedCode)) {   //input is correct
            switch (command) {
                case "heal":
                    player.Heal(50);  //adjust if needed
                    break;
                case "refuel":
                    player.Refuel();
                    break;
                case "reload":
                    player.Reload();
                    break;
            }
        } else {                                    //input is incorrect
            //disable stratagem
        }

        stratagemCooldown = STRATAGEM_COOLDOWN;
        stratagemInProgress = false;
    }

    private void ResetStratagemInput() {
        inputtedCode = "";
        //update it to display here
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class Typing : MonoBehaviour
{
    [SerializeField] public TMP_Text display; //display text
    [SerializeField] private string currWord; //word to use
    [SerializeField] private bool backSpaced; //check if word backspaced
    [SerializeField] private string sendWord; //word to send to anywhere
    [SerializeField] private bool isCompleted; //check if word is completed
    [SerializeField] public char enterKey; //which key to enter with

    //public AudioSource source; //audiosource for typing noises
    //public AudioClip type1; //type noise 1
    //public AudioClip type2; //type noise 2
    //public AudioClip enter; //enter sound

    // Start is called before the first frame update
    void Start()
    {   
        currWord = "";
        backSpaced = false;
        isCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {   
        //get input
        currWord += getInput();

        //check backspace
        if(backSpaced){
            currWord = currWord.Substring(0, currWord.Length - 1);
            backSpaced = false;
        }

        //check if word was completed
        if(currWord.Length > 0 && currWord[currWord.Length-1] == '~'){
            isCompleted = true;
            sendWord = currWord;
            currWord = "";
            setOutput(currWord);
        }else{
            setOutput(currWord);
        }
    }

    public string getInput()
    {
        string keysPressed = Input.inputString;
        string send = "";
        //int rand = 0;

        foreach (char c in keysPressed){
            //check for backspace key
            if (c == '\b'){
                if (send.Length != 0){
                    send = send.Substring(0, send.Length - 1);
                }
                else if(currWord.Length != 0){
                    backSpaced = true;
                    //rand = Random.Range(0, 2);
                    //if(rand == 0){
                        //source.PlayOneShot(type1);
                    //}else{
                     //   source.PlayOneShot(type2);
                    //}
                }
            }
            //check for enter key
            else if ((c == '\n') || (c == ' ') || (c == '\r') || (c == enterKey)){
                send += '~';
                //source.PlayOneShot(enter);

                return send;
                
            }
            //otherwise add letter
            else if (!char.IsDigit(c)){
                send += c;
                //rand = Random.Range(0, 2);
                //if(rand == 0){
                    //source.PlayOneShot(type1);
                //}else{
                   // source.PlayOneShot(type2);
                //}
            }
        }
        return send;
    }
    
    public string RecieveWord(){
        return sendWord;
    }
    public bool IsReady(){
        return isCompleted;
    }
    public void ResetReady(){
        isCompleted = false;
    }
    private void setOutput(string input){
        display.text = input;
    }
}

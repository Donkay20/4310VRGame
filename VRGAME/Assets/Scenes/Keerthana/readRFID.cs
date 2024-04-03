using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class readRFID : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UduinoManager.Instance.OnDataReceived +=DataReceived;
    }
    void DataReceived(string data, UduinoDevice board)
    {
        Debug.Log(data);
    }
  
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class buttonLED1 : MonoBehaviour
{
    public int buttonPin = 2;
    public int ledPin = 3;

    private SerialPort serialPort;
    private bool buttonState;
    private bool ledState;

    void Start()
    {
        serialPort = new SerialPort("COM9", 9600); // Change "COM3" to the appropriate port for Arduino1
        serialPort.Open();
    }

    void Update()
    {
        buttonState = ReadButtonState();
        if (buttonState && !ledState)
        {
            ledState = true;
            UpdateLED(ledState);
        }
        else if (!buttonState && ledState)
        {
            ledState = false;
            UpdateLED(ledState);
        }
    }

    bool ReadButtonState()
    {
        serialPort.WriteLine("R" + buttonPin); // Send command to Arduino1 to read button state
        string response = serialPort.ReadLine();
        return response.Trim() == "1";
    }

    void UpdateLED(bool state)
    {
        serialPort.WriteLine("W" + ledPin + (state ? "1" : "0")); // Send command to Arduino1 to update LED state
    }
}

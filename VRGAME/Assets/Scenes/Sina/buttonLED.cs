using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class ButtonLEDController : MonoBehaviour
{
    public int buttonPin = 7;
    public int ledPin = 8;
    bool buttonPressed = false;
    bool ledState = false;
    public PlayerInputPattern playerInputPattern;

    float lastButtonPressTime;
    float debounceDelay = 0.5f;

    void Start()
    {
        UduinoManager.Instance.pinMode(buttonPin, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(ledPin, PinMode.Output);
    }

    void Update()
    {
        int buttonState = UduinoManager.Instance.digitalRead(buttonPin);

        if (buttonState == 1 && Time.time - lastButtonPressTime > debounceDelay) 
        {
            ledState = !ledState;

            UduinoManager.Instance.digitalWrite(ledPin, ledState ? 255 : 0);

            lastButtonPressTime = Time.time;

            playerInputPattern.UpdatePatternElement(0, 'x', 1);
        }
    }
}

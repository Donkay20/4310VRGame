using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class ButtonLEDController : MonoBehaviour
{
    public int[] buttonPins = {2, 4, 6, 8, 10}; // Pin numbers for the buttons
    public int[] ledPins = {3, 5, 7, 9, 11}; // Pin numbers for the LEDs
    bool[] buttonStates = new bool[5]; // Array to track the state of each button
    bool[] ledStates = new bool[5]; // Array to track the state of each LED
    bool[] isPressed = {false, false, false, false, false}; // Array to track if the LED is pressed

    float[] lastButtonPressTimes = new float[5]; // Array to track the time of the last button press for each button
    float debounceDelay = 1f; // Minimum time between button presses to avoid debounce

    public PlayerInputPattern inputPattern;

    void Start()
    {
        // Configure pin modes for buttons and LEDs
        for (int i = 0; i < buttonPins.Length; i++)
        {
            UduinoManager.Instance.pinMode(buttonPins[i], PinMode.Input_pullup);
            UduinoManager.Instance.pinMode(ledPins[i], PinMode.Output);
        }

    }

    void Update()
    {
        // Check each button
        for (int i = 0; i < buttonPins.Length; i++)
        {
            int buttonState = UduinoManager.Instance.digitalRead(buttonPins[i]);

            // Check if the button is pressed (assuming LOW means pressed)
            if (buttonState == 1 && Time.time - lastButtonPressTimes[i] > debounceDelay && !isPressed[i])
            {
                isPressed[i] = true;

                // Toggle the LED state
                ledStates[i] = !ledStates[i];

                UduinoManager.Instance.digitalWrite(ledPins[i], ledStates[i] ? 255 : 0);

                // Update the last button press time
                lastButtonPressTimes[i] = Time.time;

                inputPattern.UpdatePatternElement(0, 'x', 1);
            }
            if (buttonState == 1 && Time.time - lastButtonPressTimes[i] > debounceDelay && isPressed[i])
            {
                isPressed[i] = false;

                // Toggle the LED state
                ledStates[i] = !ledStates[i];

                UduinoManager.Instance.digitalWrite(ledPins[i], ledStates[i] ? 255 : 0);

                // Update the last button press time
                lastButtonPressTimes[i] = Time.time;

                inputPattern.UpdatePatternElement(0, 'x', 0);
            }
        }
    }
}

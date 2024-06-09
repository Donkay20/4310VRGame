using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class ButtonLEDController : MonoBehaviour
{
    public int[] buttonPins = { 2, 4, 6, 8, 10, 12, 14, 16, 18 }; // Pin numbers for the buttons
    public int[] ledPins = { 3, 5, 7, 9, 11, 13, 15, 17, 19 }; // Pin numbers for the LEDs
    bool[] buttonStates = new bool[9]; // Array to track the state of each button
    bool[] ledStates = new bool[9]; // Array to track the state of each LED
    bool[] isPressed = { false, false, false, false, false, false, false, false, false }; // Array to track if the LED is pressed

    float[] lastButtonPressTimes = new float[9]; // Array to track the time of the last button press for each button
    public float debounceDelay = 1f; // Minimum time between button presses to avoid debounce

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
        // if (Input.GetKeyDown(KeyCode.Alpha0))
        // {
        //     inputPattern.UpdatePatternElement(0, 'x', 0);
        //     inputPattern.UpdatePatternElement(0, 'y', 0);
        //     inputPattern.UpdatePatternElement(0, 'z', 0);
        //     inputPattern.UpdatePatternElement(1, 'x', 0);
        //     inputPattern.UpdatePatternElement(1, 'y', 0);
        //     inputPattern.UpdatePatternElement(1, 'z', 0);
        //     inputPattern.UpdatePatternElement(2, 'x', 0);
        //     inputPattern.UpdatePatternElement(2, 'y', 0);
        //     inputPattern.UpdatePatternElement(2, 'z', 0);
        //     for (int i = 0; i < buttonPins.Length; i++)
        //     {
        //         ledStates[i] = false;

        //         UduinoManager.Instance.digitalWrite(ledPins[i], ledStates[i] ? 255 : 0);
        //     }
        // }
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

                //inputPattern.UpdatePatternElement(0, 'x', 1);
                switch (i)
                {
                    case 0:
                        inputPattern.UpdatePatternElement(0, 'x', 1);
                        break;
                    case 1:
                        inputPattern.UpdatePatternElement(0, 'y', 1);
                        break;
                    case 2:
                        inputPattern.UpdatePatternElement(0, 'z', 1);
                        break;
                    case 3:
                        inputPattern.UpdatePatternElement(1, 'x', 1);
                        break;
                    case 4:
                        inputPattern.UpdatePatternElement(1, 'y', 1);
                        break;
                    case 5:
                        inputPattern.UpdatePatternElement(1, 'z', 1);
                        break;
                    case 6:
                        inputPattern.UpdatePatternElement(2, 'x', 1);
                        break;
                    case 7:
                        inputPattern.UpdatePatternElement(2, 'y', 1);
                        break;
                    case 8:
                        inputPattern.UpdatePatternElement(2, 'z', 1);
                        break;
                    default:
                        Debug.LogError("Invalid component.");
                        break;
                }
            }
            if (buttonState == 1 && Time.time - lastButtonPressTimes[i] > debounceDelay && isPressed[i])
            {
                isPressed[i] = false;

                // Toggle the LED state
                ledStates[i] = !ledStates[i];

                UduinoManager.Instance.digitalWrite(ledPins[i], ledStates[i] ? 255 : 0);

                // Update the last button press time
                lastButtonPressTimes[i] = Time.time;

                //inputPattern.UpdatePatternElement(0, 'x', 0);
                switch (i)
                {
                    case 0:
                        inputPattern.UpdatePatternElement(0, 'x', 0);
                        break;
                    case 1:
                        inputPattern.UpdatePatternElement(0, 'y', 0);
                        break;
                    case 2:
                        inputPattern.UpdatePatternElement(0, 'z', 0);
                        break;
                    case 3:
                        inputPattern.UpdatePatternElement(1, 'x', 0);
                        break;
                    case 4:
                        inputPattern.UpdatePatternElement(1, 'y', 0);
                        break;
                    case 5:
                        inputPattern.UpdatePatternElement(1, 'z', 0);
                        break;
                    case 6:
                        inputPattern.UpdatePatternElement(2, 'x', 0);
                        break;
                    case 7:
                        inputPattern.UpdatePatternElement(2, 'y', 0);
                        break;
                    case 8:
                        inputPattern.UpdatePatternElement(2, 'z', 0);
                        break;
                    default:
                        Debug.LogError("Invalid component.");
                        break;
                }
            }
        }
    }
}
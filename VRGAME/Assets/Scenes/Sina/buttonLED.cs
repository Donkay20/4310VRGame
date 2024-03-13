using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class ButtonLED : MonoBehaviour
{
    public int ledPin = 8; // Pin number of the LED
    public int buttonPin = 7; // Pin number of the button
    public KeyCode keyWhenPressed; // Keyboard key to send when the button is pressed
    public KeyCode keyWhenReleased; // Keyboard key to send when the button is released
    bool lastButtonState = false; // Previous state of the button

    bool initialLedStateSent = false; // Flag to track if the initial LED state has been sent

    void Start()
    {
        // Initialize Uduino and configure pins
        UduinoManager.Instance.pinMode(ledPin, PinMode.Output);
        UduinoManager.Instance.pinMode(buttonPin, PinMode.Input_pullup); // Use internal pull-up resistor
    }

    void Update()
    {
        // Send initial LED state (HIGH) if not already sent
        if (!initialLedStateSent)
        {
            UduinoManager.Instance.digitalWrite(ledPin, 1); // Turn LED on
            initialLedStateSent = true;
        }

        // Read the current state of the button
        bool buttonState = UduinoManager.Instance.digitalRead(buttonPin) == 0; // Button is active low

        // Check if the button state has changed (button pressed or released)
        if (buttonState != lastButtonState)
        {
            // If the button is pressed
            if (buttonState)
            {
                // Turn off the LED
                UduinoManager.Instance.digitalWrite(ledPin, 0); // Turn LED off

                // Send the keyWhenPressed key
                if (keyWhenPressed != KeyCode.None)
                {
                    UduinoManager.Instance.sendCommand(keyWhenPressed.ToString());
                }
            }
            // If the button is released
            else
            {
                // Turn on the LED
                UduinoManager.Instance.digitalWrite(ledPin, 1); // Turn LED on

                // Send the keyWhenReleased key
                if (keyWhenReleased != KeyCode.None)
                {
                    UduinoManager.Instance.sendCommand(keyWhenReleased.ToString());
                }
            }
        }

        // Update last button state
        lastButtonState = buttonState;
    }
}

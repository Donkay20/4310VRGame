using UnityEngine;
using System.IO.Ports;

public class SerialReader : MonoBehaviour
{
    SerialPort serialPortUno;
    SerialPort serialPortUduino;
    string portNameUno = "COM9"; // COM port for Arduino Uno
    string portNameUduino = "COM6"; // COM port for Uduino Arduino
    int baudRate = 19200; // Make sure it matches your Arduino's baud rate

    void Start()
    {
        // Initialize the SerialPort objects
        serialPortUno = new SerialPort(portNameUno, baudRate);
        serialPortUduino = new SerialPort(portNameUduino, baudRate);
        
        // Open the serial ports
        serialPortUno.Open();
        serialPortUduino.Open();
    }

    void Update()
    {
        // Read data from Arduino Uno
        ReadDataFromUno();

        // Read data from Uduino Arduino
        ReadDataFromUduino();
    }

    void OnDestroy()
    {
        // Close the serial ports when the script is destroyed
        if (serialPortUno != null && serialPortUno.IsOpen)
        {
            serialPortUno.Close();
        }

        if (serialPortUduino != null && serialPortUduino.IsOpen)
        {
            serialPortUduino.Close();
        }
    }

    void ReadDataFromUno()
    {
        // Check if there is data available to read from Arduino Uno
        if (serialPortUno != null && serialPortUno.IsOpen && serialPortUno.BytesToRead > 0)
        {
            // Read the data from Arduino Uno
            string serialData = serialPortUno.ReadLine();
            
            // Display the received data from Arduino Uno in Unity console
            Debug.Log("Received data from Arduino Uno: " + serialData);
        }
    }

    void ReadDataFromUduino()
    {
        // Check if there is data available to read from Uduino Arduino
        if (serialPortUduino != null && serialPortUduino.IsOpen && serialPortUduino.BytesToRead > 0)
        {
            // Read the data from Uduino Arduino
            string serialData = serialPortUduino.ReadLine();
            
            // Display the received data from Uduino Arduino in Unity console
            Debug.Log("Received data from Uduino Arduino: " + serialData);
        }
    }
}

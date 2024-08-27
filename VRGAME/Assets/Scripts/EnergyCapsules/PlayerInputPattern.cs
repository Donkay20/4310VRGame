using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputPattern : MonoBehaviour
{
    public Vector3[] inputPattern = new Vector3[3];
    //SerialPort data_stream = new SerialPort("COM3", 19200);
    public SerialController serialController;
    // Start is called before the first frame update
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }

    // Update is called once per frame
    void Update()
    {
        AttachPatternToKey(KeyCode.Alpha1, 0, 'x');
        AttachPatternToKey(KeyCode.Alpha2, 0, 'y');
        AttachPatternToKey(KeyCode.Alpha3, 0, 'z');
        AttachPatternToKey(KeyCode.Alpha4, 1, 'x');
        AttachPatternToKey(KeyCode.Alpha5, 1, 'y');
        AttachPatternToKey(KeyCode.Alpha6, 1, 'z');
        AttachPatternToKey(KeyCode.Alpha7, 2, 'x');
        AttachPatternToKey(KeyCode.Alpha8, 2, 'y');
        AttachPatternToKey(KeyCode.Alpha9, 2, 'z');

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            CapsulesController.Instance.CheckPatternsInCapsules(inputPattern);
            serialController.SendSerialMessage("0");
            ResetPattern();
        }
        // if (Input.GetKeyDown(KeyCode.Alpha0))
        // {
        //     UpdatePatternElement(0, 'x', 0);
        //     UpdatePatternElement(0, 'y', 0);
        //     UpdatePatternElement(0, 'z', 0);
        //     UpdatePatternElement(1, 'x', 0);
        //     UpdatePatternElement(1, 'y', 0);
        //     UpdatePatternElement(1, 'z', 0);
        //     UpdatePatternElement(2, 'x', 0);
        //     UpdatePatternElement(2, 'y', 0);
        //     UpdatePatternElement(2, 'z', 0);
        // }
    }

    public void UpdatePatternElement(int row, char component)
    {
        if (row < 0 || row >= inputPattern.Length)
        {
            Debug.LogError("Row index out of range.");
            return;
        }

        switch (component)
        {
            case 'x':
                if(inputPattern[row].x == 0)
                    inputPattern[row].x = 1; 
                else if (inputPattern[row].x == 1)
                    inputPattern[row].x = 0;
                break;
            case 'y':
                if (inputPattern[row].y == 0)
                    inputPattern[row].y = 1;
                else if (inputPattern[row].y == 1)
                    inputPattern[row].y = 0;
                break;
            case 'z':
                if (inputPattern[row].z == 0)
                    inputPattern[row].z = 1;
                else if (inputPattern[row].z == 1)
                    inputPattern[row].z = 0;
                break;
            default:
                Debug.LogError("Invalid component.");
                break;
        }
    }

    public void AttachPatternToKey(KeyCode k, int row, char component)
    {
        if (Input.GetKeyDown(k))
        {
            UpdatePatternElement(row, component);
            int code = row*3 + (component - 69);
            code -= 50;
            //Debug.Log("Code sent to Ardity: "+ code);
            serialController.SendSerialMessage("" + code);
        }
    }

    public void ResetPattern()
    {
        inputPattern[0] = new Vector3(0, 0, 0);
        inputPattern[1] = new Vector3(0, 0, 0);
        inputPattern[2] = new Vector3(0, 0, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputPattern : MonoBehaviour
{
    public Vector3[] inputPattern = new Vector3[3];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            CapsulesController.Instance.CheckPatternsInCapsules(inputPattern);
        }
    }

    public void UpdatePatternElement(int row, char component, float value)
    {
        if (row < 0 || row >= inputPattern.Length)
        {
            Debug.LogError("Row index out of range.");
            return;
        }

        switch (component)
        {
            case 'x':
                inputPattern[row].x = value;
                break;
            case 'y':
                inputPattern[row].y = value;
                break;
            case 'z':
                inputPattern[row].z = value;
                break;
            default:
                Debug.LogError("Invalid component.");
                break;
        }
    }
}

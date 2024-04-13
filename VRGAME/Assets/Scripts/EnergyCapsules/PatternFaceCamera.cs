using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternFaceCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Camera.main != null)
        {
            Vector3 targetPosition = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
            transform.LookAt(targetPosition);
        }
    }
}

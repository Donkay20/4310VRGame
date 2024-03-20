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
}

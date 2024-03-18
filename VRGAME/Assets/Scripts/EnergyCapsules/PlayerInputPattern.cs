using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputPattern : MonoBehaviour
{
    public Vector4[] inputPattern = new Vector4[4];
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

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Homing : MonoBehaviour
{
    public GameObject target;
    public float speed = 1.0f;

    // This script allows this object to move towards the target game object
    // Moves in the z-direction, make sure object face +z in prefab

    void Start()
    {
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), speed * Time.deltaTime);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateChar : MonoBehaviour
{
    public bool isTurn = true;

    void Awake()
    {

    }

    void Update()
    {
        if (isTurn)
            transform.Rotate(new Vector3(0, 40f * Time.deltaTime, 0));
        else
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 180f, 0));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullHead : MonoBehaviour
{
    public SkullManager skullmanager;

    void Awake()
    {
        skullmanager = GameObject.Find("SkullManager").GetComponent<SkullManager>();
    }

    void Update()
    {
        if (transform.position.y < -10f)
        {
            skullmanager.RemoveSkull(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}

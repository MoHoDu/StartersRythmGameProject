using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    void Awake()
    {
        Invoke("DestroyMyself", 5f);
    }

    void DestroyMyself()
    {
        Destroy(this.gameObject);
    }

    void Update()
    {

    }
}

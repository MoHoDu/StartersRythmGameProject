using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullManager : MonoBehaviour
{
    public GameObject skullPrefab;
    public int maxCount = 25;
    public List<GameObject> curSkull = new List<GameObject>();
    public GameObject skullRespawner;
    public float timer = 3f;
    private float spawnPos = 0;
    public float moveSpeed = 5f;
    private float originalT;

    void Awake()
    {
        originalT = timer;
        skullRespawner = GameObject.Find("SkullRespawn");
    }

    public void RemoveSkull(GameObject skull)
    {
        curSkull.Remove(skull);
    }

    void Update()
    {
        if (curSkull.Count <= maxCount)
        {
            if (timer <= 0)
            {
                skullRespawner.transform.position = new Vector3(Random.Range(299, 301), skullRespawner.transform.position.y, 0);
                GameObject skull = Instantiate(skullPrefab);
                skull.transform.position = skullRespawner.transform.position + new Vector3(0, 5, 0);
                skull.name = "SkullHead";
                skull.transform.parent = this.transform;
                curSkull.Add(skull);
                timer = originalT;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }
}

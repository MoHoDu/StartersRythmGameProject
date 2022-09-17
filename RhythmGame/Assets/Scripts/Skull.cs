using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    private GameObject particle;
    private GameObject player;

    void Awake()
    {
        particle = FindObjectOfType<NotesManager>().skullParticlePre;
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject ptc = Instantiate(particle);
            ptc.transform.position = this.transform.position;
            if (other.transform.localScale.x == 1)
                Data.hp -= Random.Range(0.1f, 0.3f);
            // this.gameObject.SetActive(false);
            Destroy(this.transform.parent.gameObject);
        }
    }

    void Update()
    {
        if (!Data.isGame)
            return;

        if (player.transform.position.z + 2 < this.transform.position.z)
        {
            // this.gameObject.SetActive(false);
            Destroy(this.transform.parent.gameObject);
        }

        transform.localPosition = new Vector3(transform.localPosition.x, 1 + Mathf.PingPong(Time.time, 0.5f), transform.localPosition.z);
    }
}

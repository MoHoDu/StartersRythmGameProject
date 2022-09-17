using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [Range(0, 16)] public int noteNum = 0;
    private GameObject particle;
    private GameObject player;

    void Awake()
    {
        particle = FindObjectOfType<NotesManager>().noteParticlePre;
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && player.transform.localScale.x != 30)
        {
            transform.parent.GetComponent<AudioSource>().PlayOneShot(Data.notes[noteNum]);
            GameObject ptc = Instantiate(particle);
            ptc.transform.position = this.transform.position;
            FindObjectOfType<QuestManager>().combo++;
            this.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!Data.isGame)
            return;

        if (player.transform.position.z < this.transform.position.z && player.transform.localScale.x == 30)
        {
            transform.parent.GetComponent<AudioSource>().PlayOneShot(Data.notes[noteNum]);
            GameObject ptc = Instantiate(particle);
            ptc.transform.position = this.transform.position;
            FindObjectOfType<QuestManager>().combo++;
            this.gameObject.SetActive(false);
        }
        else if (player.transform.position.z + 2 < this.transform.position.z)
        {
            FindObjectOfType<QuestManager>().combo = 0;
            this.gameObject.SetActive(false);
        }
    }
}

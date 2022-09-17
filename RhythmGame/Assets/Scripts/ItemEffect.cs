using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemEffect : MonoBehaviour
{
    public GameObject player;

    void Awake()
    {

    }

    public void ItemUse(Item _item)
    {
        if (_item.itemName == "Red Potion")
        {
            player.transform.localScale = new Vector3(3f, 3f, 3f);
            player.GetComponent<Rigidbody>().mass = 3;

            if (Data.isGame)
            {
                player.transform.localScale = new Vector3(30f, 30f, 30f);
                player.transform.position = new Vector3(1000, 0, 0);
                StartCoroutine(Small());
            }
        }
        if (_item.itemName == "Blue Potion")
        {
            player.transform.localScale = new Vector3(1f, 1f, 1f);
            player.GetComponent<Rigidbody>().mass = 1;
        }
        if (_item.itemName == "key01")
        {
            SceneManager.LoadScene("Indoor");
            // player.transform.position = new Vector3(5.04f, -1.32f, 8.48f);
        }
    }

    IEnumerator Small()
    {
        yield return new WaitForSeconds(4f);
        player.transform.localScale = new Vector3(1f, 1f, 1f);
        player.GetComponent<Rigidbody>().mass = 1;
    }

    void Update()
    {

    }
}

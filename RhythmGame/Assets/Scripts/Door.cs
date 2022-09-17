using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class Door : MonoBehaviour
{
    private GameObject player;
    public GameObject playerPre;
    public TextMeshProUGUI alert;
    public Item key;

    void Awake()
    {
        player = GameObject.Find("Player");
        if (player == null)
        {
            player = Instantiate(playerPre);
            player.name = "Player";
            player.transform.position = player.transform.position = new Vector3(5.04f, -1.32f, 8.48f);
        }
        if (alert == null)
        {
            alert = GameObject.Find("Alert").GetComponent<TextMeshProUGUI>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        if (this.gameObject.scene.name == "Outdoor")
        {
            if (Data.houseLock[0])
            {
                foreach (InvenSlot slot in other.GetComponentsInChildren<InvenSlot>())
                {
                    if (slot.item != null && slot.item.itemName == "key01")
                    {
                        slot.SlotCount(-1);
                        FindObjectOfType<ItemEffect>().ItemUse(key);
                        return;
                    }
                }

                alert.text = "저순사자의 부탁을 들어준 뒤 열쇠를 먼저 획득하세요!";
                StartCoroutine(QuitAlert());

            }
            else
            {
                SceneManager.LoadScene("Indoor");
                player.transform.position = new Vector3(5.04f, -1.32f, 8.48f);
            }
        }
        else
        {
            SceneManager.LoadScene("Outdoor");
            player.transform.position = new Vector3(4.46f, 1f, 4.99f);
        }
    }

    IEnumerator QuitAlert()
    {
        yield return new WaitForSeconds(3f);
        alert.text = "";
    }

    void Update()
    {

    }
}

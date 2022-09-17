using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    void Awake()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && NPCMove.questMode)
        {
            gameObject.SetActive(false);
            QuestManager.itemCount++;
        }
    }

    void Update()
    {

    }
}

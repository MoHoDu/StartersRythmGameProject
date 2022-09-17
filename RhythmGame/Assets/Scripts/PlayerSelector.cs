using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSelector : MonoBehaviour
{
    public TextMeshProUGUI nickName;

    public GameObject[] character;

    void Awake()
    {
        nickName = GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
        nickName.text = Data.nick;

        for (int i = 0; i < character.Length; i++)
        {
            if (i != Data.charNum)
            {
                character[i].SetActive(false);
            }
            else
                character[i].SetActive(true);
        }
    }

    void Update()
    {

    }
}

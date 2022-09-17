using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputNick : MonoBehaviour
{
    public TMP_InputField nickName;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            StartBtn();
        }
    }

    public void StartBtn()
    {
        if (nickName.text.Length < 3)
        {
            nickName.text = "";
            nickName.placeholder.GetComponent<TextMeshProUGUI>().text = "Too short!";
            return;
        }

        Data.nick = nickName.text;
        Debug.Log(Data.nick);
        GetComponent<SceneChange>().OnStartBtn();
    }
}

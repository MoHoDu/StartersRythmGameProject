using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogInManager : MonoBehaviour
{
    public GameObject logInPanel;
    public GameObject charPanel;
    public GameObject nickPanel;

    public TMP_InputField id;
    public TMP_InputField ps;
    private bool isErr = false;

    void Awake()
    {
        id = GameObject.Find("ID").GetComponent<TMP_InputField>();
        ps = GameObject.Find("PASS").GetComponent<TMP_InputField>();
    }

    void Update()
    {
        if (id.isFocused)
        {
            id.placeholder.GetComponent<TextMeshProUGUI>().text = "";
            id.placeholder.GetComponent<TextMeshProUGUI>().color = new Color(50 / 255f, 50 / 255f, 50 / 255f, 128 / 255f);
            isErr = false;
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ps.Select();
            }
        }
        else if (id.text == "" && !isErr)
        {
            id.placeholder.GetComponent<TextMeshProUGUI>().text = "ID";
        }

        if (ps.isFocused)
        {
            ps.placeholder.GetComponent<TextMeshProUGUI>().text = "";
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                id.Select();
            }
        }
        else if (ps.text == "" && !isErr)
        {
            ps.placeholder.GetComponent<TextMeshProUGUI>().text = "Password";
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            LoginBtn();
        }
    }

    public void LoginBtn()
    {
        bool checkID = id.text == Data.ID;
        bool checkPS = ps.text == Data.PASS;

        if (checkID && checkPS)
        {
            logInPanel.SetActive(false);
            charPanel.SetActive(true);
        }
        else if (!checkID)
        {
            id.text = "";
            ps.text = "";

            TextMeshProUGUI idPlaceholder = id.placeholder.GetComponent<TextMeshProUGUI>();
            isErr = true;
            idPlaceholder.color = Color.red;
            idPlaceholder.text = "Check your id";
            ps.placeholder.GetComponent<TextMeshProUGUI>().text = "Password";
        }
        else
        {
            id.text = "";
            ps.text = "";

            TextMeshProUGUI idPlaceholder = id.placeholder.GetComponent<TextMeshProUGUI>();
            isErr = true;
            idPlaceholder.color = Color.red;
            idPlaceholder.text = "Check your password";
            ps.placeholder.GetComponent<TextMeshProUGUI>().text = "Password";
        }
    }

    public void NextBtn()
    {
        charPanel.SetActive(false);
        nickPanel.SetActive(true);
        Debug.Log(Data.charNum);
    }
}

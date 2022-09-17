using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPanel : MonoBehaviour
{
    public GameObject pointer;
    public Vector3 originalPos;
    public GameObject[] charImage;
    public GameObject[] charObj;

    public LogInManager loginManager;

    void Awake()
    {
        originalPos = pointer.transform.position;
    }

    void OnKey()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && Data.charNum < Data.charMaxNum)
        {
            NextChar();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && Data.charNum > 0)
        {
            PreChar();
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            loginManager.NextBtn();
        }
    }

    public void PreChar()
    {
        Data.charNum--;
        pointer.transform.position -= new Vector3(300, 0, 0);
        // ClickChar();
    }

    public void NextChar()
    {
        Data.charNum++;
        pointer.transform.position += new Vector3(300, 0, 0);
        // ClickChar();
    }

    public void ClickChar()
    {
        pointer.transform.position = originalPos + new Vector3(300 * Data.charNum, 0, 0);
    }

    void Update()
    {
        OnKey();

        for (int i = 0; i < charObj.Length; i++)
        {
            if (i == Data.charNum)
                charObj[i].GetComponent<RotateChar>().isTurn = true;
            else
                charObj[i].GetComponent<RotateChar>().isTurn = false;
        }
    }
}

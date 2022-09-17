using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static int itemCount = 0;
    public static bool curQuestClear = false;
    public TextMeshProUGUI itemCountTxt;
    public int goalCount = 80;

    public GameObject tutorialPanel;
    public TextMeshProUGUI ComboText;
    public int combo = 0;
    public int maxCombo = 0;
    public GameObject ResultPanel;
    public static bool tutorialOK = false;
    public GameObject comboIMG;

    private bool quiting = false;

    void Awake()
    {
        combo = 0;
        maxCombo = 0;
    }

    void QuitTutorial()
    {
        tutorialPanel.SetActive(false);
    }

    void QuitResult()
    {
        ResultPanel.SetActive(false);
    }

    void Update()
    {
        if (!Data.isGame && !quiting)
        {
            Invoke("QuitResult", 3f);
            quiting = true;
        }
        if (Data.isGame && !tutorialOK)
        {
            tutorialPanel.SetActive(true);
            tutorialOK = true;
            Invoke("QuitTutorial", 3f);
        }

        if (combo > maxCombo)
            maxCombo = combo;

        if (Data.isGame && combo > 30)
            comboIMG.SetActive(true);
        else
            comboIMG.SetActive(false);

        if (itemCount >= goalCount)
        {
            NPCMove.questClear = true;
        }

        itemCountTxt.text = $"x {itemCount.ToString()} / {goalCount}";
        ComboText.text = $"{combo} Combo !!!";
        ComboText.color = new Color(255 / 255f, combo >= 30 ? 1 / 255f : (30 - combo) / 30f, combo >= 30 ? 1 / 255f : (30 - combo) / 30f);
        ResultPanel.transform.Find("Count").GetComponent<TextMeshProUGUI>().text = $"총 <color=red>{itemCount}개</color> 음표 수집";
        ResultPanel.transform.Find("continueCount").GetComponent<TextMeshProUGUI>().text = $"최대 <color=red>{maxCombo}</color> 콤보 달성";

        string rank = "F";
        if (itemCount + (int)maxCombo / 5 >= 100) rank = "S";
        else if (itemCount + (int)maxCombo / 5 >= 90) rank = "A";
        else if (itemCount + (int)maxCombo / 5 >= 85) rank = "B";
        else if (itemCount + (int)maxCombo / 10 > 70) rank = "D";
        else rank = "F";

        ResultPanel.transform.Find("Rank").GetComponent<TextMeshProUGUI>().text = $"{rank}";
    }
}

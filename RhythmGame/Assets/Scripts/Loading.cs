using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Loading : MonoBehaviour
{
    public float loadingTime = 5f;
    private float maxloadingTime;
    public TextMeshProUGUI loadingT;
    public Button startBtn;
    public Image loadingBar;
    bool isChange = false;
    int tutorialNum = 0;
    public GameObject[] tutorialPanel;
    private Vector3 newPos;

    void Awake()
    {
        loadingT = GetComponent<TextMeshProUGUI>();
        loadingT.text = "Loading";

        loadingBar = GameObject.Find("loadingBar").GetComponent<Image>();

        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = Color.black;

        maxloadingTime = loadingTime;

        newPos = startBtn.GetComponent<RectTransform>().position - new Vector3(200, 0, 0);
        startBtn.onClick.AddListener(StartGame);
    }

    IEnumerator ChangeText()
    {
        isChange = true;
        yield return new WaitForSeconds(0.5f);
        loadingT.text = "Loading.";
        yield return new WaitForSeconds(0.5f);
        loadingT.text = "Loading..";
        yield return new WaitForSeconds(0.5f);
        loadingT.text = "Loading...";
        yield return new WaitForSeconds(0.5f);
        loadingT.text = "Loading";
        isChange = false;
    }

    public void StartGame()
    {
        GameObject.Find("Player").transform.position = new Vector3(12, 2, 8);
        SceneManager.LoadScene("Outdoor");
        Camera.main.clearFlags = CameraClearFlags.Skybox;
    }

    void Update()
    {
        if (tutorialNum == 0)
        {
            tutorialPanel[0].SetActive(true);

            if (Input.GetAxis("Horizontal") != 0)
                tutorialNum++;
        }
        else if (tutorialNum == 1)
        {
            tutorialPanel[1].SetActive(true);
            tutorialPanel[0].SetActive(false);

            if (Input.GetAxis("Vertical") != 0)
                tutorialNum++;
        }
        else if (tutorialNum == 2)
        {
            tutorialPanel[2].SetActive(true);
            tutorialPanel[1].SetActive(false);

            if (Input.GetKeyDown(KeyCode.Space))
                tutorialNum++;
        }
        else if (tutorialNum == 3)
        {
            tutorialPanel[3].SetActive(true);
            tutorialPanel[2].SetActive(false);

            if (Input.GetKeyDown(KeyCode.LeftShift))
                tutorialNum++;
        }
        else if (tutorialNum == 4)
        {
            tutorialPanel[4].SetActive(true);
            tutorialPanel[3].SetActive(false);

            if (Input.GetKeyDown(KeyCode.Tab))
                tutorialNum++;
        }
        else
        {
            tutorialPanel[4].SetActive(false);
            tutorialPanel[5].SetActive(true);
        }

        if (!isChange)
            StartCoroutine("ChangeText");

        if (GameObject.Find("Player").transform.position.y <= -6f)
        {
            GameObject.Find("Player").transform.position = new Vector3(300f, 0, 0);
        }

        float rnd = Random.Range(0.3f, 1f);
        if (loadingTime <= 0.5f)
        {
            rnd = Random.Range(0.1f, 0.3f);
        }
        loadingTime -= Time.deltaTime * rnd;
        loadingBar.fillAmount = (maxloadingTime - loadingTime) / maxloadingTime;

        if (loadingTime <= 0)
        {
            Vector3 tarV = (newPos - startBtn.GetComponent<RectTransform>().position);
            startBtn.transform.Translate(tarV * Time.deltaTime * 7f);
        }
    }
}

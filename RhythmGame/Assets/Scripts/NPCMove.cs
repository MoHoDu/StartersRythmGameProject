using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MonoBehaviour
{
    Vector3 OriginalPos;
    Quaternion OriginalRot;
    float detectDis;
    float OriginalDis;
    public float detectArea;
    public Animator Anim;
    public float NPCSpeed = 5f;
    private GameObject Player;
    private bool goHome = false;
    public float stopDis = 1.5f;

    public static bool questMode = false;
    public static bool questClear = false;
    public List<GameObject> dialog = new List<GameObject>();
    public GameObject checkWin;

    void Awake()
    {
        OriginalPos = this.transform.position;
        OriginalRot = this.transform.rotation;
        Player = GameObject.FindWithTag("Player");
        GameObject canvasUI = GameObject.Find("CanvasUI");
        // dialog[0] = canvasUI.transform.GetChild(1).gameObject;
        // dialog[1] = canvasUI.transform.GetChild(2).gameObject;

        checkWin = canvasUI.transform.GetChild(0).gameObject;
    }

    void FixedUpdate()
    {
        detectDis = Vector3.Distance(Player.transform.position, transform.position);
        OriginalDis = Vector3.Distance(OriginalPos, transform.position);

        if (!questMode)
        {
            if (detectArea > detectDis)
            {
                if (detectDis < stopDis)
                {
                    Anim.SetBool("isWalk", false);
                }
                else
                {
                    Vector3 newPlayerPos = Player.transform.position;
                    newPlayerPos.y = this.transform.position.y;
                    this.transform.LookAt(newPlayerPos);
                    transform.position += transform.forward * NPCSpeed * Time.deltaTime;
                    Anim.SetBool("isWalk", true);
                    goHome = true;
                }
            }

            if (detectArea <= detectDis)
            {
                if (OriginalDis < stopDis)
                {
                    transform.rotation = OriginalRot;
                    Anim.SetBool("isWalk", false);
                    goHome = false;
                }
                else
                {
                    Vector3 newPos = OriginalPos;
                    newPos.y = this.transform.position.y;
                    OriginalPos = newPos;
                    this.transform.LookAt(OriginalPos);
                    transform.position += transform.forward * NPCSpeed * Time.deltaTime;
                    Anim.SetBool("isWalk", true);
                }
            }
        }

        if (questMode)
        {
            if (goHome)
            {
                if (OriginalDis < stopDis)
                {
                    transform.rotation = OriginalRot;
                    Anim.SetBool("isWalk", false);
                    goHome = false;
                }
                else
                {
                    Vector3 newPos = OriginalPos;
                    newPos.y = this.transform.position.y;
                    OriginalPos = newPos;
                    this.transform.LookAt(OriginalPos);
                    transform.position += transform.forward * NPCSpeed * Time.deltaTime;
                    Anim.SetBool("isWalk", true);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && !questMode && !questClear)
        {
            dialog[0].SetActive(true);
        }
        if (other.gameObject.tag == "Player" && questClear)
        {
            dialog[1].SetActive(true);
        }
    }

    public void OkBtn()
    {
        dialog[0].SetActive(false);
        checkWin.SetActive(true);
        questMode = true;
        Data.isGame = true;
        GameObject.Find("Player").GetComponent<PlayerMove>().npcs = this;
    }

    public void QuestClear()
    {
        Data.hp = 1;
        questMode = false;
        questClear = false;
        checkWin.SetActive(false);
        QuestManager.itemCount = 0;
    }

    public void GiveItem(Item item)
    {
        Player.GetComponentInChildren<Inventory>().GetItem(item);
    }

    public void NoBtn()
    {
        dialog[0].SetActive(false);
    }

    public void closeBtn()
    {
        dialog[1].SetActive(false);
        checkWin.SetActive(false);
        questMode = false;
    }
}

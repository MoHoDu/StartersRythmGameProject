using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpd = 300f;
    private float oriMoveSpd = 300f;
    public float rotSpd = 300f;
    // public float rotateY = 0;

    public float jumpForce = 300f;

    private Vector3 moveDir;
    private Vector3 worldDir;
    private float fallSpeed;

    private bool isJump = false;

    private Animator[] anim;

    private float moveZ = 0;
    public GameObject cameraPos;
    private GameObject contactPlatform;
    private Vector3 platformPosition;
    private Vector3 distance;
    // private GameObject minimapCamera;

    private bool fixedGamePos = false;
    private bool fixedGameCameraPos = false;

    public NPCMove npcs;

    private Vector3 gameCPos;
    private Quaternion rot;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponentsInChildren<Animator>();
        // minimapCamera = GameObject.Find("Camera_miniMap");
        // minimapCamera.transform.parent = null;
        // DontDestroyOnLoad(minimapCamera);
    }

    void Update()
    {
        // rotateY += Input.GetAxis("Mouse X") * rotSpd * Time.deltaTime;
        // transform.localEulerAngles = new Vector3(0, rotateY, 0);

        if (Data.isGame)
        {
            GameObject.Find("curHP").GetComponent<Image>().fillAmount = Data.hp;
            if (Data.hp <= 0)
            {
                Data.isGame = false;
                QuestManager.tutorialOK = false;
                NPCMove.questMode = false;
                NPCMove.questClear = false;
                QuestManager.itemCount = 0;
                GameObject.Find("CanvasUI").transform.GetChild(0).gameObject.SetActive(false);
                ExitGame();
                return;
            }

            if (!fixedGamePos)
            {
                transform.position = Vector3.right * 1000;
                transform.rotation = Quaternion.Euler(0, 180, 0);
                gameCPos = this.transform.position;
                FindObjectOfType<QuestManager>().ComboText.gameObject.SetActive(true);
                FindObjectOfType<QuestManager>().combo = 0;
                FindObjectOfType<QuestManager>().maxCombo = 0;
                fixedGamePos = true;
            }

            foreach (Animator ani in anim)
                ani.SetFloat("moveZ", 2f);

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                transform.position = transform.position.x == 980 ? transform.position : transform.position - Vector3.right * 10;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                transform.position = transform.position.x == 1020 ? transform.position : transform.position + Vector3.right * 10;
            }

            return;
        }

        if (contactPlatform != null)
        {
            if (!isJump && Input.GetAxis("Vertical") == 0)
            {
                //캐릭터의 위치는 밟고 있는 플랫폼과 distance 만큼 떨어진 위치
                transform.position = contactPlatform.transform.position - distance;
            }
        }

        Jump();

        transform.Rotate(0, Input.GetAxis("Horizontal") * rotSpd * Time.deltaTime, 0);

        foreach (Animator ani in anim)
            ani.SetBool("isJump", isJump);
        AnimParams();
    }

    private void FixedUpdate()
    {
        // transform.position += new Vector3(0, Input.GetAxis("Vertical") * moveSpd * Time.deltaTime);
        // transform.Translate(new Vector3(0, 0, Input.GetAxis("Vertical") * moveSpd * Time.deltaTime));
        if (!Data.isGame)
            Move();
    }

    private void LateUpdate()
    {
        // Camera.main.transform.position = this.transform.position + (Vector3.forward * -10) + (Vector3.up * 10);
        // // float rotY = this.transform.rotation.eulerAngles.y;
        // float rotY = Quaternion.LookRotation(this.transform.position - Camera.main.transform.position).eulerAngles.y;
        // // Camera.main.transform.rotation = ;
        // Camera.main.transform.rotation = Quaternion.Euler(new Vector3(30, rotY, 0));

        float curYangle = Mathf.LerpAngle(Camera.main.transform.eulerAngles.y, this.transform.eulerAngles.y, 5.0f * Time.deltaTime);

        if (Data.isGame)
        {
            // if (!fixedGameCameraPos)
            // {
            //     Camera.main.transform.position = this.transform.position - (rot * Vector3.forward * 50.0f) + (Vector3.up * 50.0f);
            //     Vector3 newPos = this.transform.position - (rot * Vector3.forward * 20.0f) + (Vector3.up * 20.0f);
            //     Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, newPos, 10f * Time.deltaTime);
            //     if (Vector3.Distance(Camera.main.transform.position, newPos) < 1f)
            //     {
            //         fixedGameCameraPos = true;
            //     }
            // }
            // else

            if (!fixedGameCameraPos)
            {
                Camera.main.transform.position = new Vector3(1000, 20, 17);
                Camera.main.transform.rotation = Quaternion.Euler(40, 180, 0);
                fixedGameCameraPos = true;
            }
            // else if (this.transform.localScale.x == 30)
            // {
            //     Camera.main.transform.position = this.transform.position - (rot * Vector3.forward * 130.0f) + (Vector3.up * 100.0f);
            //     Camera.main.transform.LookAt(this.transform.position);
            // }
            else
            {
                // Camera.main.transform.LookAt(new Vector3(1000, 0, 0));
                return;
            }

        }
        else
        {
            rot = Quaternion.Euler(0, curYangle, 0);
            if (this.transform.localScale.x == 3)
                Camera.main.transform.position = this.transform.position - (rot * Vector3.forward * 45.0f) + (Vector3.up * 30.0f);
            else
                Camera.main.transform.position = this.transform.position - (rot * Vector3.forward * 15.0f) + (Vector3.up * 10.0f);

            gameCPos = this.transform.position;
            Camera.main.transform.LookAt(gameCPos);
        }


        // float miniCurYangle = Mathf.LerpAngle(minimapCamera.transform.eulerAngles.y, this.transform.eulerAngles.y, 5.0f * Time.deltaTime);
        // Quaternion miniRot = Quaternion.Euler(0, miniCurYangle, 0);
        // minimapCamera.transform.position = this.transform.position + (Vector3.up * 100.0f);
        // Camera.main.transform.LookAt(this.transform);
    }

    public void ExitGame()
    {
        transform.position = new Vector3(0, 0, 5);
        fixedGamePos = false;
        GameObject.Find("ComBo").SetActive(false);
        Data.hp = 1;
        GameObject.Find("curHP").GetComponent<Image>().fillAmount = Data.hp;
        fixedGameCameraPos = false;
    }

    void Move()
    {
        // fallSpeed = rb.velocity.y;
        moveDir = new Vector3(0, 0, Input.GetAxis("Vertical")).normalized;
        worldDir = transform.TransformDirection(moveDir) * moveSpd * this.transform.localScale.x * Time.deltaTime;
        // worldDir.y = fallSpeed;
        rb.velocity = new Vector3(worldDir.x, rb.velocity.y, worldDir.z);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            // this.transform.parent = null;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce * this.transform.localScale.x, ForceMode.Impulse);
            isJump = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.contacts[0].normal.y > 0.7f)
        {
            if (other.gameObject.tag == "Ground" && isJump)
            {
                isJump = false;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }

            //접촉한 오브젝트의 태그가 platform 일 때,
            if (other.gameObject.name == "Skel")
            {
                // rb.mass = 0.3f;
                //접촉한 순간의 오브젝트 위치를 저장
                // contactPlatform = other.gameObject;
                // platformPosition = contactPlatform.transform.position;
                // //접촉한 순간의 오브젝트 위치와 캐릭터 위치의 차이를 distance에 저장
                // distance = platformPosition - transform.position;

                this.transform.parent = other.transform;
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == "Skel")
        {
            // rb.mass = 1;
            // contactPlatform = null;
            // platformPosition = Vector3.zero;
            // distance = Vector3.zero;
            this.transform.parent = null;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void AnimParams()
    {
        float z = moveDir.z;
        if (Input.GetKey(KeyCode.LeftShift) && z > 0)
        {
            z = 2f;
            moveSpd = oriMoveSpd * 2f;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && z < 0)
        {
            z = -2f;
            moveSpd = oriMoveSpd * 2f;
        }
        else
            moveSpd = oriMoveSpd;

        if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
        {
            z = 0f;
        }

        const float LerpSpeed = 0.05f;
        moveZ = Mathf.Lerp(moveZ, z, LerpSpeed);
        foreach (Animator ani in anim)
            ani.SetFloat("moveZ", Mathf.Abs(moveZ));
    }
}

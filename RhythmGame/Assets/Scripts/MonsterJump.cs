using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterJump : MonoBehaviour
{
    public bool isJump = false;
    public bool isGround = false;
    public float timer = 3f;
    private Animator anim;
    public float jumpForce = 10f;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Jump()
    {
        isJump = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isJump = false;
            isGround = true;
            anim.SetBool("isJump", false);
        }

        if (other.gameObject.tag == "Player")
        {
            // other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            jumpForce = 17f;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            // other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            jumpForce = 10f;
        }
    }

    void Update()
    {
        if (timer <= 0)
        {
            isJump = true;
            timer = 3f;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (isJump && isGround)
        {
            GetComponent<Rigidbody>().AddForce((Vector3.up) * jumpForce, ForceMode.VelocityChange);
            anim.SetBool("isJump", true);
            isGround = false;
        }

        if (transform.position.y < -5f)
            transform.position = new Vector3(transform.position.x, 3, transform.position.z);
    }
}

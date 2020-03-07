using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_B : MonoBehaviour
{
    private CharacterController2D cc;
    private Animator anim;
    private Rigidbody2D rigid;
    private float move;
    private float speed = 5f;
    private bool jump;
    private void Start()
    {
        cc = GetComponent<CharacterController2D>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        move = Input.GetAxis("Horizontal2");
        jump = Input.GetKey(KeyCode.UpArrow);
        move *= speed;
        if (cc.isGrounded)
        {
            anim.SetFloat("speed_B", Mathf.Abs(move));
            anim.SetBool("jump_B", false);
        }
        else
        {
            anim.SetBool("jump_B", true);
        }
    }
    private void FixedUpdate()
    {
        cc.Move(move, jump);
    }
}

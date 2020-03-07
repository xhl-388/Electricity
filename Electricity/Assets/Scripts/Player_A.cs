using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_A : MonoBehaviour
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
        move = Input.GetAxis("Horizontal");
        jump = Input.GetKey(KeyCode.W);
        move *= speed;
        if (cc.isGrounded)
        {
            anim.SetFloat("speed", Mathf.Abs(move));
            anim.SetBool("jump", false);
        }
        else
        {
            anim.SetBool("jump", true);
        }
    }
    private void FixedUpdate()
    {
        cc.Move(move, jump);
    }
}

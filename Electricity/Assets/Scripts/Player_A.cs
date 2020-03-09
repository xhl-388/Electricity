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
    [HideInInspector]
    public List<GameObject> listA;
    private GameObject playerB;
    public GameObject relay1;
    public GameObject relay2;
    private GameObject relayInRadius;
    public bool isLinkedDirectly=true;//默认处于直接连接状态
    private LayerMask relayLayer;
    public Transform leftHand;
    public Transform rightHand;
    private float handSize = 0.05f;
    private GameObject holdingRelay=null;
    private bool cantControl = false;
    private void Start()
    {
        playerB = GameObject.Find("Player_B");
        cc = GetComponent<CharacterController2D>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        listA = new List<GameObject>();
        relayLayer = LayerMask.NameToLayer("Relay");
    }
    private void Update()
    {
        if (cantControl)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (relayInRadius != null&&holdingRelay==null)
            {
                relayInRadius.GetComponent<Relay>().target = this.gameObject;
                holdingRelay = relayInRadius;
            }
            else if (holdingRelay!=null)
            {
                holdingRelay.GetComponent<Relay>().PutDown();
                holdingRelay = null;
            }
        }
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
        if (!cantControl)
        {
            cc.Move(move, jump);
        }
        Collider2D collider1 = Physics2D.OverlapCircle(leftHand.position, handSize,1<<relayLayer);
        Collider2D collider2 = Physics2D.OverlapCircle(rightHand.position, handSize, 1 << relayLayer);
        if (collider1)
        {
            relayInRadius = collider1.gameObject;
        }
        else if (collider2)
        {
            relayInRadius = collider2.gameObject;
        }
        else relayInRadius = null;

        if ((playerB.transform.position - gameObject.transform.position).magnitude > 3)
        {
            isLinkedDirectly = false;
            if (relay1 != null)
            {
                if (listA.Contains(relay1))
                {
                    if ((relay1.transform.position - gameObject.transform.position).magnitude > 3)
                    {
                        listA.Remove(relay1);
                    }
                }
                else if ((relay1.transform.position - gameObject.transform.position).magnitude < 3)
                {
                    listA.Add(relay1);
                }
            }
            if (relay2 != null)
            {
                if (listA.Contains(relay2))
                {
                    if ((relay2.transform.position - gameObject.transform.position).magnitude > 3)
                    {
                        listA.Remove(relay2);
                    }
                }
                else if ((relay2.transform.position - gameObject.transform.position).magnitude < 3)
                {
                    listA.Add(relay2);
                }
            }
        }
        else
        {
            isLinkedDirectly = true;
        }
    }
    public void GotDelivered()
    {
        cantControl = true;
        this.GetComponent<Collider2D>().enabled = false;
        rigid.simulated=false;
        anim.SetFloat("speed", 0f);
        anim.SetBool("jump", false);
    }
}

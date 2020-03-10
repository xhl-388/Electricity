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
    [HideInInspector]
    public List<GameObject> listB;
    private GameObject playerA;
    public GameObject relay1;
    public GameObject relay2;
    private Player_A playerACon;
    private LayerMask relayLayer;
    private GameObject relayInRadius;
    public Transform leftHand;
    public Transform rightHand;
    private float handSize=0.1f;
    private GameObject holdingRelay=null;
    [HideInInspector]
    public bool cantControl = false;
    private bool hasBelt = false;
    private LayerMask belt;
    private void Start()
    {
        playerA = GameObject.Find("Player_A");
        playerACon = playerA.GetComponent<Player_A>();
        cc = GetComponent<CharacterController2D>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        relayLayer = LayerMask.NameToLayer("Relay");
        if (GameObject.FindWithTag("Conveyorbelt"))
        {
            hasBelt = true;
            belt = LayerMask.NameToLayer("Conveyorbelt");
        }
    }
    private void Update()
    {
        if (cantControl)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (relayInRadius != null && holdingRelay == null)
            {
                relayInRadius.GetComponent<Relay>().target = this.gameObject;
                holdingRelay = relayInRadius;
            }
            else if (holdingRelay != null)
            {
                holdingRelay.GetComponent<Relay>().PutDown();
                holdingRelay = null;
            }
        }
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
        if (hasBelt)
        {
            Collider2D collider = Physics2D.OverlapCircle(cc.groundCheck.position, 0.1f, 1 << belt);
            if (collider)
            {
                rigid.AddForce(new Vector2(-60f, 0));
            }
        }
        if (!cantControl)
        {
            cc.Move(move, jump);
        }
        Collider2D collider1 = Physics2D.OverlapCircle(leftHand.position, handSize, 1 << relayLayer);
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

        if (!playerACon.isLinkedDirectly)
        {
            if (relay1 != null)
            {
                if (listB.Contains(relay1))
                {
                    if ((relay1.transform.position - gameObject.transform.position).magnitude > 3)
                    {
                        listB.Remove(relay1);
                    }
                }
                else if ((relay1.transform.position - gameObject.transform.position).magnitude < 3)
                {
                    listB.Add(relay1);
                }
            }
            if (relay2 != null)
            {
                if (listB.Contains(relay2))
                {
                    if ((relay2.transform.position - gameObject.transform.position).magnitude > 3)
                    {
                        listB.Remove(relay2);
                    }
                }
                else if ((relay2.transform.position - gameObject.transform.position).magnitude < 3)
                {
                    listB.Add(relay2);
                }
            }
        }
    }
    public void GotDelivered()
    {
        cantControl = true;
        this.GetComponent<Collider2D>().enabled = false;
        rigid.simulated = false;
        anim.SetFloat("speed_B", 0f);
        anim.SetBool("jump_B", false);
    }
}

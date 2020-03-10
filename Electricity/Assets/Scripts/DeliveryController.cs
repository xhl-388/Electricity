using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryController : MonoBehaviour
{
    private LayerMask player;
    private bool gotDelivered_A=false;
    private bool gotDelivered_B=false;
    private GameController gameCon;
    private AudioSource audioSource;
    private void Start()
    {
        player = LayerMask.NameToLayer("Player");
        gameCon = GameObject.Find("GameManager").GetComponent<GameController>();
        audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.1f, 1 << player);
        if (collider)
        {
            if (collider.GetComponent<Player_A>())
            {
                Player_A con = collider.GetComponent<Player_A>();
                con.GotDelivered();
                audioSource.Play();
                gotDelivered_A = true;
                if (gotDelivered_B)
                {
                    gameCon.Succeed();
                }
            }
            else
            {
                Player_B con = collider.GetComponent<Player_B>();
                con.GotDelivered();
                audioSource.Play();
                gotDelivered_B = true;
                if (gotDelivered_A)
                {
                    gameCon.Succeed();
                }
            }
        }
    }
}

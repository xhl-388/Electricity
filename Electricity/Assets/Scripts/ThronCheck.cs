using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThronCheck : MonoBehaviour
{
    public Transform checkPoint;
    private GameObject gameCon;
    private float checkRadius = 0.1f;
    private float checkBreak=0.1f;
    private float checkTime;
    private LayerMask aimLayer;
    private bool isFail = false;
    private void Start()
    {
        checkTime = Time.time;
        aimLayer = LayerMask.NameToLayer("Player");
        gameCon = GameObject.Find("GameManager");
    }
    private void FixedUpdate()
    {
        if (isFail)
        {
            return;
        }
        if (checkTime + checkBreak < Time.time)
        {
            Collider2D colliders = Physics2D.OverlapCircle(checkPoint.position, checkRadius,1<<aimLayer);
            if (colliders)
            {
                isFail = true;
                gameCon.GetComponent<GameController>().Fail();
            }
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public GameObject targetObject;
    private bool isTriggerOfGate;
    private LayerMask relayLayer;
    public Transform checkOfRelay;
    public Sprite activatedSprite;
    private float checkRadius=0.1f;
    private void Start()
    {
        if (targetObject.CompareTag("Gate"))
        {
            isTriggerOfGate = true;
        }
        else isTriggerOfGate = false;
        relayLayer = LayerMask.NameToLayer("Relay");
    }
    private void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapCircle(checkOfRelay.position, checkRadius, 1 << relayLayer);
        if (collider&&collider.GetComponent<Rigidbody2D>().simulated)
        {
            GetComponent<SpriteRenderer>().sprite = activatedSprite;
            if (isTriggerOfGate)
            {
                targetObject.GetComponent<GateController>().GateOpen();
            }
            else
            {

            }
        }
    }
}
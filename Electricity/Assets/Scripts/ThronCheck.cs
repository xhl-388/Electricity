using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThronCheck : MonoBehaviour
{
    public Transform checkPoint;
    private float checkRadius = 0.1f;
    private float checkBreak=0.1f;
    private float checkTime;
    private LayerMask aimLayer;
    private void Start()
    {
        checkTime = Time.time;
        aimLayer = LayerMask.NameToLayer("Player");
    }
    private void FixedUpdate()
    {
        if (checkTime + checkBreak < Time.time)
        {
            Collider2D colliders = Physics2D.OverlapCircle(checkPoint.position, checkRadius,1<<aimLayer);
            if (colliders)
            {
                SceneManager.LoadScene("Main");
            }
        }
    }
}

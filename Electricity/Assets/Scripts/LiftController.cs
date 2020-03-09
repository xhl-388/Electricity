using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour
{
    private int dir = 1;
    private float speed = 1.7f;
    private float timeToExtreme;
    private float nextStopTime;
    private float stopTime=1f;
    private bool isStopping = false;
    IEnumerator LiftStop()
    {
        yield return new WaitForSeconds(stopTime);
        isStopping = false;
    }
    private void Start()
    {
        timeToExtreme = speed;
        nextStopTime = Time.time + timeToExtreme;
    }
    private void Update()
    {
        if (!isStopping)
        {
            transform.Translate(new Vector3(0, speed * dir * Time.deltaTime, 0));
        }
        if (Time.time >= nextStopTime)
        {
            Stop();
        }
    }
    private void Stop()
    {
        dir *= -1;
        isStopping = true;
        nextStopTime = Time.time + timeToExtreme + stopTime;
        StartCoroutine(LiftStop());
    }
}

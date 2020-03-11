using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dirt : MonoBehaviour
{
    private float timeA;
    private void Start()
    {
        timeA = Time.time;
    }
    void Update()
    {
        timeA += Time.deltaTime;
        if (timeA > 1f)
        {
            Debug.Log("111");
            Destroy(this.gameObject);
        }
    }
}

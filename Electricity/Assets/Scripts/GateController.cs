using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private Transform[] part;
    private bool isActive=false;
    IEnumerator HideRenderer()
    {
        yield return new WaitForSeconds(1f);
        isActive = false;
        yield return new WaitForSeconds(0.5f);
        part[1].GetComponent<SpriteRenderer>().sprite = null;
        part[2].GetComponent<SpriteRenderer>().sprite = null;
    }
    private void Start()
    {
        part = gameObject.GetComponentsInChildren<Transform>();
    }
    private void Update()
    {
        if (isActive)
        {
            part[1].Translate(new Vector3(0, Time.deltaTime));
            part[2].Translate(new Vector3(0, -Time.deltaTime));
        }
    }
    public void GateOpen()
    {
        isActive = true;
        StartCoroutine(HideRenderer());
    }
}

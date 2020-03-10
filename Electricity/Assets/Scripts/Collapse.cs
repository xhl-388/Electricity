using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapse : MonoBehaviour
{
    private Transform[] myChild;
    IEnumerator Slide()
    {
        for(int i = 1; i < myChild.Length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(myChild[i].gameObject);
        }
    }
    private void Start()
    {
        myChild = gameObject.GetComponentsInChildren<Transform>();
    }
    public void GetCollapse()
    {
        StartCoroutine(Slide());
    }
}

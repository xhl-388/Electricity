using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relay : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> listRelay;
    private GameObject playerA;
    private GameObject playerB;
    private Transform getPos;
    private Transform getPos2;
    public GameObject anotherRelay;
    [HideInInspector]
    public GameObject target;
    private float followSpeed = 20f;
    private void Start()
    {
        listRelay = new List<GameObject>();
        playerA = GameObject.Find("Player_A");
        playerB = GameObject.Find("Player_B");
        getPos = GameObject.Find("Getpos").transform;
        getPos2 = GameObject.Find("Getpos2").transform;
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            this.gameObject.GetComponent<Rigidbody2D>().simulated=false;
            if (target == playerA)
            {
                transform.Translate((getPos.position - transform.position)*followSpeed*Time.deltaTime);
            }
            else
            {
                transform.Translate((getPos2.position - transform.position) * followSpeed * Time.deltaTime);
            }
        }
        if (!playerA.GetComponent<Player_A>().isLinkedDirectly)
        {
            if (listRelay.Contains(playerB))
            {
                if ((playerB.transform.position - gameObject.transform.position).magnitude > 3)
                {
                    listRelay.Remove(playerB);
                }
            }
            else if ((playerB.transform.position - gameObject.transform.position).magnitude < 3)
            {
                listRelay.Add(playerB);
            }
            if (anotherRelay != null)
            {
                if (listRelay.Contains(anotherRelay))
                {
                    if ((anotherRelay.transform.position - gameObject.transform.position).magnitude > 3)
                    {
                        listRelay.Remove(anotherRelay);
                    }
                }
                else if ((anotherRelay.transform.position - gameObject.transform.position).magnitude < 3)
                {
                    listRelay.Add(anotherRelay);
                }
            }
        }
    }
    public void PutDown()
    {
        target = null;
        this.gameObject.GetComponent<Collider2D>().enabled = true;
        this.gameObject.GetComponent<Rigidbody2D>().simulated=true;
    }
}

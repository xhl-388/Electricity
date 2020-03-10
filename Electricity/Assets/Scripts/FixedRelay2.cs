using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRelay2 : MonoBehaviour
{
    private GameObject playerA;
    private GameObject playerB;
    [HideInInspector]
    public List<GameObject> listFixedRelay2;
    private Player_A playerACon;
    private GameObject relay1;
    private void Start()
    {
        playerA = GameObject.Find("Player_A");
        playerB = GameObject.Find("Player_B");
        relay1 = GameObject.Find("Relay1");
        playerACon = playerA.GetComponent<Player_A>();
        listFixedRelay2 = new List<GameObject>();
    }
    private void FixedUpdate()
    {
        if (playerACon.isLinkedDirectly)
        {
            return;
        }
        if (listFixedRelay2.Contains(playerA))
        {
            if ((playerA.transform.position - transform.position).magnitude > 3)
            {
                listFixedRelay2.Remove(playerA);
            }
        }
        else if ((playerA.transform.position - transform.position).magnitude < 3)
        {
            listFixedRelay2.Add(playerA);
        }
        if (listFixedRelay2.Contains(playerB))
        {
            if ((playerB.transform.position - transform.position).magnitude > 3)
            {
                listFixedRelay2.Remove(playerB);
            }
        }
        else if ((playerB.transform.position - transform.position).magnitude < 3)
        {
            listFixedRelay2.Add(playerB);
        }
        if (listFixedRelay2.Contains(relay1))
        {
            if ((relay1.transform.position - transform.position).magnitude > 3)
            {
                listFixedRelay2.Remove(relay1);
            }
        }
        else if ((relay1.transform.position - transform.position).magnitude < 3)
        {
            listFixedRelay2.Add(relay1);
        }
    }
}

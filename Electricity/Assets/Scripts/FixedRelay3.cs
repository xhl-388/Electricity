using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRelay3 : MonoBehaviour
{
    private GameObject playerA;
    private Player_A playerACon;
    private GameObject playerB;
    private GameObject relay1;
    private GameObject relay2;
    [HideInInspector]
    public List<GameObject> listFixedRelay3;
    private void Start()
    {
        playerA = GameObject.Find("Player_A");
        playerACon = playerA.GetComponent<Player_A>();
        playerB = GameObject.Find("Player_B");
        relay1 = GameObject.Find("Relay1");
        relay2 = GameObject.Find("Relay2");
    }
    private void FixedUpdate()
    {
        if (playerACon.isLinkedDirectly)
        {
            return;
        }
        if (listFixedRelay3.Contains(playerA))
        {
            if ((playerA.transform.position - transform.position).magnitude > 3)
            {
                listFixedRelay3.Remove(playerA);
            }
        }
        else if ((playerA.transform.position - transform.position).magnitude < 3)
        {
            listFixedRelay3.Add(playerA);
        }
        if (listFixedRelay3.Contains(playerB))
        {
            if ((playerB.transform.position - transform.position).magnitude > 3)
            {
                listFixedRelay3.Remove(playerB);
            }
        }
        else if ((playerB.transform.position - transform.position).magnitude < 3)
        {
            listFixedRelay3.Add(playerB);
        }
        if (listFixedRelay3.Contains(relay1))
        {
            if ((relay1.transform.position - transform.position).magnitude > 3)
            {
                listFixedRelay3.Remove(relay1);
            }
        }
        else if ((relay1.transform.position - transform.position).magnitude < 3)
        {
            listFixedRelay3.Add(relay1);
        }
        if (listFixedRelay3.Contains(relay2))
        {
            if ((relay2.transform.position - transform.position).magnitude > 3)
            {
                listFixedRelay3.Remove(relay2);
            }
        }
        else if ((relay2.transform.position - transform.position).magnitude < 3)
        {
            listFixedRelay3.Add(relay2);
        }
    }
}

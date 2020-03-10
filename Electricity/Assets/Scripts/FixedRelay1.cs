using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRelay1 : MonoBehaviour
{
    private GameObject playerA;
    private GameObject playerB;
    [HideInInspector]
    public List<GameObject> listFixedRelay1;
    private Player_A playerACon;
    private void Start()
    {
        playerA = GameObject.Find("Player_A");
        playerB = GameObject.Find("Player_B");
        playerACon = playerA.GetComponent<Player_A>();
        listFixedRelay1 = new List<GameObject>();
    }
    private void FixedUpdate()
    {
        if (playerACon.isLinkedDirectly)
        {
            return;
        }
        if (listFixedRelay1.Contains(playerA))
        {
            if ((playerA.transform.position - transform.position).magnitude > 3)
            {
                listFixedRelay1.Remove(playerA);
            }
        }
        else if((playerA.transform.position - transform.position).magnitude < 3)
        {
            listFixedRelay1.Add(playerA);
        }
        if(listFixedRelay1.Contains(playerB))
        {
            if ((playerB.transform.position - transform.position).magnitude > 3)
            {
                listFixedRelay1.Remove(playerB);
            }
        }
        else if ((playerB.transform.position - transform.position).magnitude < 3)
        {
            listFixedRelay1.Add(playerB);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricController : MonoBehaviour
{
    private GameObject playerA;
    private GameObject playerB;
    private LineRenderer lr;
    private Player_A playerACon;
    private float deadTime;
    private float deadMaxBreak=3f;
    private GameObject gameCon;
    private void Start()
    {
        gameCon = GameObject.Find("GameManager");
        playerA = GameObject.Find("Player_A");
        playerACon = playerA.GetComponent<Player_A>();
        playerB = GameObject.Find("Player_B");
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;
        lr.endWidth = 0.07f;
        lr.startWidth= 0.07f;
        deadTime = Time.time + deadMaxBreak;
    }
    private void LateUpdate()
    {
        lr.positionCount = 0;
        if (playerACon.isLinkedDirectly)
        {
            Linked();
            LinkWithElectric(playerA.transform.position, playerB.transform.position);
        }
        else
        {
            if (playerACon.listA.Count == 0)
            {
                NotLinked();
            }
            else if (playerACon.listA.Count == 1)
            {
                if (playerACon.listA[0].GetComponent<Relay>().listRelay.Contains(playerB))
                {
                    Linked();
                    LinkWithElectric(playerA.transform.position, playerACon.listA[0].transform.position);
                    LinkWithElectric(playerACon.listA[0].transform.position, playerB.transform.position);
                }
                else if (playerACon.listA[0].GetComponent<Relay>().listRelay.Count!=0&&playerACon.listA[0].GetComponent<Relay>().listRelay[0].GetComponent<Relay>().listRelay.Contains(playerB))
                {
                    Linked();
                    LinkWithElectric(playerA.transform.position, playerACon.listA[0].transform.position);
                    LinkWithElectric(playerACon.listA[0].transform.position, playerACon.listA[0].GetComponent<Relay>().listRelay[0].transform.position);
                    LinkWithElectric(playerACon.listA[0].GetComponent<Relay>().listRelay[0].transform.position, playerB.transform.position);
                }
                else
                {
                    NotLinked();
                }
            }
            else if(playerACon.listA.Count == 2)
            {
                int flag = 0;
                for(int i = 0; i < 2; i++)
                {
                    if (playerACon.listA[i].GetComponent<Relay>().listRelay.Contains(playerB))
                    {
                        Linked();
                        LinkWithElectric(playerA.transform.position, playerACon.listA[i].transform.position);
                        LinkWithElectric(playerACon.listA[i].transform.position, playerB.transform.position);
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                {
                    NotLinked();
                }
            }
        }
    }
    void NotLinked()
    {
        if (Time.time > deadTime)
        {
            gameCon.GetComponent<GameController>().Fail();
        }
    }
    void Linked()
    {
        deadTime = Time.time + deadMaxBreak;
    }
    private void LinkWithElectric(Vector3 start,Vector3 end)
    {
        Vector3 vec = end - start;
        int positionCountBefore = lr.positionCount;
        int num = (int)(vec.magnitude / 0.2f)+lr.positionCount;
        lr.positionCount = num + 1;
        lr.SetPosition(positionCountBefore, start);
        for (int i = 1; i < num-positionCountBefore; i++)
        {
            Vector3 tar = start + vec * i /( num-positionCountBefore) + new Vector3(Random.Range(0f, 0.2f), Random.Range(0f, 0.2f));
            lr.SetPosition(i+positionCountBefore, tar);
        }
        lr.SetPosition(num, end);
    }
}

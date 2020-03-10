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
    private int numberOfFR=0;
    public GameObject fixedRelay1;
    public GameObject fixedRelay2;
    private GameObject relay1;
    private GameObject relay2;
    private bool isFail=false;
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
        if (fixedRelay1) { 
            numberOfFR++;
            relay1 = GameObject.Find("Relay1");
            relay2 = GameObject.Find("Relay2");
        }
        if (fixedRelay2) numberOfFR++;
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
            if (numberOfFR == 2)
            {
                if (fixedRelay1.GetComponent<FixedRelay1>().listFixedRelay1.Contains(playerA))
                {
                    if (fixedRelay1.GetComponent<FixedRelay1>().listFixedRelay1.Contains(playerB))
                    {
                        Linked();
                        LinkWithElectric(playerA.transform.position, fixedRelay1.transform.position);
                        LinkWithElectric(fixedRelay1.transform.position, playerB.transform.position);
                        return;
                    }
                    else if (fixedRelay2.GetComponent<FixedRelay2>().listFixedRelay2.Contains(playerB))
                    {
                        Linked();
                        LinkWithElectric(playerA.transform.position, fixedRelay1.transform.position);
                        LinkWithElectric(fixedRelay1.transform.position, fixedRelay2.transform.position);
                        LinkWithElectric(fixedRelay2.transform.position, playerB.transform.position);
                        return;
                    }
                    else if (fixedRelay2.GetComponent<FixedRelay2>().listFixedRelay2.Contains(relay1))
                    {
                        if (relay1.GetComponent<Relay>().listRelay.Contains(playerB))
                        {
                            Linked();
                            LinkWithElectric(playerA.transform.position, fixedRelay1.transform.position);
                            LinkWithElectric(fixedRelay1.transform.position, fixedRelay2.transform.position);
                            LinkWithElectric(fixedRelay2.transform.position, relay1.transform.position);
                            LinkWithElectric(relay1.transform.position, playerB.transform.position);
                            return;
                        }
                        else if (relay1.GetComponent<Relay>().listRelay.Contains(relay2) && relay2.GetComponent<Relay>().listRelay.Contains(playerB))
                        {
                            Linked();
                            LinkWithElectric(playerA.transform.position, fixedRelay1.transform.position);
                            LinkWithElectric(fixedRelay1.transform.position, fixedRelay2.transform.position);
                            LinkWithElectric(fixedRelay2.transform.position, relay1.transform.position);
                            LinkWithElectric(relay1.transform.position, relay2.transform.position);
                            LinkWithElectric(relay2.transform.position, playerB.transform.position);
                            return;
                        }
                    }
                }
            }
            else if (numberOfFR == 1)
            {
                FixedRelay3 FR = fixedRelay1.GetComponent<FixedRelay3>();
                if (FR.listFixedRelay3.Contains(playerA) && FR.listFixedRelay3.Contains(playerB))
                {
                    Linked();
                    LinkWithElectric(playerA.transform.position, fixedRelay1.transform.position);
                    LinkWithElectric(fixedRelay1.transform.position, playerB.transform.position);
                    return;
                }
                if (FR.listFixedRelay3.Count == 2)
                {
                    if(!FR.listFixedRelay3.Contains(playerA) && !FR.listFixedRelay3.Contains(playerB))
                    {
                        if(FR.listFixedRelay3[0].GetComponent<Relay>().listRelay.Contains(playerA)&& FR.listFixedRelay3[1].GetComponent<Relay>().listRelay.Contains(playerB))
                        {
                            Linked();
                            LinkWithElectric(playerA.transform.position, FR.listFixedRelay3[0].transform.position);
                            LinkWithElectric(FR.listFixedRelay3[0].transform.position, fixedRelay1.transform.position);
                            LinkWithElectric(fixedRelay1.transform.position, FR.listFixedRelay3[1].transform.position);
                            LinkWithElectric(FR.listFixedRelay3[1].transform.position, playerB.transform.position);
                            return;
                        }
                        else if(FR.listFixedRelay3[1].GetComponent<Relay>().listRelay.Contains(playerA) && FR.listFixedRelay3[0].GetComponent<Relay>().listRelay.Contains(playerB))
                        {
                            Linked();
                            LinkWithElectric(playerA.transform.position, FR.listFixedRelay3[1].transform.position);
                            LinkWithElectric(FR.listFixedRelay3[1].transform.position, fixedRelay1.transform.position);
                            LinkWithElectric(fixedRelay1.transform.position, FR.listFixedRelay3[0].transform.position);
                            LinkWithElectric(FR.listFixedRelay3[0].transform.position, playerB.transform.position);
                            return;
                        }
                    }
                    else
                    {
                        if (FR.listFixedRelay3.Contains(playerA) && FR.listFixedRelay3.Contains(relay1))
                        {
                            if (relay1.GetComponent<Relay>().listRelay.Contains(playerB))
                            {
                                Linked();
                                LinkWithElectric(playerA.transform.position, fixedRelay1.transform.position);
                                LinkWithElectric(fixedRelay1.transform.position, relay1.transform.position);
                                LinkWithElectric(relay1.transform.position, playerB.transform.position);
                                return;
                            }
                            else if (relay1.GetComponent<Relay>().listRelay.Contains(relay2) && relay2.GetComponent<Relay>().listRelay.Contains(playerB))
                            {
                                Linked();
                                LinkWithElectric(playerA.transform.position, fixedRelay1.transform.position);
                                LinkWithElectric(fixedRelay1.transform.position, relay1.transform.position);
                                LinkWithElectric(relay1.transform.position, relay2.transform.position);
                                LinkWithElectric(relay2.transform.position, playerB.transform.position);
                                return;
                            }
                        }
                        else if(FR.listFixedRelay3.Contains(playerA) && FR.listFixedRelay3.Contains(relay2))
                        {
                            if (relay2.GetComponent<Relay>().listRelay.Contains(playerB))
                            {
                                Linked();
                                LinkWithElectric(playerA.transform.position, fixedRelay1.transform.position);
                                LinkWithElectric(fixedRelay1.transform.position, relay2.transform.position);
                                LinkWithElectric(relay2.transform.position, playerB.transform.position);
                                return;
                            }
                            else if (relay2.GetComponent<Relay>().listRelay.Contains(relay1) && relay1.GetComponent<Relay>().listRelay.Contains(playerB))
                            {
                                Linked();
                                LinkWithElectric(playerA.transform.position, fixedRelay1.transform.position);
                                LinkWithElectric(fixedRelay1.transform.position, relay2.transform.position);
                                LinkWithElectric(relay2.transform.position, relay1.transform.position);
                                LinkWithElectric(relay1.transform.position, playerB.transform.position);
                                return;
                            }
                        }
                        else if (FR.listFixedRelay3.Contains(playerB) && FR.listFixedRelay3.Contains(relay1))
                        {
                            if (relay1.GetComponent<Relay>().listRelay.Contains(playerA))
                            {
                                Linked();
                                LinkWithElectric(playerB.transform.position, fixedRelay1.transform.position);
                                LinkWithElectric(fixedRelay1.transform.position, relay1.transform.position);
                                LinkWithElectric(relay1.transform.position, playerA.transform.position);
                                return;
                            }
                            else if (relay1.GetComponent<Relay>().listRelay.Contains(relay2) && relay2.GetComponent<Relay>().listRelay.Contains(playerA))
                            {
                                Linked();
                                LinkWithElectric(playerB.transform.position, fixedRelay1.transform.position);
                                LinkWithElectric(fixedRelay1.transform.position, relay1.transform.position);
                                LinkWithElectric(relay1.transform.position, relay2.transform.position);
                                LinkWithElectric(relay2.transform.position, playerA.transform.position);
                                return;
                            }
                        }
                        else if (FR.listFixedRelay3.Contains(playerB) && FR.listFixedRelay3.Contains(relay2))
                        {
                            if (relay2.GetComponent<Relay>().listRelay.Contains(playerA))
                            {
                                Linked();
                                LinkWithElectric(playerB.transform.position, fixedRelay1.transform.position);
                                LinkWithElectric(fixedRelay1.transform.position, relay2.transform.position);
                                LinkWithElectric(relay2.transform.position, playerA.transform.position);
                                return;
                            }
                            else if (relay2.GetComponent<Relay>().listRelay.Contains(relay1) && relay1.GetComponent<Relay>().listRelay.Contains(playerA))
                            {
                                Linked();
                                LinkWithElectric(playerB.transform.position, fixedRelay1.transform.position);
                                LinkWithElectric(fixedRelay1.transform.position, relay2.transform.position);
                                LinkWithElectric(relay2.transform.position, relay1.transform.position);
                                LinkWithElectric(relay1.transform.position, playerA.transform.position);
                                return;
                            }
                        }
                    }
                }
            }
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
        if (Time.time > deadTime&&!isFail)
        {
            gameCon.GetComponent<GameController>().Fail();
            isFail = true;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCMission : MonoBehaviour
{
    [SerializeField] private Transform npcChattrans;
    [SerializeField] private GameObject npcChat;
    [SerializeField] private BoxCollider2D talkRange;

    [HideInInspector] public Player p;
    [HideInInspector]public bool npctalk;

    float talkboxtime;
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        npctalk = false;
    }

    void Update()
    {

        if (npctalk == true)
        {
            talkboxtime += Time.deltaTime;
        }
        if (talkboxtime >= 3)
        {
            npctalk = false;
            talkboxtime = 0;
        }
        NPCChatOnOFF(npctalk);
        npcChattrans.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3);
    }

    public void NPCChatOnOFF(bool npctalk)
    {
        npcChat.SetActive(npctalk);
    }

    public void TalkTrans()
    {

    }
}

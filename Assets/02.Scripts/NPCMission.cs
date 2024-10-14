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

    private Player p;

    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        
    }

    public void NPCChatOn()
    {
        npcChat.SetActive(true);
    }

    public void NPCChatOff()
    {
        npcChat.SetActive(false);
    }

    public void TalkTrans()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCMission : MonoBehaviour
{
    [SerializeField] private Transform npcChattrans;
    [SerializeField] private GameObject npcChat;

    [HideInInspector] public Player p;
    private bool npctalk;

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
        if (talkboxtime >= 2)
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
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                npctalk = true;
                Debug.Log("Active");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCMission : MonoBehaviour
{
    [SerializeField] private Transform npcChattrans;
    [SerializeField] private GameObject npcChat;
    private bool npctalk;
    float talkboxtime;

    [HideInInspector] public Player p;
    public GameObject guidebutton;
    void Start()
    {
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
        guidebutton.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    public void NPCChatOnOFF(bool npctalk)
    {
        npcChat.SetActive(npctalk);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        if (other.gameObject.tag == "Player")
        {
            guidebutton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                npctalk = true;
                Debug.Log("Active");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            guidebutton.SetActive(false);
        }
    }
}

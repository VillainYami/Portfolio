using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Potal : MonoBehaviour
{
    public GameObject guidebutton;
    public Player p;

    private int cost;

    void Start()
    {
        cost = Random.Range(1,3);
    }

    void Update()
    {
        guidebutton.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        p = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (other.gameObject.tag == "Player")
        {
            guidebutton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                NextStage();
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

    void NextStage()
    {
        SceneManager.LoadSceneAsync(cost);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player p;
    public GameObject npctalk;

    private void Awake()
    {
        
    }

    void Start()
    {
        p = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        
    }
}

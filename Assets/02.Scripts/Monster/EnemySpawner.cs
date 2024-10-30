using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> sects = new List<Transform>();
    [SerializeField] private List<Monster> monsters = new List<Monster>();

    int rans;
    void Start()
    {
        for (int i = 0; i < sects.Count; i++)
        {
            rans = Random.Range(0, monsters.Count);
            Instantiate(monsters[rans],sects[i]);
        }
    }

    void Update()
    {
        
    }

    
}

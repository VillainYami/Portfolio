using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParingBoxPlayer : MonoBehaviour
{
    public AttackBoxMonster abm;
    public Player player;

    void Start()
    {
        player = transform.parent.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            AttackBoxMonster enemy = collision.GetComponent<Monster>().GetComponentInChildren<AttackBoxMonster>();

        }
    }
}

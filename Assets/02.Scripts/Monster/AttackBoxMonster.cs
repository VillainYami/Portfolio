using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoxMonster : MonoBehaviour
{
    Player player;
    Monster enemy;

    private void Start()
    {
        enemy = transform.parent.GetComponent<Monster>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player == null)
        {
            player = collision.GetComponent<Player>();
            if (player.paring != false)
            {
                player.Damaged(0);
            }
            else
            {
                player.Damaged(enemy.ed.damage);
            }
        }
    }

    private void OnEnable() => player = null;
}

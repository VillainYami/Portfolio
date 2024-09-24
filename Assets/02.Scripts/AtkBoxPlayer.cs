using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkBoxPlayer : MonoBehaviour
{
    [HideInInspector] public List<Monster> enemies = new List<Monster>();
    public Player player;

    private void OnEnable() => enemies.Clear();

    void Start()
    {
        player = transform.parent.GetComponent<Player>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !enemies.Contains(collision.GetComponent<Monster>()))
        {
            Monster enemy = collision.GetComponent<Monster>();
            enemies.Add(enemy);
            float damage = player.Damage;
            enemy.Damaged(damage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyData
{
    public float maxhp;
    public float hp;
    public float damage;
    public float atkRange;
    public float atkDelay;

    public bool isDead;
}
public enum EnemyState
{
    Idle,
    Move,
    Attack,
    Hit,
    Dead
}

public abstract class Monster : MonoBehaviour
{
    public abstract void Init();

    public EnemyData ed = new EnemyData();

    public Animator anim;
    public Transform target;
    public int nextMove;
    protected bool canThink = true;
    protected bool canAttack = true;
    protected bool flipX;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

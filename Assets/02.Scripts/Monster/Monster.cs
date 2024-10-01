using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public struct EnemyData
{
    public EnemyState state;
    public float maxhp;
    public float hp;
    public float damage;
    public float atkRange;
    public float atkDelay;
    public float atkReady;

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
    [SerializeField] private float seehp;

    public Animator anim;
    public Transform target;
    public Rigidbody2D rigid;
    public CapsuleCollider2D capsuleColl;
    public AttackBoxMonster atkBox;
    public int nextMove;

    protected bool canThink = true;
    protected bool canAttack = true;
    protected bool flipX;
    void Awake()
    {
        ed.isDead = false;
        Think();
    }

    void Start()
    {
        Init();
        target = GameObject.FindWithTag("Player").transform;
        Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), target.gameObject.GetComponent<CapsuleCollider2D>());
    }
    void FixedUpdate()
    {
        if (ed.state == EnemyState.Hit || ed.state == EnemyState.Dead)
            return;

        //Move
        if (ed.state != EnemyState.Attack)
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //바닥 체크
        PlatformCheck();
    }

    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
            Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), target.gameObject.GetComponent<CapsuleCollider2D>());
            return;
        }

        if (ed.state == EnemyState.Dead)
            return;

        if (ed.isDead == true || ed.hp <= 0)
            Die();

        if (Vector3.Distance(transform.position, target.position) < ed.atkRange)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);

            flipX = target.position.x > transform.position.x ? false : true;

            if (canAttack)
                AttackStart();

            else if (Vector3.Distance(transform.position, target.position) > ed.atkRange)
            {
                nextMove = flipX ? -3 : 3;
                anim.SetInteger("Walk", nextMove);
            }
        }
        else if (canThink && ed.state != EnemyState.Attack)
        {
            StartCoroutine("Think");
        }
        transform.localScale = flipX ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        seehp = ed.hp;
    }

    public IEnumerator Think()
    {
        canThink = false;

        //다음 움직임 방향 * 속도
        nextMove = Random.Range(-1, 2) * 3;
        ed.state = nextMove == 0 ? EnemyState.Idle : EnemyState.Move;
        anim.SetInteger("Walk", nextMove);

        //스프라이트 Flip.x
        if (nextMove != 0)
            flipX = nextMove == -3;

        yield return new WaitForSeconds(Random.Range(1, 3));

        canThink = true;
    }

    public void Turn()
    {
        nextMove *= -1;
        flipX = nextMove == -3;

        CancelInvoke();

        Invoke("Think", 2);
    }

    protected virtual void AttackStart()
    {
        StartCoroutine("AttackCoolDown");
        nextMove = 0;
        anim.SetTrigger("Attack");
        ed.state = EnemyState.Attack;
    }

    IEnumerator AttackCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(ed.atkDelay);
        canAttack = true;
    }

    void EventAttackEnd()
    {
        ed.state = EnemyState.Idle;
        anim.ResetTrigger("Attack");
        anim.ResetTrigger("Hit");
        StartCoroutine("Think");
    }

    public virtual void Damaged(float damage)
    {
        if (ed.state != EnemyState.Dead)
        {
            ed.hp -= damage;

            if (ed.state != EnemyState.Attack)
            {
                anim.SetTrigger("Hit");
                ed.state = EnemyState.Hit;
                rigid.velocity = new Vector2(0, rigid.velocity.y);
            }

            else
            {
                anim.SetTrigger("Hit");
                ed.state = EnemyState.Hit;
                rigid.velocity = new Vector2(0, rigid.velocity.y);
            }
        }

        nextMove = 0;
    }

    void Die()
    {
        StopAllCoroutines();
        anim.SetBool("Dead", true);
        ed.state = EnemyState.Dead;
        rigid.velocity = Vector2.zero;
        rigid.bodyType = RigidbodyType2D.Static;
        capsuleColl.isTrigger = true;
        Destroy(gameObject, 2f);
    }

    void PlatformCheck()
    {
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.75f, rigid.position.y - 0.5f);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("IsGround"));
        if (rayHit.collider == null)
            Turn();
    }
}

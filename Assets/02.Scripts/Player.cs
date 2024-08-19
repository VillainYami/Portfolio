using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public enum PlayerDir
    {
        left,
        right
    }

    public RuntimeAnimatorController animators;

    [HideInInspector] private PlayerData pd;
    [HideInInspector] public PlayerDir playerDir = PlayerDir.right;
    protected PlayerDir playerDir_past;

    [HideInInspector] public Animator animator;
    protected Rigidbody2D rigid;


    [HideInInspector]  private float inputX;
    [HideInInspector]  private float inputY;

    float moveSpeed = 6f;

    protected float originalGravity = 6;

    #region 플레이어 상태
    [HideInInspector] private SpriteRenderer sprd;
    public bool isDead = false;
    protected bool isUnbeat = false;
    protected float unbeatTime = 1f;
    #endregion

    #region 슬라이드
    float slidePower = 30f;
    float slideCoolTime = 0.8f;
    float slideTime = 0.2f;
    protected bool canslide = true;
    bool isslide = false;
    #endregion

    #region 대쉬
    float dashPower = 40f;
    float dashCoolTime = 0.8f;
    float dashTime = 0.2f;
    protected bool candash = true;
    bool isdash = false;
    #endregion

    #region 점프
    int jumCount = 0;
    float jumpPower = 15f;
    bool jumped = false;
    //bool isGround = true;

    protected bool canInput = true;
    #endregion
    protected virtual void Init()
    {
        GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        pd = transform.GetChild(0).GetComponent<PlayerData>();
        sprd = GetComponent<SpriteRenderer>();

        animator.runtimeAnimatorController = animators;
    }
    void Start()
    {
        Init();
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        JumpAnimation();

        if (!canInput)
            return;

        Move();
        Jump();
        Attack();

        if (Input.GetKeyDown(KeyCode.Z) && canslide)
            StartCoroutine("CSlide");

        if (Input.GetKeyDown(KeyCode.Space) && candash)
            StartCoroutine("CDash");

        //임시 피격데미지
        PDamage();
    }

    #region 시선처리
    protected void LookDir()
    {
        switch (playerDir)
        {
            case PlayerDir.right:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;

            case PlayerDir.left:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
        }
        playerDir_past = playerDir;
    }
    #endregion

    #region 점프
    protected void Jump()
    {
        if (!Input.GetKeyDown(KeyCode.C))
            return;

        if (jumped != true)
        {
            if (jumCount != 2)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    jumCount++;
                    rigid.velocity = Vector2.zero;
                    rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
                }
            }
        }
        else if (jumCount == 2)
        {
            jumped = true;
        }

        SetGravity(true);
        animator.SetBool("Slide", false);
        animator.SetBool("Dash", false);
    }
    #endregion


    #region 무브
    protected void Move()
    {
        if (isslide || isdash || animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            return;

        rigid.velocity = new Vector2(inputX * moveSpeed, rigid.velocity.y);

        if (inputX == 0)
            animator.SetBool("Move", false);

        else
        {
            animator.SetBool("Move", true);
            playerDir = inputX > 0 ? PlayerDir.right : playerDir = PlayerDir.left;
        }

        LookDir();
    }
    #endregion

    #region 점프 애니메이션
    protected void JumpAnimation()
    {
        if (rigid.velocity.y > 0.05f)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Fall", false);
        }
        else if (rigid.velocity.y < -0.05f)
        {
            animator.SetBool("Fall", true);
            animator.SetBool("Jump", false);
        }
        else
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", false);
        }
    }
    #endregion

    #region 공격
    protected void Attack()
    {
        if (!Input.GetKeyDown(KeyCode.X) || isslide || isdash)
            return;

        animator.SetTrigger("Attack");
    }
    protected void NEXTAttack()
    {
        if (isdash || isslide)
            return;

        if (Input.GetKey(KeyCode.X))
            animator.SetTrigger("Attack");
    }
    protected void DashAttack()
    {
        if (Input.GetKey(KeyCode.X))
            animator.SetTrigger("DashAttack");
    }

    //공격 A,B - 애니메이션 첫 프레임 event
    protected void EventStopMove()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
    }

    //공격 A,B - 무기를 휘두르는 순간 event
    protected void EventMoveAttack()
    {
        //공격시, 바라보는 방향을 입력하고있으면 전진
        if ((inputX > 0 && playerDir_past == PlayerDir.right) ||
            (inputX < 0 && playerDir_past == PlayerDir.left))
            rigid.velocity = transform.right * moveSpeed + transform.up * rigid.velocity.y;
    }

    //공격 데미지 판정 event
    /*protected void EventDamage()
    {
        foreach (var enemy in atBox.enemies)
        {
            SetDamage(enemy, Damage);
        }
    }

    public void SetDamage(Enemy enemy, float damage) => enemy.Damaged(damage);*/
    #endregion

    #region 슬라이드
    protected IEnumerator CSlide()
    {
        canslide = false;
        isslide = true;

        animator.SetBool("Slide", true);
        SetGravity(false);
        playerDir = inputX < 0 ? PlayerDir.left : inputX > 0 ? PlayerDir.right : playerDir;
        float dir = playerDir == PlayerDir.right ? 1 : -1;
        LookDir();
        rigid.velocity = new Vector2(dir * slidePower, 0);

        yield return new WaitForSeconds(slideTime);

        SetGravity(true);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("WSlide"))
            rigid.velocity = Vector2.zero;

        isslide = false;
        animator.SetBool("Slide", false);

        yield return new WaitForSeconds(slideCoolTime);
        canslide = true;
    }
    #endregion

    #region 대쉬
    protected IEnumerator CDash()
    {
        candash = false;
        isdash = true;

        animator.SetBool("Dash", true);
        SetGravity(false);
        playerDir = inputX < 0 ? PlayerDir.left : inputX > 0 ? PlayerDir.right : playerDir;
        float dir = playerDir == PlayerDir.right ? 1 : -1;
        LookDir();
        rigid.velocity = new Vector2(dir * dashPower, 0);

        yield return new WaitForSeconds(dashTime);

        SetGravity(true);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("WDash"))
            rigid.velocity = Vector2.zero;

        isdash = false;
        animator.SetBool("Dash", false);

        yield return new WaitForSeconds(dashCoolTime);
        candash = true;
    }
    #endregion

    #region 피격
    public void Damaged(float damage)
    {
        if (isDead || isUnbeat)
            return;

        pd.HP -= damage;
        if (pd.HP <= 0)
        {
            pd.HP = 0;
            animator.SetTrigger("Die");
            rigid.velocity = Vector2.zero;
            isDead = true;
        }
        else
        {
            StartCoroutine("UnbeatTime");
        }
    }
    //피격시 반짝거림
    IEnumerator UnbeatTime()
    {
        isUnbeat = true;
        for (int i = 0; i < unbeatTime * 10; ++i)
        {
            if (i % 2 == 0)
                sprd.color = new Color(1f, 1f, 1f, 0.35f);
            else
                sprd.color = new Color(1f, 1f, 1f, 0.7f);

            yield return new WaitForSeconds(0.1f);
        }

        sprd.color = new Color(1f, 1f, 1f, 1f);
        isUnbeat = false;
        yield return null;
    }
    #endregion

    protected IEnumerator EventCStopInput()
    {
        canInput = false;
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f)
        {
            yield return new WaitForEndOfFrame();
        }
        canInput = true;
    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("IsGround"))
        {
            jumped = false;
            jumCount = 0;
        }
    }
    protected void SetGravity(bool On)
    {
        rigid.gravityScale = On ? originalGravity : 0;
    }

    public void PDamage()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Damaged(50);
        }
    }
}

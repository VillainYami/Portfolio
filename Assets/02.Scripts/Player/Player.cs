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

    [HideInInspector] public PlayerDir playerDir = PlayerDir.right;
    protected PlayerDir playerDir_past;

    [HideInInspector] public Animator animator;
    protected Rigidbody2D rigid;


    [HideInInspector]  private float inputX;
    [HideInInspector]  private float inputY;

    float moveSpeed = 6f;

    protected float originalGravity = 6;

    #region 슬라이드
    float slidePower = 30f;
    float slideCoolTime = 0.8f;
    float slideTime = 0.2f;
    protected bool canslide = true;
    bool isslide = false;
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
    }
    #endregion


    #region 무브
    protected void Move()
    {
        if (isslide || animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
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
        if (!Input.GetKeyDown(KeyCode.X) || isslide)
            return;

        animator.SetTrigger("Attack");
    }
    protected void NEXTAttack()
    {
        if (Input.GetKey(KeyCode.X))
            animator.SetTrigger("Attack");
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

}

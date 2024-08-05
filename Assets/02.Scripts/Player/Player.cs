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

    #region ����
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
    }
    #region �ü�ó��
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

    #region ����
    protected void Jump()
    {
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
    }
    #endregion

    #region ����
    protected void Move()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
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

    #region ���� �ִϸ��̼�
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

    #region ����
    protected void Attack()
    {
        if (!Input.GetKeyDown(KeyCode.X))
            return;

        animator.SetTrigger("Attack");
    }

    //���� A,B - �ִϸ��̼� ù ������ event
    protected void EventStopMove()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
    }

    //���� A,B - ���⸦ �ֵθ��� ���� event
    protected void EventMoveAttack()
    {
        //���ݽ�, �ٶ󺸴� ������ �Է��ϰ������� ����
        if ((inputX > 0 && playerDir_past == PlayerDir.right) ||
            (inputX < 0 && playerDir_past == PlayerDir.left))
            rigid.velocity = transform.right * moveSpeed + transform.up * rigid.velocity.y;
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

}

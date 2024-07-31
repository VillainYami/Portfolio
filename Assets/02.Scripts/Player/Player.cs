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
    public List<RuntimeAnimatorController> animators;

    [HideInInspector] public PlayerDir playerDir = PlayerDir.right;
    protected PlayerDir playerDir_past;

    [HideInInspector] public Animator animator;
    protected Rigidbody2D rigid;


    [HideInInspector]  private float inputX;
    [HideInInspector]  private float inputY;


    float moveSpeed = 6f;

    float jumpPower = 15f;
    bool jumped = false;
    bool isGround = true;
    protected virtual void Init()
    {
        GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

    }
    void Start()
    {
        Init();
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        Move();
        Jump();

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
        if (jumped != true)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                jumped = true;
                rigid.velocity = Vector2.zero;
                //animator.SetBool("Dash", false);
                rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
            }
            
        }
    }
    #endregion

    #region 무브
    protected void Move()
    {
        rigid.velocity = new Vector2(inputX * moveSpeed, rigid.velocity.y);
        if (inputX != 0)
        {
            playerDir = inputX > 0 ? PlayerDir.right : playerDir = PlayerDir.left;
        }

        LookDir();
    }
    #endregion

    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("IsGround"))
        {
            jumped = false;
        }
    }

}

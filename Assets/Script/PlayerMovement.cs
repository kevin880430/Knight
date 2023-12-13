using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度
    public float jumpForce = 7f; // 跳跃力量
    public LayerMask groundLayer; // 地面层
    float JumpTimer;

    private SpriteRenderer sr;
    private Rigidbody2D rb2d;
    public bool isGrounded;
    private Animator PlayerAnimator;
    private FallingState fallingstate;
    public bool isJumptoFall=false;
    public bool isFalling=false;
    public enum FallingState
    {
        JumptoFall,
        ElsetoFall
    }

    void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 检测是否在地面上
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.9f, groundLayer);
        isGrounded = hit.collider != null;
        PlayerAnimator.SetBool("IsGrounded", isGrounded);
        Debug.DrawRay(transform.position, Vector2.down * 0.9f, Color.red);
        /*Move();*/

        // 跳跃
        Jump();

        if (isGrounded&&JumpTimer>=0.1f)
        {
            PlayerAnimator.SetBool("Jump", false);
            JumpTimer = 0;
        }
        Fall();
       
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.C))
        { // 施加向上的力以进行跳跃 
            if (isGrounded)
            {
                rb2d.AddForce(Vector2.up * jumpForce);
                PlayerAnimator.SetFloat("Run", 0);
                PlayerAnimator.SetBool("Jump", true);
            }

        }
        if (PlayerAnimator.GetBool("Jump"))
        {
            JumpTimer += Time.deltaTime;
        }
    }
   
    void Move()
    {
        // 左右移动
        float horizontalInput = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(horizontalInput * moveSpeed, rb2d.velocity.y);
        PlayerAnimator.SetFloat("Run", Mathf.Abs(horizontalInput));
        if (horizontalInput > 0)
        {
            sr.flipX = false;
        }
        if (horizontalInput <0)
        {
            sr.flipX = true;
        }
    }
    void Fall()
    { 
        int intValue = (int)Mathf.Floor(rb2d.velocity.y);

        if (!isGrounded)
        {
            if (intValue == 0 )
                isJumptoFall = true;
            PlayerAnimator.SetBool("JumptoFall", isJumptoFall);
        }
        if (isGrounded)
        {
            isFalling = false;
            PlayerAnimator.SetBool("Fall", isFalling);
        }
        if (rb2d.velocity.y<0 && !isGrounded) 
        {
            isFalling = true;
            isJumptoFall = false;
            PlayerAnimator.SetBool("Fall", isFalling);

        }
    }
    public void TestJump()
    {
        if (isGrounded)
        {
            rb2d.AddForce(Vector2.up * jumpForce);
            PlayerAnimator.SetFloat("Run", 0);
            PlayerAnimator.SetBool("Jump", true);
        }
        if (PlayerAnimator.GetBool("Jump"))
        {
            JumpTimer += Time.deltaTime;
        }
    }

}

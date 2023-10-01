using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度
    public float jumpForce = 7f; // 跳跃力量
    public LayerMask groundLayer; // 地面层

    private SpriteRenderer sr;
    private Rigidbody2D rb2d;
    public bool isGrounded;
    private Animator PlayerAnimator;

    void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 检测是否在地面上
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        isGrounded = hit.collider != null;
        Debug.DrawRay(transform.position, Vector2.down * 1f, Color.red);
        Move();

        // 跳跃
        if (Input.GetKeyDown(KeyCode.C) && isGrounded)
        {
            Jump();
        }

        // 处理下落
        if (rb2d.velocity.y<0)
        {
            Fall();
        }
    }

    void Jump()
    {
        // 施加向上的力以进行跳跃
        rb2d.AddForce(Vector2.up * jumpForce);
        if (rb2d.velocity.y >= 0)
        {
            PlayerAnimator.SetBool("Jump",true);
            PlayerAnimator.SetBool("Fall", false);
            PlayerAnimator.SetBool("JumpFallBtw", false);

        }
        if (rb2d.velocity.y == 0&&(PlayerAnimator.GetBool("Jump")||(PlayerAnimator.GetBool("Fall"))))
        {
            PlayerAnimator.SetBool("JumpFallBtw", true);
        }
        if(rb2d.velocity.y<0)
        {
            PlayerAnimator.SetBool("Fall", true);
            PlayerAnimator.SetBool("Jump", false);
            PlayerAnimator.SetBool("JumpFallBtw", false);
        }
    }
    void Move()
    {
        // 左右移动
        float horizontalInput = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(horizontalInput * moveSpeed, rb2d.velocity.y);
        if (horizontalInput != 0&&isGrounded)
        {
            PlayerAnimator.SetBool("Run", true);
        }
        else
        {
            PlayerAnimator.SetBool("Run", false);
        }
    }
    void Fall()
    {
        
    }
}

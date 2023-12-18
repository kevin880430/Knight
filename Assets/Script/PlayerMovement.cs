using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //移動速度
    public float moveSpeed = 5f; 
    //ジャンプ力
    public float jumpForce = 7f; 
    //地面レイヤー
    public LayerMask groundLayer;
    //ジャンプタイマー
    float JumpTimer;
    //SpriteRendererを格納する
    private SpriteRenderer sr;
    //Rigidbody2Dを格納する
    private Rigidbody2D rb2d;
    //地面にいるかとかの検知
    public bool isGrounded;
    //アニメーターを格納する
    private Animator PlayerAnimator;
    //空中速度=0のフラグ
    public bool isJumptoFall=false;
    //落下のフラグ
    public bool isFalling=false;

    void Start()
    {
        //アニメーターなどの情報を取得
        PlayerAnimator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 線を下に飛ばす、地面にいるかとかを検知
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.9f, groundLayer);
        //線がなにかに当たったら(検知有効)
        isGrounded = hit.collider != null;
        //プレイヤーが地面にいると判定
        PlayerAnimator.SetBool("IsGrounded", isGrounded);
        //飛ばされた線を表示する
        Debug.DrawRay(transform.position, Vector2.down * 0.9f, Color.red);
        /*Move();*/

        //ジャンプ
        Jump();

        //地面判定があると地面から離れた0.1秒以上
        if (isGrounded&&JumpTimer>=0.1f)
        {
            //アニメションを止める
            PlayerAnimator.SetBool("Jump", false);
            JumpTimer = 0;
        }
        //落下
        Fall();
       
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.C))
        { // 地面にいるなら 
            if (isGrounded)
            {
                //ジャンプさせる
                rb2d.AddForce(Vector2.up * jumpForce);
                PlayerAnimator.SetFloat("Run", 0);
                PlayerAnimator.SetBool("Jump", true);
            }

        }
        //ジャンプのタイマーを足していく
        if (PlayerAnimator.GetBool("Jump"))
        {
            JumpTimer += Time.deltaTime;
        }
    }
   
    void Move()
    {
        //左右移動
        float horizontalInput = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(horizontalInput * moveSpeed, rb2d.velocity.y);
        //スピードの値は常に正数
        PlayerAnimator.SetFloat("Run", Mathf.Abs(horizontalInput));
        //入力による画像を左右反転
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
        //RigidBody2Dのy軸速度を整数にする(0になった瞬間判断しやすい)
        int intValue = (int)Mathf.Floor(rb2d.velocity.y);
        //プレイヤーが地面にいない
        if (!isGrounded)
        {
            //空中速度=0の時
            if (intValue == 0 )
                //落下の直前フラグon
                isJumptoFall = true;
            //落下の直前アニメションを再生
            PlayerAnimator.SetBool("JumptoFall", isJumptoFall);
        }
        //プレイヤーが地面にいるなら
        if (isGrounded)
        {
            //落下のフラグOff
            isFalling = false;
            PlayerAnimator.SetBool("Fall", isFalling);
        }
        //RigidBody2Dのy軸速度が0以下、地面にいな(つまり落下中)
        if (rb2d.velocity.y<0 && !isGrounded) 
        {
            //落下フラグon
            isFalling = true;
            //落下の直前フラグoff
            isJumptoFall = false;
            //落下アニメションを再生
            PlayerAnimator.SetBool("Fall", isFalling);

        }
    }

    //チュートリアル用呼ばれるメソッド
    public void TestJump()
    {
        //呼ばれたらジャンプさせる
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

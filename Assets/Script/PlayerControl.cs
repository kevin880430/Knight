using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //攻撃状態を宣言
    private AttackState attackState = AttackState.Idle;
    //最近の攻撃時間(いつ)
    private float lastAttackTime;
    //連撃の許容時間
    public float comboTimeout = 1.0f; 
    //攻撃入力制限時間
    public float attackCooldown = 0.5f;
    //タイマーのスクリプトを取得するためGMを宣言する
    private Timer GameManager;
    //アニメーターを宣言
    private Animator PlayerAnimator;
    public enum AttackState
    {
        //プレイヤーの攻撃状態
        Idle,        
        FirstAttack, 
        SecondAttack 
    }
    

 
    void Start()
    {
        //アニメーターを取得する
        PlayerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        // Zを押されたらプレイヤーの攻撃を処理
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attcak();
        }

        // 攻撃した後一定時間後プレイヤーの攻撃状態をリセット
        if (attackState != AttackState.Idle && Time.time - lastAttackTime > comboTimeout)
        {
            attackState = AttackState.Idle;
        }
    }
    public void Attcak()
    {
        // 今の攻撃状態によって、違う攻撃処理をする
        switch (attackState)
        {
            case AttackState.Idle:
                // 一撃
                FirstAttack();
                break;
            case AttackState.FirstAttack:
                // 二連撃
                SecondAttack();
                break;
            case AttackState.SecondAttack:
                // 二連撃後一から連撃
                FirstAttack();
                break;
        }
    }
    public void FirstAttack()
    {
        //攻撃頻度制限時間内なら
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            
            Debug.Log("Performing First Attack");
            //攻撃状態の移行
            attackState = AttackState.FirstAttack;
            //攻撃アニメションを再生
            PlayerAnimator.SetTrigger("Attack1");
            //攻撃時間を記録(制限時間を計算するため)
            lastAttackTime = Time.time;
        }
      
    }

    private void SecondAttack()
    {
        
        Debug.Log("Performing Second Attack");
        //攻撃状態の移行
        attackState = AttackState.SecondAttack;
        //攻撃アニメションを再生(二連撃目)
        PlayerAnimator.SetTrigger("Attack2");
        //攻撃時間を記録(制限時間を計算するため)
        lastAttackTime = Time.time;
    }
    public void GetHit()
    {
        //被ダメージアニメションを再生
        PlayerAnimator.SetTrigger("GetHit");
    }
    public void Dead()
    {
        //死亡アニメションを再生
        PlayerAnimator.SetTrigger("isDead");
        //自分を削除する
        Destroy(this.gameObject, 1.5f);
        //タイマースクリプトを取得する
        GameManager = GameObject.Find("GameManager").GetComponent<Timer>();
        //GameOver画面に遷移
        GameManager.DelayEnd();
    }
}

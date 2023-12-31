﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AttackState
{
    //プレイヤーの攻撃状態
    Idle,
    FirstAttack,
    SecondAttack
}
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
    //ChangeSceneのスクリプトを取得する
    private ChangeScene changeScene;
    //アニメーターを宣言
    private Animator PlayerAnimator;
    //HPSystemのスクリプトを取得する
    private HPSystem hpSystem;
    void Start()
    {
        //ChangeScene、hpSystemのスクリプトを取得する
        changeScene = GameObject.Find("GameManager").GetComponent<ChangeScene>();
        hpSystem = GameObject.Find("Player").GetComponent<HPSystem>();
        //アニメーターを取得する
        PlayerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        // Zを押されたらプレイヤーの攻撃を処理
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }

        // 攻撃した後一定時間後プレイヤーの攻撃状態をリセット
        if (attackState != AttackState.Idle && Time.time - lastAttackTime > comboTimeout)
        {
            attackState = AttackState.Idle;
        }
    }
    public void Attack()
    {
        // 今の攻撃状態によって、違う攻撃処理をする
        switch (attackState)
        {
            case AttackState.Idle:
                // 一撃
                AttackMode("Attack1");
                break;
            case AttackState.FirstAttack:
                // 二連撃
                AttackMode("Attack2");
                break;
            case AttackState.SecondAttack:
                // 二連撃後一から連撃
                AttackMode("Attack1");
                break;
        }
    }
    public void AttackMode(string attackName)
    {
        //攻撃頻度制限時間内なら
        if (Time.time - lastAttackTime >= attackCooldown)
        {

            Debug.Log(attackName);
            //攻撃状態の移行
            attackState = AttackState.FirstAttack;
            //攻撃アニメションを再生
            PlayerAnimator.SetTrigger(attackName);
            //攻撃時間を記録(制限時間を計算するため)
            lastAttackTime = Time.time;
        }

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
        Invoke("GameOver", 1.5f);
       
    }
    //ゲームオーバー画面に遷移
    void GameOver()
    {
        changeScene.TransitionToScene("GameOver");
    }

    //FireBallに当たったらhpを減らす
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FireBall"))
        {
            //ダメージアニメションを再生
            GetHit();
            //ダメージする
            hpSystem.TakeDamage();
        }
    }
}

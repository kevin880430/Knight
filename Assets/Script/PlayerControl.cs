using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private AttackState attackState = AttackState.Idle;
    private float lastAttackTime;
    public float comboTimeout = 1.0f; // 定义连击的超时时间
    public float attackCooldown = 0.5f; // 攻击冷却时间
    public enum AttackState
    {
        Idle,        // 空闲状态
        FirstAttack, // 第一段攻击
        SecondAttack // 第二段攻击
    }
    private Animator PlayerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        // 检查玩家输入或点击以触发攻击
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attcak();
        }

        // 如果攻击超时，重置为Idle状态
        if (attackState != AttackState.Idle && Time.time - lastAttackTime > comboTimeout)
        {
            attackState = AttackState.Idle;
        }
    }
    private void Attcak()
    {
        // 根据当前攻击状态执行不同的逻辑
        switch (attackState)
        {
            case AttackState.Idle:
                // 触发第一段攻击
                FirstAttack();
                break;
            case AttackState.FirstAttack:
                // 触发第二段攻击
                SecondAttack();
                break;
            case AttackState.SecondAttack:
                // 重置为第一段攻击
                FirstAttack();
                break;
        }
    }
    private void FirstAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            // 执行第一段攻击逻辑
            Debug.Log("Performing First Attack");
            attackState = AttackState.FirstAttack;
            PlayerAnimator.SetTrigger("Attack1");
            lastAttackTime = Time.time;
        }
      
    }

    private void SecondAttack()
    {
        // 执行第二段攻击逻辑
        Debug.Log("Performing Second Attack");
        attackState = AttackState.SecondAttack;
        PlayerAnimator.SetTrigger("Attack2");
        lastAttackTime = Time.time;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyControl : MonoBehaviour
{
    
    //�A�j���[�^�[��錾����
    private Animator EnemeyAnimator;
    //�^�C�}�[�̃X�N���v�g���擾���邽��GM��錾����
    private Timer GameManager;
    //�ő�hp
    public float maxHealth = 3.0f;
    //����hp
    public static float currentHealth;
    //hp�o�[�̐}�`
    public Image hpBar;
    //FireBall�̃v���n�u
    public GameObject fireballPrefab;
    public Transform fireballPos;
    void Start()
    {
        //�A�j���[�^�[���擾����
        EnemeyAnimator = GetComponent<Animator>();
        //hp������
        currentHealth = maxHealth;
    }
    public void Gethurt()
    {
        //�U�����ꂽ�A�j���[�V�������Đ�
        Invoke("GetHit", 0.15f);
        //�_���[�W�󂯂���hp������    
        currentHealth -= 1;
        //hp�o�[����������
        hpBar.fillAmount -= 1 / maxHealth;

        Debug.Log("EnemyGetDamage");

        //hp��0�ȉ��ɂȂ�����
        if (currentHealth <= 0)
        {
            //���S����
            Dead();

        }
    }
    public void GetHit()
    {
        //��_���[�W�A�j���V�������Đ�
        EnemeyAnimator.SetTrigger("GetHit");
    }
    public void Dead()
    {
        //���S�`�F�b�NON
        EnemeyAnimator.SetBool("isDead", true);
        //�������폜
        Destroy(this.gameObject, 0.9f);
        //�^�C�}�[�X�N���v�g���擾����
        GameManager = GameObject.Find("GameManager").GetComponent<Timer>();
        //GameClear��ʂɑJ��
        GameManager.DelayClear();
    }

    public void Attack()
    {
        Instantiate(fireballPrefab, fireballPos);
        //�U���A�j���V�������Đ�
        EnemeyAnimator.SetTrigger("Attack");
    }
}

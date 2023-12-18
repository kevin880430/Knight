using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyControl : MonoBehaviour
{
    
    //アニメーターを宣言する
    private Animator EnemeyAnimator;
    //ChangeSceneのスクリプトを取得するためGMを宣言する
    private ChangeScene changeScene;
    //タイマーのスクリプトを取得するためGMを宣言する
    private Timer timer;
    //最大hp
    public float maxHealth = 3.0f;
    //現在hp
    public static float currentHealth;
    //hpバーの図形
    public Image hpBar;
    //FireBallのプレハブ
    public GameObject fireballPrefab;
    public Transform fireballPos;
    void Start()
    {
        //ChangeSceneスクリプトを取得する
        changeScene = GameObject.Find("GameManager").GetComponent<ChangeScene>();
        //ChangeSceneスクリプトを取得する
        timer = GameObject.Find("GameManager").GetComponent<Timer>();
        //アニメーターを取得する
        EnemeyAnimator = GetComponent<Animator>();
        //hp初期化
        currentHealth = maxHealth;
    }
    public void Gethurt()
    {
        //攻撃されたアニメーションを再生
        Invoke("GetHit", 0.15f);
        //ダメージ受けたらhpを減る    
        currentHealth -= 1;
        //hpバー長さを減る
        hpBar.fillAmount -= 1 / maxHealth;

        Debug.Log("EnemyGetDamage");

        //hpが0以下になったら
        if (currentHealth <= 0)
        {
            //死亡処理
            Dead();

        }
    }
    public void GetHit()
    {
        //被ダメージアニメションを再生
        EnemeyAnimator.SetTrigger("GetHit");
    }
    public void Dead()
    {
        //死亡チェックON
        EnemeyAnimator.SetBool("isDead", true);
        //GameClear画面に遷移
        Invoke("GameClear", 1.5f);
    }
    public void Attack()
    {
        Instantiate(fireballPrefab, fireballPos);
        //攻撃アニメションを再生
        EnemeyAnimator.SetTrigger("Attack");
    }
    public void GameClear()
    {
        //残り時間を取得する
        timer.RecordTime();
        //GameClearに画面遷移
        changeScene.TransitionToScene("GameClear");
        
    }
}

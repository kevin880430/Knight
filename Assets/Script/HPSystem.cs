using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HPSystem : MonoBehaviour
{
    //プレイヤー最大hp
    public float maxHealth = 3.0f;
    //プレイヤー現在hp
    public static float currentHealth;
    //hpバーの図形
    public Image hpBar;
    //ChangeSceneのスクリプトを取得する
    //PlayerControllのスクリプトを取得する
    private PlayerControl Player;
    private void Start()
    {
        //プレイヤーが持ってるスクリプト情報を取得する
        Player = GameObject.Find("Player").GetComponent<PlayerControl>();
        //プレイヤーhpを初期化する
        currentHealth = maxHealth;

    }
    public void TakeDamage()
    {
        //hpを減る    
        currentHealth -= 1;
        //hpバー長さを減る
        hpBar.fillAmount-= 1/maxHealth;
        //攻撃されるアニメーションを再生
        Player.GetHit();
        Debug.Log("GetDamage");

        //hpが0以下になったら
        if (currentHealth <= 0)
        {
            //死亡メッセージを表示する
            Debug.Log("Player has died!");
            //死亡アニメーションを再生
            Player.Dead();
        }
    }
}
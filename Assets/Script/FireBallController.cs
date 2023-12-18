using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    //爆発プレハブ
    public GameObject explodePrefab;
    //飛ばし速度
    public float speed=1;
    void Update()
    {
        //右方向に移動させる
        transform.Translate(Vector3.right* speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //プレイヤーなどとの衝突処理
        if (collision.gameObject.CompareTag("Player"))
        {
            //爆発プレハブを生成
            Instantiate(explodePrefab, transform.position,transform.rotation);
            //自分を削除する(0.1秒後)
            Destroy(this.gameObject,0.1f);
            
        }
        //壁と衝突したら
        if (collision.gameObject.CompareTag("Wall"))
        {
            //自分を削除する
            Destroy(this.gameObject);
        }
    }
}

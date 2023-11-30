using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    //爆発プレハブ
    public GameObject explodePrefab;
    void Update()
    {
        transform.Translate(Vector3.right*Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //プレイヤーなどとの衝突処理
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(explodePrefab, transform.position,transform.rotation);
            Destroy(this.gameObject,0.1f);
            
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}

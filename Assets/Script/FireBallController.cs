using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    //�����v���n�u
    public GameObject explodePrefab;
    //��΂����x
    public float speed=1;
    void Update()
    {
        transform.Translate(Vector3.right* speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�v���C���[�ȂǂƂ̏Փˏ���
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

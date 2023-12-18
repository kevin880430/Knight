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
        //�E�����Ɉړ�������
        transform.Translate(Vector3.right* speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�v���C���[�ȂǂƂ̏Փˏ���
        if (collision.gameObject.CompareTag("Player"))
        {
            //�����v���n�u�𐶐�
            Instantiate(explodePrefab, transform.position,transform.rotation);
            //�������폜����(0.1�b��)
            Destroy(this.gameObject,0.1f);
            
        }
        //�ǂƏՓ˂�����
        if (collision.gameObject.CompareTag("Wall"))
        {
            //�������폜����
            Destroy(this.gameObject);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HPSystem : MonoBehaviour
{
    //�v���C���[�ő�hp
    public float maxHealth = 3.0f;
    //�v���C���[����hp
    public static float currentHealth;
    //hp�o�[�̐}�`
    public Image hpBar;
    //ChangeScene�̃X�N���v�g���擾����
    //PlayerControll�̃X�N���v�g���擾����
    private PlayerControl Player;
    private void Start()
    {
        //�v���C���[�������Ă�X�N���v�g�����擾����
        Player = GameObject.Find("Player").GetComponent<PlayerControl>();
        //�v���C���[hp������������
        currentHealth = maxHealth;

    }
    public void TakeDamage()
    {
        //hp������    
        currentHealth -= 1;
        //hp�o�[����������
        hpBar.fillAmount-= 1/maxHealth;
        //�U�������A�j���[�V�������Đ�
        Player.GetHit();
        Debug.Log("GetDamage");

        //hp��0�ȉ��ɂȂ�����
        if (currentHealth <= 0)
        {
            //���S���b�Z�[�W��\������
            Debug.Log("Player has died!");
            //���S�A�j���[�V�������Đ�
            Player.Dead();
        }
    }
}
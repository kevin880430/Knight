using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QTEManager2 : MonoBehaviour
{
    //�{�^����ۑ�����z���錾
    public GameObject[] buttons;
    //�{�^���̐����ʒu
    public Transform spawnPosition;
    //��������{�^���̐�
    public int sequenceLength = 4;
    //���݃{�^���̏���
    public int currentIndex = 0;
    //�����_�����������{�^���I�u�W�F�N�g�̏��Ԃ����X�g�ɕۑ�����(Prefab Asset)
    public List<GameObject> currentSequence = new List<GameObject>();
    //�����_���������ꂽ�{�^���I�u�W�F�N�g�̏��Ԃ����X�g�ɕۑ�����(Clone)
    public List<GameObject> generatedButtons = new List<GameObject>();
    //�{�^���̐����ʒu�̃I�u�W�F�N�g
    public GameObject spawnPositionObj;
    //PlayerControl�̏����擾���邽�ߐ錾����ϐ�
    private PlayerControl Player;
    //HPSystem�̏����擾���邽�ߐ錾����ϐ�
    private HPSystem PlayerHP;
    //EnemyControl�̏����擾���邽�ߐ錾����ϐ�
    private EnemyControl Enemy;
    //���͋��`�F�b�N
    private bool canInput = true;
    public float Timer;
    private bool hasExecuted = false;


    void Start()
    {
        
        //�{�^���������_����������
        GenerateRandomSequence();
    }
    void Update()
    {
        
            Timer += Time.deltaTime;
            
            // �v���C���[�̓��͂ƌ��ݏ��Ԃ̃{�^����v���邩�Ƃ�(upArrowKey�̏ꍇ)
            if (Timer >=1&& currentIndex==0)
            {
                //��v�����玟�̃{�^���𔻒f����(���f����+1)
                currentIndex++;
                //���m�ɉ����ꂽ�{�^���̉摜�������ꂽ��Ԃɐ؂�ւ�
                generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState();
            }
            // �v���C���[�̓��͂ƌ��ݏ��Ԃ̃{�^����v���邩�Ƃ�(downArrowKey�̏ꍇ)
            else if (Timer >= 2 && currentIndex == 1)
            {
                //��v�����玟�̃{�^���𔻒f����(���f���� + 1)
                currentIndex++;
                //���m�ɉ����ꂽ�{�^���̉摜�������ꂽ��Ԃɐ؂�ւ�
                generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState(); // ���p�����I����
            }
        // �v���C���[�̓��͂ƌ��ݏ��Ԃ̃{�^����v���邩�Ƃ�(leftArrowKey�̏ꍇ)
            else if (Timer >= 3 && currentIndex == 2)
            {
                //��v�����玟�̃{�^���𔻒f����(���f���� + 1)
                currentIndex++;
                //���m�ɉ����ꂽ�{�^���̉摜�������ꂽ��Ԃɐ؂�ւ�
                generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState(); // ���p�����I����
            }
        // �v���C���[�̓��͂ƌ��ݏ��Ԃ̃{�^����v���邩�Ƃ�(rightArrowKey�̏ꍇ)
            else if (Timer >= 4 && currentIndex == 3)
            {
                //��v�����玟�̃{�^���𔻒f����(���f���� + 1)
                currentIndex++;
                //���m�ɉ����ꂽ�{�^���̉摜�������ꂽ��Ԃɐ؂�ւ�
                generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState(); // ���p�����I����
            }
            //�{�^�������m�ɉ�����Ă��Ȃ��ꍇ
            /*else if (Input.anyKeyDown)
            {
                //Enemy�̍U������������
                Enemy = GameObject.Find("Enemy").GetComponent<EnemyControl>();
                Enemy.Attack();
                //�v���C���[�̔�_���[�W����������
                PlayerHP = GameObject.Find("Player").GetComponent<HPSystem>();
                PlayerHP.TakeDamage();
                //�V�����{�^����������܂œ��͕s��
                canInput = false;
                //�{�^���摜��U��(���͈Ⴄ�̕\��)
                spawnPositionObj.GetComponent<Shake>().ShakeThis();
                //0.5�b��V�����{�^����������
                Invoke("ReGenerate", 0.5f);
            }*/

            //�S���̃{�^�������m�ɉ����ꂽ��
            if (currentIndex == sequenceLength)
            {
                
                Timer = 0;
                //�v���C���[�̍U���ƓG�̔�_���[�W����������
                Player = GameObject.Find("Player").GetComponent<PlayerControl>();
                Enemy = GameObject.Find("Enemy").GetComponent<EnemyControl>();
                Player.FirstAttack();
                Enemy.Gethurt();
                //�V�����{�^����������܂œ��͕s��
                canInput = false;
                Debug.Log("Attack");
            currentIndex = 0;
            //0.3�b��V�����{�^����������
            Invoke("ReGenerate", 0.3f);
            }
        
    }
    void GenerateRandomSequence()
    {
        // �{�^�����Ԃ������_���Ő���(�ݒ�{�^������)
        for (int i = 0; i < sequenceLength; i++)
        {
            //0����ݒ萔�܂Ń����_���l��ϐ��ɑ��
            int randomIndex = Random.Range(0, buttons.Length);
            //��������{�^�����Ԃ�z��ɒǉ�
            currentSequence.Add(buttons[randomIndex]);
        }
        //��������{�^���̊ԋ���
        float spacing = 1.3f;

        for (int i = 0; i < currentSequence.Count; i++)
        {
            //�ݒ萔�܂Ń{�^���v���n�u�𐶐�(���ԁA�ʒu�A�ԋ����Ȃ�)
            GameObject button = Instantiate(currentSequence[i], spawnPosition.position + Vector3.right * spacing * i, Quaternion.identity, spawnPosition);
            //�������ꂽ�v���n�u��z��ɒǉ�
            generatedButtons.Add(button);
            //�������ꂽ�{�^���v���n�u�摜��\������
            button.SetActive(true);
        }
    }


    public void ReGenerate()
    {
        //�������ꂽ�{�^���v���n�u���폜����
        foreach (var button in generatedButtons)
        {
            Destroy(button);
        }
        //�{�^���v���n�u�̃��X�g��������(�N���A)
        generatedButtons.Clear();
        //�{�^�����Ԃ̃��X�g��������(�N���A)
        currentSequence.Clear();
        //���ݔ��f�̃{�^�����Ԃ����Z�b�g(��ڂ̃{�^������)
        currentIndex = 0;
        //�{�^���������_���Ő�������
        GenerateRandomSequence();
        //���͉\�`�F�b�N
        canInput = true;

    }



}
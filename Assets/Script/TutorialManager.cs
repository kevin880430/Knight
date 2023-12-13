using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialManager : MonoBehaviour
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
    //PlayerPlayerMovement�̏����擾���邽�ߐ錾����ϐ�
    private PlayerMovement PlayerM;
    //HPSystem�̏����擾���邽�ߐ錾����ϐ�
    private HPSystem PlayerHP;
    //EnemyControl�̏����擾���邽�ߐ錾����ϐ�
    private EnemyControl Enemy;
    //���͋��`�F�b�N
    public static bool canInput = true;
    public RhythmControl rhythmControl;
    public GameObject PerfectObj;
    public GameObject GoodObj;
    public GameObject BadObj;
    public Transform JudgeMentObjPos;
    public Image PlayerGage;
    public Image EnemyGage;
    public float PlayerGageValue = 10.0f;
    public float EnemyGageValue = 10.0f;
    public GameObject UltimatePictruePrefab;
    public JUDGE_STATE JudgeState;
    public string inputButton = "";
    public float PerfectPoint = 2;
    public float GoodPoint = 1;
    private bool InUltimate = false;

    public enum JUDGE_STATE
    {
        //����]�[��
        IS_PERFECT,
        IS_GOOD,
        IS_BAD
    }

    void Start()
    {
        //�v���C���[�ƓG�̃X�N���v�g���擾����
        Player = GameObject.Find("Player").GetComponent<PlayerControl>();
        PlayerM = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Enemy = GameObject.Find("Enemy").GetComponent<EnemyControl>();
        //�{�^���������_����������
        GenerateRandomSequence();
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
        //�S�Ă̕\���{�^�����폜����
        CleanAllButton();
        //�{�^���������_���Ő�������
        GenerateRandomSequence();
        //���͉\�`�F�b�N
        canInput = true;

    }
    public void AutoJudge()
    {
        Instantiate(PerfectObj, JudgeMentObjPos.position, Quaternion.identity, JudgeMentObjPos);
        //���݂̔��菇��+1
        currentIndex++;
        //���m�ɉ����ꂽ�{�^���̉摜��ؑ�
        generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState();
        //���m�ɉ����ꂽ�{�^����e��(�����ꂽ��)
        generatedButtons[currentIndex - 1].GetComponent<ButtonController>().Scale();
        //�S�Ẵ{�^�������m�ɉ����ꂽ�ꍇ
        if (currentIndex >= sequenceLength)
        {
            //�Z���g������Z�̐ݒ�����Z�b�g
            if (InUltimate)
            {
                InUltimate = false;
                sequenceLength = 4;
            }
            //�v���C���[�̍U���ƓG�̔�_���[�W����������
            
            Player.FirstAttack();
            Enemy.Gethurt();
            //�V�����{�^����������܂œ��͕s��
            canInput = false;
            Debug.Log("Attack");
            //0.3�b��V�����{�^����������
            Invoke("ReGenerate", 0.3f);
        }
    }
    public void EnemyGageFill(float FillAmount)
    {
        EnemyGage.fillAmount += FillAmount / EnemyGageValue;
        if (EnemyGage.fillAmount == 1)
        {
            //Enemy�̍U������������
            Enemy = GameObject.Find("Enemy").GetComponent<EnemyControl>();
            Enemy.Attack();
            //�v���C���[�������I�W�����v������
            PlayerM.TestJump();
            EnemyGage.fillAmount = 0;
            
        }
    }
    private void CleanAllButton()
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
    }
   

}
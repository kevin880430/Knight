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
        //�{�^���������_����������
        GenerateRandomSequence();
    }
    void Update()
    {
        //���̓^�C�~���O���m�F
        JudgeStateDetect();
        //�{�^���̔��f�p���O�𓯊�
        ButtonCheck();
        //���͉\�Ȃ�
        if (canInput && currentIndex < sequenceLength)
        {
            // ���݂̏��Ԃ̃{�^���Ɠ��͂��ꂽ�{�^������v���邩����
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.C) && !Input.GetKeyDown(KeyCode.X))
            {
                if (!string.IsNullOrEmpty(inputButton) && inputButton == currentSequence[currentIndex].name && !InUltimate)
                {
                    switch (JudgeState)
                    {
                        case JUDGE_STATE.IS_PERFECT:
                            //���蕶����\������
                            Instantiate(PerfectObj, JudgeMentObjPos.position, Quaternion.identity, JudgeMentObjPos);
                            //���݂̔��菇��+1
                            currentIndex++;
                            //���m�ɉ����ꂽ�{�^���̉摜��ؑ�
                            generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState();
                            //���m�ɉ����ꂽ�{�^����e��(�����ꂽ��)
                            generatedButtons[currentIndex - 1].GetComponent<ButtonController>().Scale();
                            break;
                        case JUDGE_STATE.IS_GOOD:
                            //���蕶����\������
                            Instantiate(GoodObj, JudgeMentObjPos.position, Quaternion.identity, JudgeMentObjPos);
                            //���݂̔��菇��+1
                            currentIndex++;
                            //���m�ɉ����ꂽ�{�^���̉摜��ؑ�
                            generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState();
                            //���m�ɉ����ꂽ�{�^����e��(�����ꂽ��)
                            generatedButtons[currentIndex - 1].GetComponent<ButtonController>().Scale();

                            break;
                        case JUDGE_STATE.IS_BAD:
                            //���蕶����\������
                            Instantiate(BadObj, JudgeMentObjPos.position, Quaternion.identity, JudgeMentObjPos);
                            ErrorInputProcess();
                            break;

                    }

                }
                //�Z���g���Ƃ�����]�[���𖳎�����
                else if (InUltimate && !string.IsNullOrEmpty(inputButton) && inputButton == currentSequence[currentIndex].name)
                {
                    Instantiate(PerfectObj, JudgeMentObjPos.position, Quaternion.identity, JudgeMentObjPos);
                    currentIndex++;
                    generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState();
                }
                //�Z���ԈȊO���͈Ⴂ�͔F�߂�
                else if (!InUltimate)
                {
                    Instantiate(BadObj, JudgeMentObjPos.position, Quaternion.identity, JudgeMentObjPos);
                    ErrorInputProcess();
                }


            }
            //�S���̃{�^�������m�ɉ����ꂽ��
            if (currentIndex >= sequenceLength)
            {
                //�Z���g������Z�̐ݒ�����Z�b�g
                if (InUltimate)
                {
                    InUltimate = false;
                    sequenceLength = 4;
                }
                //�v���C���[�̍U���ƓG�̔�_���[�W����������
                Player = GameObject.Find("Player").GetComponent<PlayerControl>();
                Enemy = GameObject.Find("Enemy").GetComponent<EnemyControl>();
                Player.FirstAttack();
                Enemy.Gethurt();
                //�V�����{�^����������܂œ��͕s��
                canInput = false;
                Debug.Log("Attack");
                //Player�Q�[�W�𗭂܂�
                PlayerGageFill();
                //0.3�b��V�����{�^����������
                Invoke("ReGenerate", 0.3f);
            }

        }
        //Player�̋Z�����A�Q�[�W�����܂�����X�Ŕ���
        if (PlayerGage.fillAmount == 1 && Input.GetKeyDown(KeyCode.X))
        {
            PlayerUlitmate();
            PlayerGage.fillAmount = 0;
            Instantiate(UltimatePictruePrefab, spawnPosition.position, transform.rotation);
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
        //�S�Ă̕\���{�^�����폜����
        CleanAllButton();
        //�{�^���������_���Ő�������
        GenerateRandomSequence();
        //���͉\�`�F�b�N
        canInput = true;

    }
    private void ErrorInputProcess()
    {
        print("input error");
        Instantiate(BadObj, JudgeMentObjPos.position, Quaternion.identity, JudgeMentObjPos);
        //Enemy�Q�[�W�𗭂܂�
        EnemyGageFill(1.0f);
        /*//�v���C���[�̔�_���[�W����������
        PlayerHP = GameObject.Find("Player").GetComponent<HPSystem>();
        PlayerHP.TakeDamage();*/
        //�V�����{�^����������܂œ��͕s��
        canInput = false;
        //�{�^���摜��U��(���͈Ⴄ�̕\��)
        spawnPositionObj.GetComponent<Shake>().ShakeThis();
        //0.5�b��V�����{�^����������
        Invoke("ReGenerate", 0.5f);
    }
    private void JudgeStateDetect()
    {
        //����]�[�������m
        if (rhythmControl.JudgeState == RhythmControl.JUDGE_STATE.ISPERFECT)
        {
            JudgeState = JUDGE_STATE.IS_PERFECT;
        }
        if (rhythmControl.JudgeState == RhythmControl.JUDGE_STATE.ISGOOD)
        {
            JudgeState = JUDGE_STATE.IS_GOOD;
        }
        if (rhythmControl.JudgeState == RhythmControl.JUDGE_STATE.ISBAD)
        {
            JudgeState = JUDGE_STATE.IS_BAD;
        }
    }

    private void ButtonCheck()
    {
        //�{�^���������Ƃ��������{�^���ɖ��O����(����p)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            inputButton = "Button_up";
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            inputButton = "Button_down";
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            inputButton = "Button_left";
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            inputButton = "Button_right";
        }
    }
    private void PlayerGageFill()
    {
        PlayerGage.fillAmount += 2 / PlayerGageValue;

    }
    public void EnemyGageFill(float FillAmount)
    {
        EnemyGage.fillAmount += FillAmount / EnemyGageValue;
        if (EnemyGage.fillAmount == 1)
        {
            //Enemy�̍U������������
            Enemy = GameObject.Find("Enemy").GetComponent<EnemyControl>();
            Enemy.Attack();
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
    private void PlayerUlitmate()
    {
        CleanAllButton();
        InUltimate = true;
        // �{�^�����Ԃ������_���Ő���(�ݒ�{�^������)
        for (int i = 0; i < sequenceLength; i++)
        {
            //0����ݒ萔�܂Ń����_���l��ϐ��ɑ��
            int randomIndex = Random.Range(0, buttons.Length);
            //��������{�^�����Ԃ�z��ɒǉ�
            currentSequence.Add(buttons[randomIndex]);
        }
        //��������{�^���̊ԋ���
        float spacing = 0.6f;

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

}
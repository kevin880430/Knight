using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;


public class ChangeScene : MonoBehaviour
{
    //TransitionManager���擾����
    TransitionManager TransitionM;
    //Transition�̎d��������Ƃ���
    public TransitionSettings transition;
    //����scene�̖��O
    private string sceneName;
    void Start()
    {
        //����scene�̖��O���擾����
        sceneName = SceneManager.GetActiveScene().name;
        //TransitionManager��TransitionM �Ɋi�[����
        TransitionM = TransitionManager.Instance();
    }
    private void Update()
    {
        //���݂͂ǂ�scene�ɂ���𔻒f
        switch (sceneName)
        {
            //�`���[�g���A�����esc����������^�C�g����ʂɑJ��
            case "Tutorial":
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    TransitionToScene("Title");
                }
                break;
            //�Q�[���I�o�[���̓Q�[���N���A��ʂ�R����������^�C�g����ʂɑJ��
            case "GameOver":
            case "GameClear":
                if (Input.GetKeyDown(KeyCode.R))
                {
                    TransitionToScene("Title");
                }
                break;
        }
    }

    //ONClicK�ɑΉ�����Scene�̐ؑ֕�
    public void TransitionToScene(string sceneName)
    {
        //Transition����Đ�
        TransitionM.Transition(transition, 0.3f);
        //�w�肵��scene�ɑJ��
        StartCoroutine(LoadSceneAfterDelay(sceneName, 0.5f));
    }

    IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        //�����҂��Ă����ʑJ�ڂ��s��
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
    //UI�{�^���p���C����ʑJ��
    public void LoadMain()
    {
        TransitionToScene("Main");
    }
    //UI�{�^���p�`���[�g���A����ʑJ��
    public void LoadTutorial()
    {
        TransitionToScene("Tutorial");
    }

    //UI�{�^���p�Q�[���𗣒E
    public void QuitGame()
    {
        Application.Quit();
    }
    
}


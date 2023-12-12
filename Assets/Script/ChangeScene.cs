using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;


public class ChangeScene : MonoBehaviour
{
    //Transition�@�\�̃N���X���i�[
    TransitionManager TransitionM;
    //Transition�̃G�t�F�N�g���(Object)
    public TransitionSettings transition;
    //�؂�ւ���Scene�̖��O
    public string SceneName;
    void Update()
    {
        //R���������烁�C����ʂɖ߂�
        if (Input.GetKeyDown(KeyCode.R))
        {
            TransitionM = TransitionManager.Instance();
            TransitionM.Transition(transition, 0.0f);
            Invoke("LoadScene", 1f);
        }
    }
    void LoadScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}

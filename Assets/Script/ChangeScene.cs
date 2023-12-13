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
      /*  
        if (Input.GetKeyDown(KeyCode.R))
        {
            TransitionM = TransitionManager.Instance();
            TransitionM.Transition(transition, 0.0f);
            Invoke("LoadScene", 1f);
        }*/
    }
    public void TransitionScene()
    {
        //Transition���s��
        TransitionM = TransitionManager.Instance();
        //Transition�̎�ށA�J�n�܂ł̎���
        TransitionM.Transition(transition, 0.0f);
        Invoke("LoadScene", 1f);
    }
    void LoadScene()
    {
        //Scene��؂�ւ���
        SceneManager.LoadScene(SceneName);
    }
}

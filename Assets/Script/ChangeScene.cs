using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;


public class ChangeScene : MonoBehaviour
{
    //Transition機能のクラスを格納
    TransitionManager TransitionM;
    //Transitionのエフェクト種類(Object)
    public TransitionSettings transition;
    //切り替えるSceneの名前
    public string SceneName;
    void Update()
    {
        //Rを押したらメイン画面に戻る
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;


public class ChangeScene : MonoBehaviour
{
    //TransitionManagerを取得する
    TransitionManager TransitionM;
    //Transitionの仕方を入れるところ
    public TransitionSettings transition;
    //現在sceneの名前
    private string sceneName;
    void Start()
    {
        //現在sceneの名前を取得する
        sceneName = SceneManager.GetActiveScene().name;
        //TransitionManagerをTransitionM に格納する
        TransitionM = TransitionManager.Instance();
    }
    private void Update()
    {
        //現在はどのsceneにいるを判断
        switch (sceneName)
        {
            //チュートリアル画面escを押したらタイトル画面に遷移
            case "Tutorial":
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    TransitionToScene("Title");
                }
                break;
            //ゲームオバー又はゲームクリア画面でRを押したらタイトル画面に遷移
            case "GameOver":
            case "GameClear":
                if (Input.GetKeyDown(KeyCode.R))
                {
                    TransitionToScene("Title");
                }
                break;
        }
    }

    //ONClicKに対応するSceneの切替方
    public void TransitionToScene(string sceneName)
    {
        //Transition動画再生
        TransitionM.Transition(transition, 0.3f);
        //指定したsceneに遷移
        StartCoroutine(LoadSceneAfterDelay(sceneName, 0.5f));
    }

    IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        //少し待ってから画面遷移を行う
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
    //UIボタン用メイン画面遷移
    public void LoadMain()
    {
        TransitionToScene("Main");
    }
    //UIボタン用チュートリアル画面遷移
    public void LoadTutorial()
    {
        TransitionToScene("Tutorial");
    }

    //UIボタン用ゲームを離脱
    public void QuitGame()
    {
        Application.Quit();
    }
    
}


﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EasyTransition;
public class Timer : MonoBehaviour
{
    //タイマーを表示するテキスト
    public Text countdownText; 
    //総時間
    public float totalTime = 30f; 
    //現在の時間
    private float currentTime = 0f;
    //カウントダウン状態チェック
    private bool isCounting = false;
    //Transition機能のクラスを格納
    TransitionManager TransitionM;
    //Transitionのエフェクト種類(Object)
    public TransitionSettings transition;
    //ChangeSceneのスクリプトを取得する
    private ChangeScene changeScene;
    void Start()
    {
        //ChangeSceneのスクリプトを取得する
        changeScene = GameObject.Find("GameManager").GetComponent<ChangeScene>();
        //時間を初期化
        currentTime = totalTime;
        //カウントされてる時間テキストを更新する
        UpdateCountdownText();
    }

    void Update()
    {
        //入力があったらカウントが始まる
        if (Input.anyKeyDown)
        {
            StartCountdown();
        }
        //カウント状態、時間もゼロじゃなかったら
        if (isCounting&& currentTime!=0)
        {
            //現在時間を計算する
            currentTime -= Time.deltaTime;
            //時限になったら
            if (currentTime <= 0f)
            {
                //残り時間=0、GameOver
                currentTime = 0f;
                isCounting = false;
                countdownText.text = "Times Up!";
                changeScene.TransitionToScene("GameOver");
            }
            ////カウントされてる時間テキストを更新する
            UpdateCountdownText();
        }
    }

   
    void UpdateCountdownText()
    {
        //カウントされてる時間テキストを更新する
        int seconds = Mathf.CeilToInt(currentTime);
        countdownText.text = "Time Left: " + seconds.ToString() + "s";
    }

    
    public void StartCountdown()
    {
        //カウントダウン状態on
        isCounting = true;
    }
    
    public void RecordTime()
    {
        float remainingTime = currentTime;
        //残り時間を記録する
        PlayerPrefs.SetFloat("RemainingTime", remainingTime);
    }
}

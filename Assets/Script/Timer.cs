using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    void Start()
    {
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
                DelayEnd();
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
    public void DelayEnd()
    {
        //1.5秒後GameOver画面に
        Invoke("GameOver", 1.5f);
    }
    public void DelayClear()
    {
        //1.5秒後GameClear画面に
        Invoke("GameClear", 1.5f);
    }
    // 结束游戏
    public void GameOver()
    {
        //GameOver画面に遷移
        SceneManager.LoadScene("GameOver");
    }
    public void GameClear()
    {
        float remainingTime = currentTime;
        //残り時間を記録する
        PlayerPrefs.SetFloat("RemainingTime", remainingTime);
        //GameClear画面に遷移
        SceneManager.LoadScene("GameClear");
    }
}

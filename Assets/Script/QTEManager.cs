using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QTEManager : MonoBehaviour
{
    //ボタンを保存する配列を宣言
    public GameObject[] buttons;
    //ボタンの生成位置
    public Transform spawnPosition;
    //生成するボタンの数
    public int sequenceLength = 4;
    //現在ボタンの順番
    public int currentIndex = 0;
    //ランダム生成したボタンオブジェクトの順番をリストに保存する(Prefab Asset)
    public List<GameObject> currentSequence = new List<GameObject>();
    //ランダム生成されたボタンオブジェクトの順番をリストに保存する(Clone)
    public List<GameObject> generatedButtons = new List<GameObject>();
    //ボタンの生成位置のオブジェクト
    public GameObject spawnPositionObj;
    //PlayerControlの情報を取得するため宣言する変数
    private PlayerControl Player;
    //HPSystemの情報を取得するため宣言する変数
    private HPSystem PlayerHP;
    //EnemyControlの情報を取得するため宣言する変数
    private EnemyControl Enemy;
    //入力許可チェック
    public static bool canInput = true;
    //RhythmControlを取得する
    public RhythmControl rhythmControl;
    //技の画像プレハブ
    public GameObject UltimatePictruePrefab;
    //良い判定の文字プレハブ
    public GameObject PerfectObj;
    //可判定の文字プレハブ
    public GameObject GoodObj;
    //不可判定の文字プレハブ
    public GameObject BadObj;
    //判定文字を生成する場所
    public Transform JudgeMentObjPos;
    //プレイヤーゲージの画像
    public Image PlayerGage;
    //敵のゲージの画像
    public Image EnemyGage;
    //プレイヤーゲージ最大値
    public float PlayerGageValue = 10.0f;
    //プレイヤーゲージの最大値
    public float EnemyGageValue = 10.0f;
    //今の判定状態
    public JUDGE_STATE JudgeState;
    //ボタンの名前を判別
    public string inputButton = "";
    //技使ってるかとか
    private bool InUltimate=false;

   
    void Start()
    {
        //プレイヤーと敵のスクリプトを取得する
        Player = GameObject.Find("Player").GetComponent<PlayerControl>();
        Enemy = GameObject.Find("Enemy").GetComponent<EnemyControl>();
        //ボタンをランダム生成する
        GenerateRandomSequence();
    }
    void Update()
    {
        //入力タイミングを確認
        JudgeStateDetect();
        //ボタンの判断用名前を同期
        ButtonCheck();
        //入力可能なら
        if (canInput && currentIndex < sequenceLength )
        {
            // 現在の順番のボタンと入力されたボタンが一致するか判定
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.C) && !Input.GetKeyDown(KeyCode.X))
            {
              if (!string.IsNullOrEmpty(inputButton) && inputButton == currentSequence[currentIndex].name && !InUltimate)
              {
                    switch (JudgeState)
                    {
                        case JUDGE_STATE.IS_PERFECT:
                            //判定文字を表示する
                            Instantiate(PerfectObj, JudgeMentObjPos.position, Quaternion.identity, JudgeMentObjPos);
                            //現在の判定順番+1
                            currentIndex++;
                            //正確に押されたボタンの画像を切替
                            generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState();
                            //正確に押されたボタンを弾く(押された感)
                            generatedButtons[currentIndex - 1].GetComponent<ButtonController>().Scale();
                            break;
                        case JUDGE_STATE.IS_GOOD:
                            //判定文字を表示する
                            Instantiate(GoodObj, JudgeMentObjPos.position, Quaternion.identity, JudgeMentObjPos);
                            //現在の判定順番+1
                            currentIndex++;
                            //正確に押されたボタンの画像を切替
                            generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState();
                            //正確に押されたボタンを弾く(押された感)
                            generatedButtons[currentIndex - 1].GetComponent<ButtonController>().Scale();

                            break;
                        case JUDGE_STATE.IS_BAD:
                            //判定文字を表示する
                            Instantiate(BadObj, JudgeMentObjPos.position, Quaternion.identity, JudgeMentObjPos);
                            ErrorInputProcess();
                            break;

                    }

              }
                //技を使うとき判定ゾーンを無視する
                else if (InUltimate && !string.IsNullOrEmpty(inputButton) && inputButton == currentSequence[currentIndex].name)
                {
                    //判定文字を表示する
                    Instantiate(PerfectObj, JudgeMentObjPos.position, Quaternion.identity, JudgeMentObjPos);
                    currentIndex++;
                    generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState();
                }
                //技時間以外入力違いは認める
                else if(!InUltimate)
                {
                    //判定文字を表示する
                    Instantiate(BadObj, JudgeMentObjPos.position, Quaternion.identity, JudgeMentObjPos);
                    ErrorInputProcess();
                }
               

            }
            //全部のボタンが正確に押されたら
            if (currentIndex >= sequenceLength)
            {
                //技を使った後技の設定をリセット
                if (InUltimate)
                {
                    InUltimate = false;
                    sequenceLength = 4;
                }
                //プレイヤーの攻撃と敵の被ダメージを処理する
                Player.AttackMode("Attack1");
                Enemy.Gethurt();
                //新しいボタン生成するまで入力不可
                canInput = false;
                Debug.Log("Attack");
                //Playerゲージを溜まる
                PlayerGageFill();
                //0.3秒後新しいボタン生成する
                Invoke("ReGenerate", 0.3f);
            }

        }
        //Playerの技処理、ゲージが溜まったらXで発動
        if (PlayerGage.fillAmount == 1 && Input.GetKeyDown(KeyCode.X))
        {
            PlayerUlitmate();
            PlayerGage.fillAmount = 0;
            Instantiate(UltimatePictruePrefab, spawnPosition.position, transform.rotation);
        }
    }
    void GenerateRandomSequence()
    {
        // ボタン順番をランダムで生成(設定ボタン数分)
        for (int i = 0; i < sequenceLength; i++)
        {
            //0から設定数までランダム値を変数に代入
            int randomIndex = Random.Range(0, buttons.Length);
            //生成されボタン順番を配列に追加
            currentSequence.Add(buttons[randomIndex]);
        }
        //生成するボタンの間距離
        float spacing = 1.3f;

        for (int i = 0; i < currentSequence.Count; i++)
        {
            //設定数までボタンプレハブを生成(順番、位置、間距離など)
            GameObject button = Instantiate(currentSequence[i], spawnPosition.position + Vector3.right * spacing * i, Quaternion.identity, spawnPosition);
            //生成されたプレハブを配列に追加
            generatedButtons.Add(button);
            //生成されたボタンプレハブ画像を表示する
            button.SetActive(true);
        }
    }

    public void ReGenerate()
    {
        //全ての表示ボタンを削除する
        CleanAllButton();
        //ボタンをランダムで生成する
        GenerateRandomSequence();
        //入力可能チェック
        canInput = true;

    }
    private void ErrorInputProcess()
    {
        print("input error");
        //判定文字を表示する
        Instantiate(BadObj, JudgeMentObjPos.position, Quaternion.identity, JudgeMentObjPos);
        //Enemyゲージを溜まる
        EnemyGageFill(1.0f);
        /*//新しいボタン生成するまで入力不可
        canInput = false;*/
        //ボタン画像を振動(入力違うの表現)
        spawnPositionObj.GetComponent<Shake>().ShakeThis();
       /* //0.5秒後新しいボタン生成する
        Invoke("ReGenerate", 0.5f);*/
    }
    private void JudgeStateDetect()
    {
        //判定ゾーンを検知
        if (rhythmControl.JudgeState == JUDGE_STATE.IS_PERFECT)
        {
            JudgeState = JUDGE_STATE.IS_PERFECT;
        }
        if (rhythmControl.JudgeState == JUDGE_STATE.IS_GOOD)
        {
            JudgeState = JUDGE_STATE.IS_GOOD;
        }
        if (rhythmControl.JudgeState == JUDGE_STATE.IS_BAD)
        {
            JudgeState = JUDGE_STATE.IS_BAD;
        }
    }

    private void ButtonCheck()
    {
        //ボタンを押すとき押したボタンに名前つける(判定用)
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
        //プレイヤーゲージを溜める
        PlayerGage.fillAmount += 2 / PlayerGageValue;
        
    }
    public void EnemyGageFill(float FillAmount)
    {
       EnemyGage.fillAmount += FillAmount / EnemyGageValue;
        if (EnemyGage.fillAmount == 1)
        {
            //敵の攻撃を処理する
            Enemy = GameObject.Find("Enemy").GetComponent<EnemyControl>();
            Enemy.Attack();
            //敵のゲージを溜める
            EnemyGage.fillAmount = 0;
        }
    }
    private void CleanAllButton()
    {
        //生成されたボタンプレハブを削除する
        foreach (var button in generatedButtons)
        {
            Destroy(button);
        }
        //ボタンプレハブのリストを初期化(クリア)
        generatedButtons.Clear();
        //ボタン順番のリストを初期化(クリア)
        currentSequence.Clear();
        //現在判断のボタン順番をリセット(一個目のボタンから)
        currentIndex = 0;
    }
    private void  PlayerUlitmate()
    {
        CleanAllButton();
        InUltimate = true;
        // ボタン順番をランダムで生成(設定ボタン数分)
        for (int i = 0; i < sequenceLength; i++)
        {
            //0から設定数までランダム値を変数に代入
            int randomIndex = Random.Range(0, buttons.Length);
            //生成されボタン順番を配列に追加
            currentSequence.Add(buttons[randomIndex]);
        }
        //生成するボタンの間距離
        float spacing = 0.6f;

        for (int i = 0; i < currentSequence.Count; i++)
        {
            //設定数までボタンプレハブを生成(順番、位置、間距離など)
            GameObject button = Instantiate(currentSequence[i], spawnPosition.position + Vector3.right * spacing * i, Quaternion.identity, spawnPosition);
            //生成されたプレハブを配列に追加
            generatedButtons.Add(button);
            //生成されたボタンプレハブ画像を表示する
            button.SetActive(true);
        }
    }

}

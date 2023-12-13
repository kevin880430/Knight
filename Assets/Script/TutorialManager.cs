using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialManager : MonoBehaviour
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
    //PlayerPlayerMovementの情報を取得するため宣言する変数
    private PlayerMovement PlayerM;
    //HPSystemの情報を取得するため宣言する変数
    private HPSystem PlayerHP;
    //EnemyControlの情報を取得するため宣言する変数
    private EnemyControl Enemy;
    //入力許可チェック
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
        //判定ゾーン
        IS_PERFECT,
        IS_GOOD,
        IS_BAD
    }

    void Start()
    {
        //プレイヤーと敵のスクリプトを取得する
        Player = GameObject.Find("Player").GetComponent<PlayerControl>();
        PlayerM = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Enemy = GameObject.Find("Enemy").GetComponent<EnemyControl>();
        //ボタンをランダム生成する
        GenerateRandomSequence();
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
    public void AutoJudge()
    {
        Instantiate(PerfectObj, JudgeMentObjPos.position, Quaternion.identity, JudgeMentObjPos);
        //現在の判定順番+1
        currentIndex++;
        //正確に押されたボタンの画像を切替
        generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState();
        //正確に押されたボタンを弾く(押された感)
        generatedButtons[currentIndex - 1].GetComponent<ButtonController>().Scale();
        //全てのボタンが正確に押された場合
        if (currentIndex >= sequenceLength)
        {
            //技を使った後技の設定をリセット
            if (InUltimate)
            {
                InUltimate = false;
                sequenceLength = 4;
            }
            //プレイヤーの攻撃と敵の被ダメージを処理する
            
            Player.FirstAttack();
            Enemy.Gethurt();
            //新しいボタン生成するまで入力不可
            canInput = false;
            Debug.Log("Attack");
            //0.3秒後新しいボタン生成する
            Invoke("ReGenerate", 0.3f);
        }
    }
    public void EnemyGageFill(float FillAmount)
    {
        EnemyGage.fillAmount += FillAmount / EnemyGageValue;
        if (EnemyGage.fillAmount == 1)
        {
            //Enemyの攻撃を処理する
            Enemy = GameObject.Find("Enemy").GetComponent<EnemyControl>();
            Enemy.Attack();
            //プレイヤーを自動的ジャンプさせる
            PlayerM.TestJump();
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
   

}
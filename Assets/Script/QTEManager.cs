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

    void Start()
    {
        //ボタンをランダム生成する
        GenerateRandomSequence();  
    }
    void Update()
    {
        //入力可能なら
        if (canInput && currentIndex < sequenceLength)
        {
            // プレイヤーの入力と現在順番のボタン一致するかとか(upArrowKeyの場合)
            if (Input.GetKeyDown(KeyCode.UpArrow) && currentSequence[currentIndex].name == "Button_up")
            {
                //一致したら次のボタンを判断する(判断順番+1)
                currentIndex++;
                //正確に押されたボタンの画像を押された状態に切り替え
                generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState(); 
            }
            // プレイヤーの入力と現在順番のボタン一致するかとか(downArrowKeyの場合)
            else if (Input.GetKeyDown(KeyCode.DownArrow) && currentSequence[currentIndex].name == "Button_down")
            {
                //一致したら次のボタンを判断する(判断順番 + 1)
                currentIndex++;
                //正確に押されたボタンの画像を押された状態に切り替え
                generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState(); // 調用按鈕上的函數
            }
            // プレイヤーの入力と現在順番のボタン一致するかとか(leftArrowKeyの場合)
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentSequence[currentIndex].name == "Button_left")
            {
                //一致したら次のボタンを判断する(判断順番 + 1)
                currentIndex++;
                //正確に押されたボタンの画像を押された状態に切り替え
                generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState(); // 調用按鈕上的函數
            }
            // プレイヤーの入力と現在順番のボタン一致するかとか(rightArrowKeyの場合)
            else if (Input.GetKeyDown(KeyCode.RightArrow) && currentSequence[currentIndex].name == "Button_right")
            {
                //一致したら次のボタンを判断する(判断順番 + 1)
                currentIndex++;
                //正確に押されたボタンの画像を押された状態に切り替え
                generatedButtons[currentIndex-1].GetComponent<ButtonController>().SetPressedState(); // 調用按鈕上的函數
            }
            //ボタンが正確に押されていない場合
            else if (Input.anyKeyDown)
            {
                //Enemyの攻撃を処理する
                Enemy = GameObject.Find("Enemy").GetComponent<EnemyControl>();
                Enemy.Attack();
                //プレイヤーの被ダメージを処理する
                PlayerHP = GameObject.Find("Player").GetComponent<HPSystem>();
                PlayerHP.TakeDamage();
                //新しいボタン生成するまで入力不可
                canInput = false;
                //ボタン画像を振動(入力違うの表現)
                spawnPositionObj.GetComponent<Shake>().ShakeThis();
                //0.5秒後新しいボタン生成する
                Invoke("ReGenerate", 0.5f);
            }

            //全部のボタンが正確に押されたら
            if (currentIndex >= sequenceLength)
            {
                //プレイヤーの攻撃と敵の被ダメージを処理する
                Player = GameObject.Find("Player").GetComponent<PlayerControl>();
                Enemy = GameObject.Find("Enemy").GetComponent<EnemyControl>();
                Player.FirstAttack();
                Enemy.Gethurt();
                //新しいボタン生成するまで入力不可
                canInput = false;
                Debug.Log("Attack");
                //0.3秒後新しいボタン生成する
                Invoke("ReGenerate", 0.3f);
            }
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
        //ボタンをランダムで生成する
        GenerateRandomSequence();
        //入力可能チェック
        canInput = true;

    }
    
   
    
}

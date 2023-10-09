using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QTEManager : MonoBehaviour
{
    public GameObject[] buttons; // 存放按鈕的GameObject
    public Transform spawnPosition; // 生成按鈕的位置
    public int sequenceLength = 4; // 按鈕序列的長度
    public int currentIndex = 0;
    public List<GameObject> currentSequence = new List<GameObject>();
    public List<GameObject> generatedButtons = new List<GameObject>();
    public GameObject spawnPositionObj;
    public ButtonController bottoncontrol;
    private PlayerControl Player;
    private EnemyControl Enemy;
    private bool canInput = true;

    void Start()
    {
        GenerateRandomSequence();
        
    }
    void Update()
    {

        if (canInput && currentIndex < sequenceLength)
        {
            // 檢查當前按鈕是否與玩家輸入相符
            if (Input.GetKeyDown(KeyCode.UpArrow) && currentSequence[currentIndex].name == "Button_up")
            {
                currentIndex++;
                generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState(); // 調用按鈕上的函數
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && currentSequence[currentIndex].name == "Button_down")
            {
                currentIndex++;
                generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState(); // 調用按鈕上的函數
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentSequence[currentIndex].name == "Button_left")
            {
                currentIndex++;
                generatedButtons[currentIndex - 1].GetComponent<ButtonController>().SetPressedState(); // 調用按鈕上的函數
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && currentSequence[currentIndex].name == "Button_right")
            {
                currentIndex++;
                generatedButtons[currentIndex-1].GetComponent<ButtonController>().SetPressedState(); // 調用按鈕上的函數
            }
            else if (Input.anyKeyDown)
            {
                canInput = false;
                spawnPositionObj.GetComponent<Shake>().ShakeThis();
                Invoke("ReGenerate", 0.5f);
            }

            // 如果序列輸入完成，執行攻擊
            if (currentIndex >= sequenceLength)
            {
                Player = GameObject.Find("Player").GetComponent<PlayerControl>();
                Enemy = GameObject.Find("Enemy").GetComponent<EnemyControl>();
                Player.FirstAttack();
                Enemy.Gethurt();
                canInput = false;
                Debug.Log("Attack");
                Invoke("ReGenerate", 0.3f);
            }
        }
    }
    void GenerateRandomSequence()
    {
        // 隨機生成按鈕序列，可以允許重複
        for (int i = 0; i < sequenceLength; i++)
        {
            int randomIndex = Random.Range(0, buttons.Length);
            currentSequence.Add(buttons[randomIndex]);
        }
        // 根據生成的按鈕序列生成按鈕在畫面中
        float spacing = 2f; // 按键之间的间距
        for (int i = 0; i < currentSequence.Count; i++)
        {
            GameObject button = Instantiate(currentSequence[i], spawnPosition.position + Vector3.right * spacing * i, Quaternion.identity, spawnPosition);
            generatedButtons.Add(button);
            button.SetActive(true);
        }
    }

    
    public void ReGenerate()
    {
        foreach (var button in generatedButtons)
        {
            Destroy(button);
        }
        // 清空列表以便下次使用
        generatedButtons.Clear();
        currentSequence.Clear();
        currentIndex = 0;
        GenerateRandomSequence();
        canInput = true;

    }
    
   
    
}

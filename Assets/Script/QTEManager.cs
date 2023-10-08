using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QTEManager : MonoBehaviour
{
    public GameObject[] buttons; // 存放按鈕的GameObject
    public Transform spawnPoint; // 生成按鈕的位置
    public int sequenceLength = 4; // 按鈕序列的長度
    public int currentIndex = 0;
    public List<GameObject> currentSequence = new List<GameObject>();
    public bool isRightBtn;
    public BottonControler bottoncontrol;

    void Start()
    {
        GenerateRandomSequence();
        
    }
    void Update()
    {

        // 檢查玩家輸入
        if (currentIndex < sequenceLength)
        {
            // 檢查當前按鈕是否與玩家輸入相符
            if (Input.GetKeyDown(KeyCode.UpArrow) && currentSequence[currentIndex].name == "Botton_up")
            {
                currentIndex++;
                Destroy(currentSequence[currentIndex - 1]);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && currentSequence[currentIndex].name == "Botton_down")
            {
                currentIndex++;
                Destroy(currentSequence[currentIndex - 1]);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentSequence[currentIndex].name == "Botton_left")
            {
                currentIndex++;
                Destroy(currentSequence[currentIndex - 1]);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && currentSequence[currentIndex].name == "Botton_right")
            {
                currentIndex++;
                Destroy(currentSequence[currentIndex - 1]);
            }
            else if (Input.anyKeyDown)
            {
                ReGenerate();
            }


            // 如果序列輸入完成，執行攻擊
            if (currentIndex >= sequenceLength)
            {
                Debug.Log("Attack");
                ReGenerate();
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
        float spacing = 2f; // 按鈕之間的間距
        for (int i = 0; i < currentSequence.Count; i++)
        {
            GameObject button = Instantiate(currentSequence[i], spawnPoint.position + Vector3.right * spacing * i, Quaternion.identity);
            button.transform.SetParent(spawnPoint);
            currentSequence[i].SetActive(true);
            
        }
    }

  
    public void ReGenerate()
    {
        currentSequence.Clear();
        currentIndex = 0;
        GenerateRandomSequence();
        
    }
   
    
}

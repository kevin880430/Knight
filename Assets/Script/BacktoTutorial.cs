using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoTutorial : MonoBehaviour
{

    void Update()
    {
        //Rを押したらメイン画面に戻る
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
}

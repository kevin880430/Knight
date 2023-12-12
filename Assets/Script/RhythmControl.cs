using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmControl : MonoBehaviour
{
    //ノーツのプレハブ
    public GameObject beatPrefab;  
    //中心位置(ノーツの終点)
    public Transform centerPoint;
    //ノーツの移動速度
    public float beatSpeed = 5f; 
    //剣の弾くアニメション
    public Animator BeatingAnimator;
    //今の判定状態
    public JUDGE_STATE JudgeState;
    public enum JUDGE_STATE
    {
        //良い、可、不可三つの状態
        ISPERFECT,
        ISGOOD,
        ISBAD
    }
    private QTEManager qteManager;

    void Start()
    {
        //ノーツを生成
        StartCoroutine(GenerateBeats());
    }

    IEnumerator GenerateBeats()
    {
        //オブジェクト存在してる間、繰り返す生成
        while (true)
        {
            // 左側のノーツを生成
            GameObject leftBeat = Instantiate(beatPrefab, new Vector3(-5f, centerPoint.position.y, 0f), Quaternion.identity);

            // 右側のノーツを生成
            GameObject rightBeat = Instantiate(beatPrefab, new Vector3(5f, centerPoint.position.y, 0f), Quaternion.identity);

            // ノーツを中心位置に移動
            StartCoroutine(MoveBeat(leftBeat.transform, centerPoint.position));
            StartCoroutine(MoveBeat(rightBeat.transform, centerPoint.position));

            //次のノーツが生成するまで待つ
            yield return new WaitForSeconds(1f / beatSpeed);
        }
    }

    //ノーツを目標位置(中心点)に移動させる
    IEnumerator MoveBeat(Transform beatTransform, Vector3 targetPosition)
    {
         //ノーツと目標位置の距離
    　   float distance = Vector3.Distance(beatTransform.position, targetPosition);

         //距離が0.01以上の時
      　 while (distance > 0.01f)
    　   {
            //ノーツを移動
            beatTransform.position = Vector3.MoveTowards(beatTransform.position, targetPosition, beatSpeed * Time.deltaTime);
            //ノーツと目標位置の距離を常に更新する
            distance = Vector3.Distance(beatTransform.position, targetPosition);
            //次のフレームまで待つ
            yield return null;
    　   }

    　　//ノーツが中心位置に来たら削除する
    　　Destroy(beatTransform.gameObject);
      
    }

    //ノーツが来た時アニメションを再生(弾く)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //接触するオブジェクトのタグはBEATの場合
        if (collision.gameObject.CompareTag("Beat"))
        {
            //アニメションを再生
            BeatingAnimator.SetBool("Beating", true);
            Debug.Log("triggered");
        }
       
    }

    //ノーツが離れたらアニメションの再生を停止
    private void OnCollisionExit2D(Collision2D collision)
    {
        //接触するオブジェクトのタグはBEATの場合
        if (collision.gameObject.CompareTag("Beat"))
        {
            //アニメションを再生
            BeatingAnimator.SetBool("Beating", false);
            Debug.Log("untriggered");
            //Enemyゲージを溜まる
            GameObject.Find("GameManager").GetComponent<QTEManager>().EnemyGageFill(0.5f);
        }

    }
    //各判定エリアの状態をチェック、0はnormal、1はperfect、2はBad
    public virtual void OnTriggerEnterProxy(Collider2D other, RhythmJudge judge)
    {
        //判定番号は0の場合
        if (judge.JudgeIndex == 0)
        {
            //可の状態に切替
            JudgeState = JUDGE_STATE.ISGOOD;
        }
        //判定番号は1の場合
        if (judge.JudgeIndex == 1)
        {
            //良の状態に切替
            JudgeState = JUDGE_STATE.ISPERFECT;
        }
        //判定番号は2の場合
        if (judge.JudgeIndex == 2)
        {
            //不可の状態に切替
            JudgeState = JUDGE_STATE.ISBAD;
        }
    }
    
}

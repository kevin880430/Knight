using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TutorialRhythmControl : MonoBehaviour
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
    private float notecount=0;
    private TutorialManager tutorial;
    void Start()
    {
        //GameManageのScriptを取得する
        tutorial=GameObject.Find("GameManager").GetComponent<TutorialManager>();
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
            //判定したノーツを計算
            notecount++;
            //ノーツが二個判定したら
            if (notecount == 2&&tutorial!=null)
            {
                //自動的ボタンを判定
                tutorial.AutoJudge();
                notecount = 0;
            }
            //Enemyゲージを溜まる
            tutorial.EnemyGageFill(1);

        }

    }
    //各判定エリアの状態をチェック、0はnormal、1はperfect、2はBad
    public virtual void OnTriggerEnterProxy(Collider2D other, RhythmJudge judge)
    {
        switch (judge.JudgeIndex)
        {
            case 0:
                JudgeState = JUDGE_STATE.IS_GOOD;
                break;
            case 1:
                JudgeState = JUDGE_STATE.IS_PERFECT;
                break;
            case 2:
                JudgeState = JUDGE_STATE.IS_BAD;
                break;
        }
    }

}

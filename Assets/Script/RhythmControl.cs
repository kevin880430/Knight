using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmControl : MonoBehaviour
{
    public GameObject beatPrefab;  // 节拍预制体
    public Transform centerPoint;  // 中心点
    public float beatSpeed = 5f;   // 节拍移动速度
    public Animator BeatingAnimator;
    
    void Start()
    {
        // 开始协程生成节拍
        StartCoroutine(GenerateBeats());
    }

    IEnumerator GenerateBeats()
    {
        while (true)
        {
            // 生成左侧节拍
            GameObject leftBeat = Instantiate(beatPrefab, new Vector3(-5f, centerPoint.position.y, 0f), Quaternion.identity);

            // 生成右侧节拍
            GameObject rightBeat = Instantiate(beatPrefab, new Vector3(5f, centerPoint.position.y, 0f), Quaternion.identity);

            // 移动节拍向中心
            StartCoroutine(MoveBeat(leftBeat.transform, centerPoint.position));
            StartCoroutine(MoveBeat(rightBeat.transform, centerPoint.position));

            // 等待下一拍
            yield return new WaitForSeconds(1f / beatSpeed);
        }
    }

    IEnumerator MoveBeat(Transform beatTransform, Vector3 targetPosition)
    {
    float distance = Vector3.Distance(beatTransform.position, targetPosition);

    while (distance > 0.01f)
    {
        beatTransform.position = Vector3.MoveTowards(beatTransform.position, targetPosition, beatSpeed * Time.deltaTime);
        distance = Vector3.Distance(beatTransform.position, targetPosition);
        yield return null;
    }

    // 销毁节拍物体或者进行其他处理
    Destroy(beatTransform.gameObject);
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Beat"))
        {
            BeatingAnimator.SetBool("Beating", true);
            Debug.Log("triggered");
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Beat"))
        {
            BeatingAnimator.SetBool("Beating", false);
            Debug.Log("untriggered");
        }

    }
}

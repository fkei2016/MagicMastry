using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// オブザーバーパターン用の初期設定.
[RequireComponent(typeof(BehaviorTree_ObseverSCR))]
[RequireComponent(typeof(BehaviorTree_SubjectSCR))]

public class MoveNodeSCR : BehaviorTreeNodeSCR
{
    [SerializeField, Tooltip("自分自身")]
    GameObject m_gameObject;


    // 移動速度
    float m_speed = 5.0f;

    // 待機時間の最大値.
    float m_waitTimeMax = 2;

    [SerializeField, Tooltip("待機時間")]
    float m_waitTime;
    [SerializeField, Tooltip("経過時間")]
    float m_elapsedTime;

    /**
    * @brief 初期化
    * @author shinji
    * @date 10/27
    */
    public override void Setup()
    {
        m_waitTime = Random.Range(0.0f, m_waitTimeMax);
        m_elapsedTime = 0;
    }

    /**
    * @brief 実行する
    * @author shinji
    * @date 10/27
    */
    public override void DoAction()
    {
        // 1.時間経過.
        m_elapsedTime += Time.deltaTime;

        // 2.経過時間が待ち時間を超えた場合.
        if (m_elapsedTime > m_waitTime)
        {
            m_subjectSCR.Notify();
            return;
        }

        // 3.移動させる.
        Vector3 deltaDistance;
        deltaDistance = m_gameObject.transform.forward * m_speed * Time.deltaTime;
        m_gameObject.transform.position += deltaDistance;
    }
}
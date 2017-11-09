using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// オブザーバーパターン用の初期設定.
[RequireComponent(typeof(BehaviorTree_ObseverSCR))]
[RequireComponent(typeof(BehaviorTree_SubjectSCR))]

public class RotateNodeSCR : BehaviorTreeNodeSCR
{
    [SerializeField, Tooltip("オブジェクト")]
    GameObject m_gameObject;


    // 回転速度.
    float m_speed = 75;
    // 待機時間の最大値.
    float m_waitTimeMax = 2;

    [SerializeField, Tooltip("待機時間")]
    float m_waitTime;
    [SerializeField, Tooltip("経過時間")]
    float m_elapsedTime;


    /**
      * @brief Startと酷似する関数
      * @author shinji
      * @date 10/30
      */
    public override void Setup()
    {
        m_waitTime = Random.Range(0.0f, m_waitTimeMax);
        m_elapsedTime = 0;

        int hoge = Random.Range(0, 2);
        if (hoge == 0)
            m_speed *= -1;
    }

    /**
      * @brief Updateと酷似する関数
      * @author shinji
      * @date 10/30
      */
    public override void DoAction()
    {
        // 1.経過時間が待ち時間を超えた場合.
        if (m_elapsedTime > m_waitTime)
        {
            m_subjectSCR.Notify();
            return;
        }

        // 2.時間経過.
        m_elapsedTime += Time.deltaTime;

        // 3.回転させる.
        var angles = m_gameObject.transform.rotation.eulerAngles;
        angles.y += Time.deltaTime * m_speed;
        m_gameObject.transform.rotation = Quaternion.Euler(angles);
    }
}